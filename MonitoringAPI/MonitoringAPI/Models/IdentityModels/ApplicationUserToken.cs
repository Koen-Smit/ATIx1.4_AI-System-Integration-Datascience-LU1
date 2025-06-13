using Microsoft.AspNetCore.Identity;

public class ApplicationUserToken : IdentityUserToken<int>
{
    public virtual ApplicationUser? User { get; set; }
}
