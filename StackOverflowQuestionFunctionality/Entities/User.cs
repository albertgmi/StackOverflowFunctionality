using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
namespace StackOverflowQuestionFunctionality.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        
        // Relation - one user many answers
        public List<Answer> Answers = new List<Answer>();

        // Relation - one user many comments
        public List<Comment> Comments = new List<Comment>();

        // Relation - one user many questions
        public List<Question> Questions = new List<Question>();
    }
}
