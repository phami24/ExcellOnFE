namespace Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string GroupId { get; set; }
        //Take in Identity Table
        public string SenderId { get; set; }
        //Take in Identity Table
        public string ReceiveId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
