using BlogManagement.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogManagement.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostBody { get; set; }
        public System.DateTime PostDate { get; set; }
        public int UserId { get; set; }
        public string PostStatus { get; set; }

        public virtual ICollection<CommentDTO> Comments { get; set; }

        public PostDTO() {
            Comments = new List<CommentDTO>();
        }

    }
}