namespace StackOverflowQuestionFunctionality.Entities
{
    public class QuestionTag
    {
        // Connection entity between Questions and Tags for many to many relation
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        public Tag Tag { get; set; }
        public Guid TagId { get; set; }
    }
}
