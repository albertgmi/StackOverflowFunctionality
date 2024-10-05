using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowQuestionFunctionality.Entities.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> eb)
        {
            eb.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(fk => fk.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            eb.HasMany(a => a.Comments)
            .WithOne(c => c.Answer)
            .HasForeignKey(fk => fk.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);

            eb.HasOne(a => a.User)
            .WithMany(u => u.Answers)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
