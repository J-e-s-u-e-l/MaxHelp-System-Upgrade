using Microsoft.AspNetCore.Identity;

namespace MaxHelp_System_Upgrade.Models
{
    public class User : IdentityUser
    {
        public int BusinessUnitId { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
    }
}
