using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class BlogPost
    {
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public bool Published { get; set; }
    }

    public class Blog
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
    }
}
