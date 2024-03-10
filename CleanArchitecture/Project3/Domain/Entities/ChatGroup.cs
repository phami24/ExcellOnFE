namespace Domain.Entities
{

    public class ChatGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
    }
}
