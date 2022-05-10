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
    }
    public abstract class AuditedEntity<TKey> : Entity<TKey>, IAuditedEntity, HasKey<TKey>, HasKey where TKey : struct
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void CreateTime()
        {
            this.CreatedAt = DateTime.Now;
        }

        public void UpdateTime()
        {
            this.UpdatedAt = DateTime.Now;
        }
    }
}
