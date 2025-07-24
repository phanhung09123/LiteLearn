using FlashCardApp.Data;
using FlashCardApp.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FlashCardApp.Views
{
    public partial class TopicManager : Window
    {
        private readonly LiteLearnContext _context;

        public TopicManager()
        {
            InitializeComponent();
            _context = new LiteLearnContext();
            LoadTopics();
        }

        private void LoadTopics()
        {
            TopicListBox.ItemsSource = _context.Topics.ToList();
            TopicListBox.DisplayMemberPath = "Title";
        }

        private void AddTopic_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddTopic();
            if (dialog.ShowDialog() == true)
            {
                _context.Topics.Add(new Topic { Title = dialog.TopicTitle });
                _context.SaveChanges();
                LoadTopics();
            }
        }

        private void TopicListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TopicListBox.SelectedItem is Topic selected)
            {
                var detailWindow = new TopicDetail(selected); // Mở giao diện chi tiết
                detailWindow.ShowDialog(); // Chờ đóng
                LoadTopics(); // Load lại danh sách nếu có thay đổi
            }

            // Optionally: reset selection để click lại cùng topic lần nữa
            TopicListBox.SelectedItem = null;
        }

    }
}
