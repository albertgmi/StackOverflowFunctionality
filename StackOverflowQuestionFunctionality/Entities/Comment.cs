using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
namespace StackOverflowQuestionFunctionality.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content {  get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set;}

        // Relation - one answer, many comments
        public Answer Answer { get; set; }
        public Guid AnswerId { get; set; }
        
        // Relation - one question, many comments
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }

        // Relation - one user, many comments
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
