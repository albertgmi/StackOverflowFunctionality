using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
namespace StackOverflowQuestionFunctionality.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Relation - one question, many answers
        public List<Answer> Answers { get; set; } = new List<Answer>();
        
        // Relation - one question, many comments
        public List<Comment> Comments { get; set; } = new List<Comment>();

        // Relation - one user, many questions
        public User User { get; set; }
        public Guid UserId { get; set; }

        // Relation - many questions, many tags
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
