using System;
using System.Collections.Generic;

namespace FlashCardApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Password { get; set; }


    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
