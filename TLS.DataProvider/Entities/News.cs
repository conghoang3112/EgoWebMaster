using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums;

namespace TLS.DataProvider.Entities
{
    public class News
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Content { get; set; }

        public string ImageFileName { get; set; }
        public string ImageTitle { get; set; }

        public string MetaKeyWord { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }
        public bool IsPublish { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Tags { get; set; }
    }
}
