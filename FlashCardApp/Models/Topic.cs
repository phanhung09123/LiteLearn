using System;
using System.Collections.Generic;

namespace FlashCardApp.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    public string Title { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual User? User { get; set; }
}
