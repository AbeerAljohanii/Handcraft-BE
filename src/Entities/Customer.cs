namespace Backend_Teamwork.src.Entities
{
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public byte[]? Salt { get; set; }
    }
}
