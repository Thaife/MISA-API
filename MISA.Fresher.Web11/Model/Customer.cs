namespace MISA.Fresher.Web11.Model
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
