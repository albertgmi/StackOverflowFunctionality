using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
namespace StackOverflowQuestionFunctionality.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Relation - many questions, many tags
        public List<Question> Questions = new List<Question>();
    }
}
