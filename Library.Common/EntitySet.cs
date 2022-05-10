using Library.Common.Dtos;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Common
{
    public class EntitySet<T> : IEntitySet<T>
    {
        protected Type entityType;
        protected readonly SqlConnection _connection;
        public EntitySet(SqlConnection connection)
        {
            _connection = connection;
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            entityType = typeof(T);
            if (string.IsNullOrWhiteSpace(TableName))
            {
                if (entityType.Name.EndsWith('y'))
                {
                    _tableName = entityType.Name.Substring(0, entityType.Name.Length - 1) + "es";
                }
                else
                {
                    _tableName = entityType.Name + "s";
                }
            }
        }

        protected readonly string _tableName = "";

        protected virtual string TableName => _tableName;

        public virtual T Create(T entity)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            var pros = entityType.GetProperties();
            var parameters = new List<SqlParameter>(pros.Length);
            var cmdIdentity = _connection.CreateCommand();
            cmdIdentity.CommandText = $"DECLARE @next_id int = IDENT_CURRENT('dbo.[{TableName}]') + IDENT_INCR('dbo.[{TableName}]');" +
                "SELECT @next_id";
            var id = cmdIdentity.ExecuteScalar();
            var check = id.ToString();
            if (!string.IsNullOrWhiteSpace(check))
            {
                entityType.GetProperty("Id").SetValue(entity, id);
            }
            var columns = "";
            var parameterNames = "";
            if (entityType.GetInterface(nameof(IAuditedEntity)) != null)
            {
                entityType.GetMethod(nameof(IAuditedEntity.CreateTime)).Invoke(entity, null);
            }
            foreach (var prop in pros)
            {
                var value = prop.GetValue(entity);
                if (value != null)
                {
                    columns += prop.Name + ", ";
                    parameterNames += $"@{prop.Name}, ";
                    parameters.Add(new SqlParameter("@" + prop.Name, value));
                }
            }
            columns = columns.Trim(',', ' ');
            parameterNames = parameterNames.Trim(',', ' ');
            cmd.CommandText = $"INSERT INTO [dbo].[{TableName}] ({columns}) VALUES ({parameterNames}) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            cmd.Parameters.AddRange(parameters.ToArray());
            if (cmd.ExecuteNonQuery() > 0)
            {
                return entity;
            }
            return default(T);
        }

        public virtual bool Delete(object id)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"DELETE [dbo].[{TableName}] WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;

        }

        public virtual T Find(object id)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"SELECT * FROM [dbo].[{TableName}] WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            var obj = (T)entityType.GetConstructors()[0].Invoke(null);
            if (reader.Read())
                foreach (var pro in entityType.GetProperties())
                {
                    try
                    {
                        pro.SetValue(obj, reader[pro.Name]);
                    }
                    catch { }
                }
            return obj;
        }

        public virtual PagedAndSortedResultDto<T> Pagination(PagedAndSortedLookUpDto request)
        {
            var cmd = _connection.CreateCommand();
            var cmdCount = _connection.CreateCommand();
            var searchBy = "";
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                searchBy = "WHERE (";
                foreach (var pro in entityType.GetProperties())
                {
                    searchBy += $"{pro.Name} LIKE @search OR ";
                }
                searchBy = searchBy.Substring(0, searchBy.Length - 4) + ")";
                cmd.Parameters.AddWithValue("@search", $"%{request.Search}%");
                cmdCount.Parameters.AddWithValue("@search", $"%{request.Search}%");
            }
            cmd.CommandText = $"DECLARE @PageNumber AS INT DECLARE @RowsOfPage " +
                $"AS INT SET @PageNumber={request.PageIndex} SET @RowsOfPage={request.PageSize} " +
                $"SELECT * FROM [dbo].[{TableName}] {searchBy} ORDER BY (SELECT @column) {request.SortOrder} " +
                $"OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY";
            if (string.IsNullOrWhiteSpace(request.Column))
            {
                request.Column = "id";
            }
            cmd.Parameters.AddWithValue("@column", request.Column);
            cmdCount.CommandText = $"SELECT COUNT(*) FROM [dbo].[{TableName}] {searchBy}";
            var count = (int)cmdCount.ExecuteScalar();
            var reader = cmd.ExecuteReader();
            PagedAndSortedResultDto<T> result = new()
            {
                CurrentPage = request.PageIndex,
                PerPage = request.PageSize,
                TotalRecords = count,
                TotalPages = count / request.PageSize + (count % request.PageSize == 0 ? 0 : 1),
                Items = new List<T>()
            };
            while (reader.Read())
            {
                var obj = (T)entityType.GetConstructors()[0].Invoke(null);
                foreach (var pro in entityType.GetProperties())
                {
                    try
                    {
                        pro.SetValue(obj, reader[pro.Name]);
                    }
                    catch { }
                }
                result.Items.Add(obj);
            }
            reader.Close();
            return result;
        }

        public virtual bool Update(object id, T entity)
        {
            var cmd = _connection.CreateCommand();
            var sets = "";
            var pros = entityType.GetProperties();
            if (entityType.GetInterface(nameof(IAuditedEntity)) != null)
            {
                entityType.GetMethod(nameof(IAuditedEntity.UpdateTime)).Invoke(entity, null);
            }
            foreach (var prop in pros)
            {
                var value = prop.GetValue(entity);
                if (prop.Name.ToLower() != "id" && prop.Name.ToLower() != "createdat" && prop.Name.ToLower() != "createdby")
                {
                    if (value != null)
                    {
                        sets += $"{prop.Name} = @{prop.Name},";
                        cmd.Parameters.AddWithValue("@" + prop.Name, value);
                    }
                    else
                    {
                        sets += $"{prop.Name} = NULL,";
                    }
                }
            }
            sets = sets.Trim(',');
            cmd.CommandText = $"UPDATE [dbo].[{TableName}] SET {sets} WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public virtual IList<T> GetList(int? Count)
        {
            var cmd = _connection.CreateCommand();
            var top = "";
            if (Count.HasValue && Count.Value > 0)
            {
                top = "TOP " + Count;
            }
            cmd.CommandText = $"SELECT {top} * FROM [dbo].[{TableName}]";
            var reader = cmd.ExecuteReader();
            var result = new List<T>();
            while (reader.Read())
            {
                var obj = (T)entityType.GetConstructors()[0].Invoke(null);
                foreach (var pro in entityType.GetProperties())
                {
                    try
                    {
                        pro.SetValue(obj, reader[pro.Name]);
                    }
                    catch { }
                }
                result.Add(obj);
            }
            reader.Close();
            return result;
        }
    }
}
