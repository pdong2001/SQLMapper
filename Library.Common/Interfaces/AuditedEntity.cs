using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Interfaces
{
    public interface IAuditedEntity
    {
        void CreateTime();
        void UpdateTime();
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }


    public abstract class AuditedEntity<TKey> : Entity<TKey>, IAuditedEntity, HasKey<TKey>, HasKey where TKey : struct
    {
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        public void CreateTime()
        {
            this.Created_At = DateTime.Now;
        }

        public void UpdateTime()
        {
            this.Updated_At = DateTime.Now;
        }
    }
}
