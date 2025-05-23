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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для EmployeeListWindow.xaml
    /// </summary>
    public partial class EmployeeListWindow : Window
    {
        private string _connectionString;
        private string _currentUsername;
        private int _currentEmployeeId;
        private int _currentAccessLevel;

        private ObservableCollection<EmployeeViewModel> _allEmployees;
        private ObservableCollection<EmployeeViewModel> _filteredEmployees;

        public EmployeeListWindow(string connectionString, string username, int employeeId, int accessLevel)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _currentUsername = username;
            _currentEmployeeId = employeeId;
            _currentAccessLevel = accessLevel;
            LoadEmployees();
            ViewVacationRequestsButton.Visibility = accessLevel >= 2 ? Visibility.Visible : Visibility.Collapsed;
        }
        private void LoadEmployees()
        {
            try
            {
                _allEmployees = new ObservableCollection<EmployeeViewModel>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Подгружаем сотрудников
                    string query = @"
                        SELECT e.employee_id, e.full_name, p.title AS position_title, d.department_name,
                            e.phone, e.email, e.hire_date, e.salary
                        FROM employees e
                        LEFT JOIN positions p ON e.position_id = p.position_id
                        LEFT JOIN departments d ON e.department_id = d.department_id
                        ORDER BY e.full_name;
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var emp = new EmployeeViewModel
                            {
                                EmployeeId = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                PositionTitle = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                DepartmentName = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Phone = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                Email = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                HireDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                                Salary = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7)
                            };

                            // Настраиваем видимость колонок согласно уровню доступа
                            emp.SetVisibilityByAccessLevel(_currentAccessLevel);

                            _allEmployees.Add(emp);
                        }
                    }
                }

                _filteredEmployees = new ObservableCollection<EmployeeViewModel>(_allEmployees);
                EmployeesDataGrid.ItemsSource = _filteredEmployees;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки сотрудников: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = FilterTextBox.Text.ToLower();

            var filtered = _allEmployees.Where(emp => emp.FullName.ToLower().Contains(filter)).ToList();

            _filteredEmployees.Clear();
            foreach (var emp in filtered)
                _filteredEmployees.Add(emp);
        }

        private void OpenVacationRequestWindow_Click(object sender, RoutedEventArgs e)
        {
            var vacationWindow = new VacationRequestWindow(_connectionString, _currentUsername);
            vacationWindow.Owner = this;
            vacationWindow.ShowDialog();
        }

        private void ViewVacationRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно просмотра заявок на отпуск
            var requestsWindow = new VacationRequestsWindow(_connectionString, _currentUsername);
            requestsWindow.Owner = this;
            requestsWindow.ShowDialog();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

    // ViewModel с логикой видимости колонок в зависимости от accessLevel
    public class EmployeeViewModel : DependencyObject
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string PositionTitle { get; set; }
        public string DepartmentName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }

        public string HireDateString => HireDate?.ToString("yyyy-MM-dd") ?? "";
        public string SalaryString => Salary?.ToString("F2") ?? "";

        // Используем свойства DependencyProperty для видимости в DataGrid
        public Visibility PhoneVisibility { get; set; } = Visibility.Collapsed;
        public Visibility EmailVisibility { get; set; } = Visibility.Collapsed;
        public Visibility HireDateVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SalaryVisibility { get; set; } = Visibility.Collapsed;

        public void SetVisibilityByAccessLevel(int accessLevel)
        {
            // accessLevel 1: базовая инфа (ФИО, должность, отдел)
            // accessLevel 2: добавить телефон, email, дату приема
            // accessLevel 3: добавить еще и зарплату

            PhoneVisibility = (accessLevel >= 2) ? Visibility.Visible : Visibility.Collapsed;
            EmailVisibility = (accessLevel >= 2) ? Visibility.Visible : Visibility.Collapsed;
            HireDateVisibility = (accessLevel >= 2) ? Visibility.Visible : Visibility.Collapsed;
            SalaryVisibility = (accessLevel >= 3) ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
