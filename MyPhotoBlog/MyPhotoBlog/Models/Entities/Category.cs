using System.Collections.Generic;

namespace MyPhotoBlog.Models.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}