using Microsoft.AspNetCore.Identity;

public class ApplicationUserLogin : IdentityUserLogin<int>
{
    public virtual ApplicationUser? User { get; set; }
}

