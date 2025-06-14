using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole() : base() { }
    public ApplicationRole(string roleName) : base(roleName) { }

    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}