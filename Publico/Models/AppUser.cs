using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Publico.Models;

public class AppUser : IdentityUser
{
    public AppUser()
    {
        Messages = new HashSet<Message>();
    }

    // 1 - * (AppUser to Messages)
    public virtual ICollection<Message> Messages { get; set; }
}