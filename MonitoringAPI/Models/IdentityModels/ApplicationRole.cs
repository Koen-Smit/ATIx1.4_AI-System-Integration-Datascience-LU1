using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
}
