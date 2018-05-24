namespace HotelsApp.Core.DataModels
{
    public class Customer
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneContact { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName} [{Email}]";
        }
    }
}