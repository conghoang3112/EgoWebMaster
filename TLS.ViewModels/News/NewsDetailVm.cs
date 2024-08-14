using System;
using System.Collections.Generic;
using System.Text;

namespace TLS.ViewModels.News
{
    public class NewsDetailVm
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Content { get; set; }

        public string MetaKeyWord { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string Author { get; set; }
        public string ThumnailImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DetailLink { get; set; }
        public string[] Tags { get; set; }
    }
}