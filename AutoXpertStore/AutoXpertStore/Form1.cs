using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
    
    
    
    namespace AutoXpertStore
{
    public partial class Form1 : Form
    {

        
        string connectionString = "Server=DESKTOP-8471MT5;Database=AutoXpert;Integrated Security=True;TrustServerCertificate=True;";


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = textBox1.Text.Trim();
                string password = textBox2.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate user and get employee details
                (bool isValid, string employeeName, int employeeID) = IsValidUser(username, password);

                if (isValid)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Open Main Form and pass employee details
                    this.Hide();
                    MainForm mainForm = new MainForm(employeeName, employeeID);
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private (bool, string, int) IsValidUser(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // string query = "SELECT EmployeeID, EmployeeName FROM Access_Login WHERE Username = @Username AND Password_ = @Password";
                    string query = @"
                SELECT 
                    e.Emp_ID, 
                    e.First_Name + ' ' + e.Last_Name AS EmployeeName
                FROM 
                    Access_Login al
                INNER JOIN 
                    Employees e 
                ON 
                    al.Emp_ID = e.Emp_ID
                WHERE 
                    al.Username = @Username AND al.Password_ = @Password";



                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int employeeID = reader.GetInt32(0); // EmployeeID
                        string employeeName = reader.GetString(1); // EmployeeName
                        return (true, employeeName, employeeID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return (false, null, 0);
        }
    }


    
}
