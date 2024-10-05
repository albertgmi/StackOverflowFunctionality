using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
namespace StackOverflowQuestionFunctionality.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Relation - one question, many answers
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }

        // Relation - one answer, many comments
        public List<Comment> Comments { get; set; } = new List<Comment>();

        // Relation - one user, many answers
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
