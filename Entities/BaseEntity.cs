using System;
namespace OganiProject.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Status = DataStatus.Inserted;
            CreatedDate = DateTime.Now;
            ModifatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifatedDate { get; set; }
        public DataStatus Status { get; set; }

    }

    public enum DataStatus
    {
        Inserted=0,
        Updated=1,
        Deleted=2
    }
}
