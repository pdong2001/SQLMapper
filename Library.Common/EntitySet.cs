using Library.Common.Attributes;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

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
            var table = (TableNameAttribute)Attribute.GetCustomAttribute(entityType, typeof(TableNameAttribute));
            if (table == null)
            {
                if (entityType.Name.EndsWith('y'))
                {
                    TableName = entityType.Name.Substring(0, entityType.Name.Length - 1) + "es";
                }
                else
                {
                    TableName = entityType.Name + "s";
                }
            }
            else
            {
                TableName = table.TableName;
            }
        }

        public void CreateTableIfNotExists()
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName AND TABLE_SCHEMA = 'dbo'";
            cmd.Parameters.AddWithValue("@tableName", TableName);
            var isNotExists = (int)cmd.ExecuteScalar() == 0;
            cmd.Dispose();
            if (isNotExists)
            {
                Console.WriteLine($"[{DateTime.Now}] Tạo bảng [db].[{TableName}]");
                var properties = entityType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public);
                var columnDefine = "";
                cmd = _connection.CreateCommand();
                if (entityType.GetInterface(nameof(HasKey)) != null)
                {
                    var idType = entityType.GetProperty("Id").PropertyType;
                    var itemTypeName = idType.GetSQLTypeName();
                    columnDefine += $"id {itemTypeName} PRIMARY KEY {(itemTypeName == "INT NOT NULL" || itemTypeName == "BIGINT NOT NULL" ? "IDENTITY" : "")},";
                }
                if (entityType.GetInterface(nameof(IAuditedEntity)) != null)
                {
                    columnDefine += $"[{nameof(IAuditedEntity.Created_At)}] DATETIME2 NOT NULL,";
                    columnDefine += $"[{nameof(IAuditedEntity.Updated_At)}] DATETIME2 NULL,";
                }
                foreach (var item in properties)
                {
                    var propertyType = item.PropertyType;
                    columnDefine += $"[{item.Name}] {propertyType.GetSQLTypeName()} ,";
                }
                cmd.CommandText = $"CREATE TABLE [dbo].[{TableName}] ({columnDefine})";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"[{DateTime.Now}] Tạo bảng [dbo].[{TableName}] hoàn tất.");
            }
        }

        protected readonly string TableName;

        public string GetTableName() => TableName;

        public virtual T Create(T entity)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            var pros = entityType.GetProperties();
            var parameters = new List<SqlParameter>(pros.Length);
            var cmdIdentity = _connection.CreateCommand();
            var idType = typeof(T).GetProperty("Id").PropertyType;
            if (idType == typeof(long) || idType == typeof(int))
            {
                pros = pros.Where(pro => pro.Name.ToLower() != "id").ToArray();
            }
            var columns = "";
            var parameterNames = "";
            if (entityType.GetInterface(nameof(IAuditedEntity)) != null)
            {
                entityType.GetMethod(nameof(IAuditedEntity.CreateTime)).Invoke(entity, null);
                pros = pros.Where(pro => pro.Name.ToLower() != "id").ToArray();
            }
            foreach (var prop in pros)
            {
                var value = prop.GetValue(entity);
                if (value != null)
                {
                    columns += $"[{prop.Name}], ";
                    parameterNames += $"@{prop.Name}, ";
                    parameters.Add(new SqlParameter("@" + prop.Name, value));
                }
            }
            columns = columns.Trim(',', ' ');
            parameterNames = parameterNames.Trim(',', ' ');
            cmd.CommandText = $"INSERT INTO [dbo].[{TableName}] ({columns}) VALUES ({parameterNames}) SELECT IDENT_CURRENT('{TableName}')";
            cmd.Parameters.AddRange(parameters.ToArray());
            var id = cmd.ExecuteScalar();
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                entityType.GetProperty("Id").SetValue(entity, long.Parse(id.ToString()));
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
            reader.Close();
            return obj;
        }

        public virtual PagedAndSortedResultDto<T> Pagination(PageRequestDto request, params DbQueryParameterGroup[] dbQueryParameterGroups)
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
            var whereBy = "";
            if (dbQueryParameterGroups.Length > 0)
            {
                whereBy = "WHERE";
                for (int index = 0; index < dbQueryParameterGroups.Length; index++)
                {
                    var group = dbQueryParameterGroups[index];
                    whereBy += " (";
                    var dbQueryParameters = group.dbQueryParameters;
                    for (int i = 0; i < dbQueryParameters.Count; i++)
                    {
                        var parameter = dbQueryParameters[i];
                        whereBy += $"{parameter.Name} {parameter.GetCompareOperator()} @{parameter.Name + i} ";
                        if (i < dbQueryParameters.Count - 1)
                        {
                            whereBy += $"{parameter.GetLogicOperator()} ";
                        }
                        try
                        {
                            cmd.Parameters.AddWithValue("@" + parameter.Name + i, parameter.Value);
                        }
                        catch
                        { }
                    }
                    whereBy += ") ";
                    if (index < dbQueryParameterGroups.Length - 1)
                    {
                        whereBy += $"{group.LogicOperator} ";
                    }

                }
            }
            var properties = entityType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public);
            if (string.IsNullOrWhiteSpace(request.Column))
            {
                request.Column = "Id";
            }
            cmd.CommandText = $"SELECT * FROM [dbo].[{TableName}] {searchBy} {whereBy} ORDER BY {request.Column} {request.Sort} " +
                $"OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY";
            cmd.Parameters.AddWithValue("@RowsOfPage", request.Limit);
            cmd.Parameters.AddWithValue("@PageNumber", request.Page);
            cmdCount.CommandText = $"SELECT COUNT(*) FROM [dbo].[{TableName}] {searchBy}";
            var count = (int)cmdCount.ExecuteScalar();
            var reader = cmd.ExecuteReader();
            PagedAndSortedResultDto<T> result = new()
            {
                CurrentPage = request.Page,
                PerPage = request.Limit,
                TotalRecords = count,
                TotalPages = count / request.Limit + (count % request.Limit == 0 ? 0 : 1),
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
            foreach (var prop in pros.Where(p => p.Name != nameof(IAuditedEntity.Created_At)))
            {
                var value = prop.GetValue(entity);
                if (prop.Name.ToLower() != "id" && prop.Name.ToLower() != "createdat" && prop.Name.ToLower() != "createdby")
                {
                    if (value != null)
                    {
                        sets += $"[{prop.Name}] = @{prop.Name},";
                        cmd.Parameters.AddWithValue("@" + prop.Name, value);
                    }
                    else
                    {
                        sets += $"{prop.Name} = NULL,";
                    }
                }
            }
            sets = sets.Trim(',');
            cmd.CommandText = $"UPDATE [dbo].[{TableName}] SET {sets} WHERE [ID] = @id";
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public virtual IList<T> GetList(int? Count = null, params DbQueryParameterGroup[] dbQueryParameterGroups)
        {
            var cmd = _connection.CreateCommand();
            var top = "";
            if (Count.HasValue && Count.Value > 0)
            {
                top = "TOP " + Count;
            }
            var whereBy = "";
            if (dbQueryParameterGroups.Length > 0)
            {
                whereBy = "WHERE";
                for (int index = 0; index < dbQueryParameterGroups.Length; index++)
                {
                    var group = dbQueryParameterGroups[index];
                    whereBy += " (";
                    var dbQueryParameters = group.dbQueryParameters;
                    for (int i = 0; i < dbQueryParameters.Count; i++)
                    {
                        var parameter = dbQueryParameters[i];
                        whereBy += $"{parameter.Name} {parameter.GetCompareOperator()} @{parameter.Name + i} ";
                        if (i < dbQueryParameters.Count - 1)
                        {
                            whereBy += $"{parameter.GetLogicOperator()} ";
                        }

                        cmd.Parameters.AddWithValue("@" + parameter.Name + i, parameter.Value);
                    }
                    whereBy += ") ";
                    if (index < dbQueryParameterGroups.Length - 1)
                    {
                        whereBy += $"{group.LogicOperator} ";
                    }

                }
            }
            cmd.CommandText = $"SELECT {top} * FROM [dbo].[{TableName}] " + whereBy;
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

        public void AddConstraints()
        {
            var properties = entityType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public);
            var cmd = _connection.CreateCommand();
            Console.WriteLine($"[{DateTime.Now}] Tạo ràng buộc trên bảng [dbo].[{TableName}]");
            foreach (var pro in properties)
            {
                var foreignKeyAttribute = pro.GetCustomAttributes(typeof(ForeignKeyAttribute), true).Cast<ForeignKeyAttribute>().FirstOrDefault();
                if (foreignKeyAttribute != null)
                {
                    var foreignTableName = foreignKeyAttribute.GetTableName();
                    Console.WriteLine($"[{DateTime.Now}] Tạo ràng buộc [FK_{TableName}_{foreignTableName}]");
                    cmd.CommandText = $"ALTER TABLE [dbo].[{TableName}] " +
                        $"ADD CONSTRAINT [FK_{TableName}_{foreignTableName}] FOREIGN KEY([{pro.Name}]) " +
                        $"REFERENCES[dbo].[{foreignTableName}]([Id]) " +
                        $"ON UPDATE NO ACTION ON DELETE {(pro.PropertyType.Name.Contains(nameof(Nullable)) ? "SET NULL" : "CASCADE")}";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"[{DateTime.Now}] Tạo ràng buộc [FK_{TableName}_{foreignTableName}] hoàn tất.");

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine($"[{DateTime.Now}] Thêm ràng buộc thất bại [FK_{TableName}_{foreignTableName}]");
                        Console.WriteLine($"[{DateTime.Now}] {ex.Message}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            }
        }
    }
}
