using FlashCardApp.Data;
using FlashCardApp.Models;
using FlashCardApp.Views;
using System.Linq;
using System.Windows;

namespace FlashCardApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LiteLearnContext _context;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new LiteLearnContext();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            var user = _context.Users.FirstOrDefault(u =>
                u.Username == username && u.Password == password);

            if (user != null)
            {
                MessageBox.Show($"Welcome {user.Username} ({user.Role})!");

                // Truyền user vào HomeWindow (nếu bạn đã sửa constructor)
                var home = new Home(user);
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); 
            var register = new RegisterWindow();
            register.ShowDialog(); 
            this.Show();
        }

    }
}
