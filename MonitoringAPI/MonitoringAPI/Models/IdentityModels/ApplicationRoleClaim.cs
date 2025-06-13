using Microsoft.AspNetCore.Identity;

public class ApplicationRoleClaim : IdentityRoleClaim<int>
{
    public virtual ApplicationRole? Role { get; set; }
}
