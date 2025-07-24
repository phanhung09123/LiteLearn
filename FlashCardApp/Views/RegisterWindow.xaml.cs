using System.Linq;
using System.Windows;
using FlashCardApp.Data;
using FlashCardApp.Models;

namespace FlashCardApp.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly LiteLearnContext _context;

        public RegisterWindow()
        {
            InitializeComponent();
            _context = new LiteLearnContext();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            string rePassword = RePasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rePassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != rePassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                MessageBox.Show("Username already exists.");
                return;
            }

            _context.Users.Add(new User
            {
                Username = username,
                Password = password,
                Role = "User" // mặc định
            });

            _context.SaveChanges();

            MessageBox.Show("Registration successful!");
            this.Close(); // quay lại LoginWindow
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng cửa sổ đăng ký và trở về cửa sổ đăng nhập
        }
    }
}
