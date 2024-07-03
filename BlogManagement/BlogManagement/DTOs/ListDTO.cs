using BlogManagement.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogManagement.DTOs
{
    public class ListDTO
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostBody { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }
    }
}