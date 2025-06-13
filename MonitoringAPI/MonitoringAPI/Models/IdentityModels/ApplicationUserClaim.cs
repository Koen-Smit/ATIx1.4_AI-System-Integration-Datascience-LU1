using Microsoft.AspNetCore.Identity;

public class ApplicationUserClaim : IdentityUserClaim<int>
{
    public virtual ApplicationUser? User { get; set; }
}
