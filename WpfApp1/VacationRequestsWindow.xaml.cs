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
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для VacationRequestsWindow.xaml
    /// </summary>
    public partial class VacationRequestsWindow : Window
    {
        private string _connectionString;
        private string _currentUsername;

        private ObservableCollection<VacationRequestViewModel> _requests;

        public VacationRequestsWindow(string connectionString, string username)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _currentUsername = username;

            LoadVacationRequests();
        }
        private void LoadVacationRequests()
        {
            try
            {
                _requests = new ObservableCollection<VacationRequestViewModel>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT vr.request_id, e.full_name, vr.start_date, vr.end_date, vr.status,
                               approver.full_name AS approved_by_name, vr.created_at
                        FROM vacation_requests vr
                        JOIN employees e ON vr.employee_id = e.employee_id
                        LEFT JOIN employees approver ON vr.approved_by = approver.employee_id
                        ORDER BY vr.created_at DESC;
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _requests.Add(new VacationRequestViewModel
                            {
                                RequestId = reader.GetInt32(0),
                                EmployeeFullName = reader.GetString(1),
                                StartDate = reader.GetDateTime(2),
                                EndDate = reader.GetDateTime(3),
                                Status = reader.GetString(4),
                                ApprovedByName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                CreatedAt = reader.GetDateTime(6)
                            });
                        }
                    }
                }

                RequestsDataGrid.ItemsSource = _requests;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заявок: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class VacationRequestViewModel
    {
        public int RequestId { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime CreatedAt { get; set; }

        public string StartDateString => StartDate.ToString("yyyy-MM-dd");
        public string EndDateString => EndDate.ToString("yyyy-MM-dd");
        public string CreatedAtString => CreatedAt.ToString("yyyy-MM-dd HH:mm");
    }
}
