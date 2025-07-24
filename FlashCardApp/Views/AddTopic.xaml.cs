using System.Windows;

namespace FlashCardApp.Views
{
    public partial class AddTopic : Window
    {
        public string TopicTitle { get; private set; }

        public AddTopic()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                TopicTitle = TitleBox.Text.Trim();
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a topic title.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
