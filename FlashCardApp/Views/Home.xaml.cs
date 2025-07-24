using System.Windows;
using FlashCardApp.Data;
using FlashCardApp.Models;
using System.Linq;

namespace FlashCardApp.Views
{
    public partial class Home : Window
    {
        private User _currentUser;

        private readonly LiteLearnContext _context;

        public Home()
        {
            InitializeComponent();
            _context = new LiteLearnContext();
            LoadStats();
        }
        public Home(User user) : this()
        {
            _currentUser = user;
            WelcomeText.Text = $"Welcome back, {_currentUser.Username}!";
            // Nếu muốn hiển thị tên hoặc role thì có thể làm ở đây
            // Ví dụ:
            // WelcomeTextBlock.Text = $"Hello, {user.Username}!";
        }

        private void LoadStats()
        {
            var topicCount = _context.Topics.Count();
            var flashcardCount = _context.Flashcards.Count();
            var noteCount = _context.Notes.Count();

            TopicCountText.Text = $"Topics: {topicCount}";
            FlashcardCountText.Text = $"Flashcards: {flashcardCount}";
            NoteCountText.Text = $"Notes: {noteCount}";
        }

        private void StartLearning_Click(object sender, RoutedEventArgs e)
        {
            var chooseWindow = new ChooseTopicWindow();
            if (chooseWindow.ShowDialog() == true && chooseWindow.SelectedTopic != null)
            {
                var context = new LiteLearnContext();
                var flashcards = context.Flashcards
                    .Where(f => f.TopicId == chooseWindow.SelectedTopic.TopicId)
                    .ToList();

                if (flashcards.Count == 0)
                {
                    MessageBox.Show("This topic doesn't have any flashcards yet!", "Empty Topic", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var reviewWindow = new FlashcardReview(flashcards, chooseWindow.SelectedTopic.Title);
                reviewWindow.ShowDialog();
            }
        }



        private void ManageTopics_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var topicManager = new TopicManager();
            topicManager.ShowDialog();
            this.Show();
        }


        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
