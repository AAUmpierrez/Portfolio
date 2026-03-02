using BussinesLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public class UserComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public bool IsInternal { get; set; }
        public DateTime CreatedAt { get; set; }

        private UserComment() { }
        public UserComment(int userId, string content, bool isInternal)
        {
            UserId = userId;
            Content = content;
            IsInternal = isInternal;
            CreatedAt = DateTime.Now;
            Validate();
        }

        private void Validate()
        {
            if (UserId <= 0) throw new CommentException("Error. Incorrect user");
            if (string.IsNullOrEmpty(Content)) throw new CommentException("Error. Comment must have content");
        }


    }
}
