namespace Domain.Entities
{

    public class ChatGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string EmployeeId { get; set; }
    }
}
