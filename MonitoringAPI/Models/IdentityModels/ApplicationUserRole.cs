﻿using Microsoft.AspNetCore.Identity;

public class ApplicationUserRole : IdentityUserRole<int>
{
    public virtual ApplicationUser? User { get; set; }
    public virtual ApplicationRole? Role { get; set; }

}
