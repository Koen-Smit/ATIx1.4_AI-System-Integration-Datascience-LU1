using Microsoft.AspNetCore.Identity;
public class ApplicationUser : IdentityUser<int>
{
    public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
}
