using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FlashCardApp.Data;
using FlashCardApp.Models;

namespace FlashCardApp.Views
{
    public partial class ChooseTopicWindow : Window
    {

        private List<Topic> _allTopics;

        public Topic SelectedTopic { get; private set; }

        public ChooseTopicWindow()
        {
            InitializeComponent();
            LoadTopics();

            // Placeholder xử lý khi bắt đầu
            SearchBox.TextChanged += (_, __) =>
            {
                SearchPlaceholder.Visibility = string.IsNullOrWhiteSpace(SearchBox.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            };
        }


        private void LoadTopics()
        {
            using var context = new LiteLearnContext();
            var topics = context.Topics.OrderBy(t => t.Title).ToList();
            TopicListBox.ItemsSource = topics;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (TopicListBox.SelectedItem is Topic topic)
            {
                SelectedTopic = topic;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please select a topic.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_allTopics == null) return;

            string keyword = SearchBox.Text.Trim().ToLower();

            var filtered = _allTopics
                .Where(t => t.Title != null && t.Title.ToLower().Contains(keyword))
                .ToList();

            TopicListBox.ItemsSource = filtered;

            if (SearchPlaceholder != null)
            {
                SearchPlaceholder.Visibility = string.IsNullOrWhiteSpace(SearchBox.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }


    }
}
