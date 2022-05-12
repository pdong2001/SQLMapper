using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Interfaces
{
    public interface HasKey
    {
        public static string TableName { get; }
        object GetKey();
    }

    public interface HasKey<TKey> : HasKey where TKey : struct
    {
        public TKey Id { get; set; }
    }

    public abstract class Entity<TKey> : HasKey<TKey> where TKey : struct
    {
        public TKey Id { get; set; }

        public object GetKey()
        {
            return Id;
        }
    }
}
