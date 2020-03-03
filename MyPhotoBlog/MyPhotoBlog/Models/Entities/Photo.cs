using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPhotoBlog.Models.Entities
{
    public class Photo:BaseEntity
    {
        // başlık,açıklama,yolu,kategori,thumb,likehit,

        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set;}
        public byte[] Thumb { get; set; }
        public int LikeHit { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}