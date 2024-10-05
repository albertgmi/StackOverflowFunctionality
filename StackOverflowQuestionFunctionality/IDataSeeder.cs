using StackOverflowQuestionFunctionality.Entities;

namespace StackOverflowQuestionFunctionality
{
    public interface IDataSeeder
    {
        public void Seed(StackOverflowDbContext context);
    }
}
