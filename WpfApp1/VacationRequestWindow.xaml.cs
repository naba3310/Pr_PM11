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
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для VacationRequestWindow.xaml
    /// </summary>
    public partial class VacationRequestWindow : Window
    {
        private string _connectionString;
        private int _currentEmployeeId;

        public VacationRequestWindow(string connectionString, string username)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _currentEmployeeId = GetEmployeeIdByUsername(username);
        }
        private int GetEmployeeIdByUsername(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT employee_id FROM users WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        var result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int empId))
                            return empId;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения ID сотрудника: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return -1;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentEmployeeId != -1 && StartDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null)
            {
                DateTime startDate = StartDatePicker.SelectedDate.Value;
                DateTime endDate = EndDatePicker.SelectedDate.Value;

                if (endDate < startDate)
                {
                    MessageBox.Show("Дата окончания не может быть раньше даты начала.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = @"
                            INSERT INTO vacation_requests (request_id, employee_id, start_date, end_date, status, created_at) 
                            VALUES ((SELECT ISNULL(MAX(request_id),0) + 1 FROM vacation_requests), @employeeId, @startDate, @endDate, 'pending', GETDATE())
                        ";

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@employeeId", _currentEmployeeId);
                            cmd.Parameters.AddWithValue("@startDate", startDate);
                            cmd.Parameters.AddWithValue("@endDate", endDate);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Заявка успешно создана и отправлена на рассмотрение.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Не удалось создать заявку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении заявки: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните корректно все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
