using Microsoft.AspNetCore.Identity;

namespace Auth_Cls.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string CellPhone { get; set; }
        public string Country { get; set; }
    }
}
