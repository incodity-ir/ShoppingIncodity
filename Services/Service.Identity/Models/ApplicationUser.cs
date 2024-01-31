namespace Service.Identity.Models
{
    public class ApplicationUser:IdentityUser
    {
        //? Other properties
        public string FirstName { get; set; }   
        public string LastName { get; set; }
    }
}