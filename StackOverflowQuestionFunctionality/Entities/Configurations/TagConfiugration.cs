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
    public class TagConfiugration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
                builder.HasMany(t => t.Questions)
                  .WithMany(q => q.Tags)
                  .UsingEntity<QuestionTag>(
                qtBuilder => qtBuilder
                .HasOne(qt => qt.Question)
                .WithMany()
                .HasForeignKey(qt => qt.QuestionId),

                qtBuilder => qtBuilder
                .HasOne(qt => qt.Tag)
                .WithMany()
                .HasForeignKey(qt => qt.TagId),

                qtBuilder =>
                {
                    qtBuilder.HasKey(qt => new { qt.QuestionId, qt.TagId });
                });
        }
    }
}
