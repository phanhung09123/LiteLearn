using System;
using System.Collections.Generic;

namespace FlashCardApp.Models;

public partial class Flashcard
{
    public int FlashcardId { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public string? Tags { get; set; }

    public int? TopicId { get; set; }

    public virtual Topic? Topic { get; set; }
}
