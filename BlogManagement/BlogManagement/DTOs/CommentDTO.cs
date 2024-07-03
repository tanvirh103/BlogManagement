using BlogManagement.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogManagement.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        [Required]
        [StringLength(100,MinimumLength =5)]
        public string CommentDetails { get; set; }
        public System.DateTime CommentTime { get; set; }
        public int CommentUser { get; set; }
        public int PostId { get; set; }

        public string UserName { get; set; }
    }
}