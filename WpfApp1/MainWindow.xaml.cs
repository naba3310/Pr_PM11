using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=LAPTOP-VIIPDO76\\MSSQLSERVERR;Initial Catalog=KadryAdministratsi;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var userData = AuthenticateUser(username, password);
                if (userData != null)
                {
                    // userData: Tuple<int employeeId, int accessLevel>
                    int employeeId = userData.Item1;
                    int accessLevel = userData.Item2;

                    EmployeeListWindow empListWindow = new EmployeeListWindow(connectionString, username, employeeId, accessLevel);
                    empListWindow.Show();
                    this.Close(); // Закрываем окно авторизации
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при попытке авторизации:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Tuple<int, int> AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT u.employee_id, p.access_level
                    FROM users u
                    JOIN employees e ON u.employee_id = e.employee_id
                    JOIN positions p ON e.position_id = p.position_id
                    WHERE u.username = @username AND u.password = @password;
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int employeeId = reader.GetInt32(0);
                            int accessLevel = reader.GetByte(1);  // access_level — tinyint
                            return new Tuple<int, int>(employeeId, accessLevel);
                        }
                    }
                }
            }

            return null;
        }
    }
}
