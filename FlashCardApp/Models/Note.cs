using System;
using System.Collections.Generic;

namespace FlashCardApp.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public string Content { get; set; } = null!;

    public int? TopicId { get; set; }

    public virtual Topic? Topic { get; set; }
}
