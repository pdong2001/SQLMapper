using Library.Common.Interfaces;

namespace Library.DataModels
{
    public class Blob : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string File_Path { get; set; }
    }
}