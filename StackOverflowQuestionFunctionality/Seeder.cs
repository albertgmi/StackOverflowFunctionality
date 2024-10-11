using StackOverflowQuestionFunctionality.Entities;
using Bogus;
using System.Xml.Linq;

namespace StackOverflowQuestionFunctionality
{
    public class Seeder
    {
        private readonly Faker faker = new Faker();
        public void SeedAll(StackOverflowDbContext context)
        {
            bool usersInDataBase = context.Users.Any();
            if (!usersInDataBase)
            {
                var users = CreateUsersInDatabase(context);
                var questions = CreateQuestionsInDatabase(context, users);
                var answers = CreateAnswersInDatabase(context, questions, users);
                var comments = CreateCommentsInDatabase(context, answers, questions, users);
                CreateTagsAndAssignThemToQuestions(context, questions);
            }
        }
        public List<User> CreateUsersInDatabase(StackOverflowDbContext context) 
        { 
            var users = new List<User>();
            for (int i = 0; i < 100; i++)
            {
                // New faker object for different full names and emails every iteration
                var faker2 = new Faker();
                users.Add(new User
                {
                    Id = faker.Random.Guid(),
                    FullName = faker2.Person.FullName,
                    Email = faker2.Person.Email,
                    City = faker.Address.City(),
                    Street = faker.Address.StreetAddress()
                });
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
            return users;
        }
        public List<Question> CreateQuestionsInDatabase(StackOverflowDbContext context, List<User> users)
        {
            var questions = new List<Question>();
            for (int i = 0; i < 100; i++)
            {
                var edited = faker.Random.Bool();
                var randomDate = faker.Date.Between(new DateTime(2020, 1, 1), DateTime.Now);
                var randomDate2 = faker.Date.Between(new DateTime(2023, 1, 1), DateTime.Now);
                questions.Add(new Question
                {
                    Id = faker.Random.Guid(),
                    Title = faker.Lorem.Sentence(6, 3),
                    Content = faker.Lorem.Sentence(5, 4),
                    CreationDate = randomDate,
                    UpdatedDate = edited ? randomDate2 : randomDate,
                    UserId = users[faker.Random.Int(0, users.Count - 1)].Id
                });
            }
            if (!context.Questions.Any())
            {
                context.Questions.AddRange(questions);
                context.SaveChanges();
            }
            return questions;
        }
        public List<Answer> CreateAnswersInDatabase(StackOverflowDbContext context, List<Question> questions, List<User> users)
        {
            var answers = new List<Answer>();

            for (int i = 0; i < 100; i++)
            {
                var edited = faker.Random.Bool();
                var randomDate = faker.Date.Between(new DateTime(2020, 1, 1), DateTime.Now);
                var randomDate2 = faker.Date.Between(new DateTime(2023, 1, 1), DateTime.Now);
                answers.Add(new Answer
                {
                    Id = faker.Random.Guid(),
                    Title = faker.Lorem.Sentence(6, 3),
                    Content = faker.Lorem.Sentence(5,4),
                    CreationDate = randomDate,
                    UpdatedDate = edited ? randomDate2 : randomDate,
                    QuestionId = questions[faker.Random.Int(0, questions.Count - 1)].Id,
                    UserId = users[faker.Random.Int(0,users.Count - 1)].Id
                }); 
            }
            if (!context.Answers.Any())
            {
                context.Answers.AddRange(answers);
                context.SaveChanges();
            }
            return answers;
        }
        public List<Comment> CreateCommentsInDatabase(StackOverflowDbContext context, List<Answer> answers, List<Question> questions, List<User> users)
        {
            var comments = new List<Comment>();

            for (int i = 0; i < 100; i++)
            {
                var edited = faker.Random.Bool();
                var randomDate = faker.Date.Between(new DateTime(2020, 1, 1), DateTime.Now);
                var randomDate2 = faker.Date.Between(new DateTime(2023, 1, 1), DateTime.Now);

                comments.Add(new Comment
                {
                    Id = faker.Random.Guid(),
                    Content = faker.Lorem.Sentence(6,3),
                    CreationDate = randomDate,
                    UpdatedDate = edited ? randomDate2 : randomDate,
                    AnswerId = answers[faker.Random.Int(0, answers.Count - 1)].Id,
                    QuestionId = questions[faker.Random.Int(0, questions.Count - 1)].Id,
                    UserId = users[faker.Random.Int(0, users.Count - 1)].Id
                });
            }
            if (!context.Comments.Any())
            {
                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
            return comments;
        }
        public void CreateTagsAndAssignThemToQuestions(StackOverflowDbContext context, List<Question> questions)
        {
            var tags = new List<Tag>();
            for (int i = 0; i < 50; i++)
            {
                tags.Add(new Tag
                {
                    Name = faker.Lorem.Word()
                });
            }
            if (!context.Tags.Any())
            {
                context.Tags.AddRange(tags);
                context.SaveChanges();
            }
            foreach (var question in questions)
            {
                var tagsToAssign = new List<Tag>();
                for (int i = 0; i < faker.Random.Int(1,5); i++)
                {
                    var newTag = tags[faker.Random.Int(0,tags.Count-1)];
                    tagsToAssign.Add(newTag);
                }
                question.Tags = tagsToAssign;
            }
            context.SaveChanges();
        }
    }
}
