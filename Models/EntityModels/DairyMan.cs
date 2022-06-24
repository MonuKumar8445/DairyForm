using Microsoft.AspNetCore.Identity;

namespace Models.EntityModels
{
    public class DairyMan : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Age { get; set; }
        public string? Address { get; set; }
        public DateTime Create_Date { get; set; }
        public string? Create_By { get; set; }
        public string? Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        //public string Subject { get; set; }
        //public string Body { get; set; }

        //public string? ConfirmPassword { get; set; }
        // public string? Id { get; set; }
        //public object Password { get; set; }
    }
}
