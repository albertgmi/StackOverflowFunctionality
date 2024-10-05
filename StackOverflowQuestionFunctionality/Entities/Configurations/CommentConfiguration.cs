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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> eb)
        {
            eb.HasOne(c => c.Question)
                .WithMany(q => q.Comments)
                .HasForeignKey(fk => fk.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            eb.HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(fk => fk.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
