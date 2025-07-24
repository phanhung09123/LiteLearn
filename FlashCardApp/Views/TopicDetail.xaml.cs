using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FlashCardApp.Data;
using FlashCardApp.Models;

namespace FlashCardApp.Views
{
    public partial class TopicDetail : Window
    {
        private readonly LiteLearnContext _context;
        private readonly Topic _topic;
        private Note _note;
        private List<Flashcard> _allFlashcards;

        public TopicDetail(Topic topic)
        {
            InitializeComponent();
            _context = new LiteLearnContext();
            _topic = topic;
            TitleText.Text = $"Topic: {topic.Title}";

            LoadFlashcards();
            LoadNote();
        }

        private void LoadFlashcards()
        {
            _allFlashcards = _context.Flashcards
                .Where(f => f.TopicId == _topic.TopicId)
                .OrderBy(f => f.FlashcardId)
                .ToList();

            FlashcardListBox.ItemsSource = _allFlashcards;

            LoadTagsForFilter();
        }

        private void LoadTagsForFilter()
        {
            var tags = _allFlashcards
                .SelectMany(f => (f.Tags ?? "").Split(',', System.StringSplitOptions.RemoveEmptyEntries))
                .Select(t => t.Trim())
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            tags.Insert(0, "All");
            TagFilterBox.ItemsSource = tags;
            TagFilterBox.SelectedIndex = 0;
        }

        private void TagFilterBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TagFilterBox.SelectedItem is string selectedTag)
            {
                if (selectedTag == "All")
                {
                    FlashcardListBox.ItemsSource = _allFlashcards;
                }
                else
                {
                    FlashcardListBox.ItemsSource = _allFlashcards
                        .Where(f => (f.Tags ?? "").Split(',', System.StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim())
                        .Contains(selectedTag))
                        .ToList();
                }
            }
        }

        private void LoadNote()
        {
            _note = _context.Notes.FirstOrDefault(n => n.TopicId == _topic.TopicId);
            if (_note != null)
                NoteBox.Text = _note.Content;
        }

        private void AddFlashcard_Click(object sender, RoutedEventArgs e)
        {
            var q = QuestionBox.Text.Trim();
            var a = AnswerBox.Text.Trim();
            if (string.IsNullOrEmpty(q) || string.IsNullOrEmpty(a)) return;

            _context.Flashcards.Add(new Flashcard
            {
                Question = q,
                Answer = a,
                Tags = TagsBox.Text.Trim(),
                TopicId = _topic.TopicId
            });
            _context.SaveChanges();
            LoadFlashcards();
            ClearInputs();
        }

        private void UpdateFlashcard_Click(object sender, RoutedEventArgs e)
        {
            if (FlashcardListBox.SelectedItem is Flashcard card)
            {
                card.Question = QuestionBox.Text.Trim();
                card.Answer = AnswerBox.Text.Trim();
                card.Tags = TagsBox.Text.Trim();
                _context.SaveChanges();
                LoadFlashcards();
            }
        }

        private void DeleteFlashcard_Click(object sender, RoutedEventArgs e)
        {
            if (FlashcardListBox.SelectedItem is Flashcard card)
            {
                _context.Flashcards.Remove(card);
                _context.SaveChanges();
                LoadFlashcards();
                ClearInputs();
            }
        }

        private void FlashcardListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FlashcardListBox.SelectedItem is Flashcard card)
            {
                QuestionBox.Text = card.Question;
                AnswerBox.Text = card.Answer;
                TagsBox.Text = card.Tags;
            }
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            if (_note == null)
            {
                _note = new Note
                {
                    TopicId = _topic.TopicId,
                    Content = NoteBox.Text
                };
                _context.Notes.Add(_note);
            }
            else
            {
                _note.Content = NoteBox.Text;
            }
            _context.SaveChanges();
            MessageBox.Show("Note saved!");
        }

        private void ClearInputs()
        {
            QuestionBox.Text = "";
            AnswerBox.Text = "";
            TagsBox.Text = "";
        }
    }
}
