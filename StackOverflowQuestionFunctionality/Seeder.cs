using StackOverflowQuestionFunctionality.Entities;
using Bogus;

namespace StackOverflowQuestionFunctionality
{
    public class Seeder : IDataSeeder
    {
        private readonly Faker faker = new Faker();
        public void Seed(StackOverflowDbContext context)
        {
            if (context.Users.Any())
                return;
            var users = GetUsers();
            var questions = GetQuestions(users);
            var answers = GetAnswers(questions, users);
            var comments = GetComments(answers,questions,users);
            var tags = GetTags();
            AssignTagsToQuestions(questions,tags);

            context.Users.AddRange(users);
            context.Questions.AddRange(questions);
            context.Answers.AddRange(answers);
            context.Comments.AddRange(comments);
            context.Tags.AddRange(tags);

            context.SaveChanges();
        }
        public List<User> GetUsers() 
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
            return users;
        }
        public List<Answer> GetAnswers(List<Question> questions, List<User> users)
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
            return answers;
        }
        public List<Comment> GetComments(List<Answer> answers, List<Question> questions, List<User> users)
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
            return comments;
        }
        public List<Question> GetQuestions(List<User> users)
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
            return questions;
        }
        public List<Tag> GetTags()
        {
            var tags = new List<Tag>();
            for (int i = 0; i < 50; i++)
            {
                tags.Add(new Tag
                {
                    Name = faker.Lorem.Word()
                });
            }
            return tags;
        }
        public void AssignTagsToQuestions(List<Question> questions, List<Tag> tags)
        {
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
        }
    }
}
