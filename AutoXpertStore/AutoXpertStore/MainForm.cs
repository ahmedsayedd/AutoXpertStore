using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoXpertStore
{
    public partial class MainForm : Form
    {
        private string employeeName;
        private int employeeID;



        public MainForm(string employeeName, int employeeID)
        {
            InitializeComponent();

            // Store the employee details
            this.employeeName = employeeName;
            this.employeeID = employeeID;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Display employee details in text boxes
            textBox1.Text = employeeName;
            textBox2.Text = employeeID.ToString();


            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.White;

            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.White;

            // TextBoxes: Set background color and text color
            textBox1.BackColor = Color.LightBlue; // Adjust to match your wallpaper
            textBox1.ForeColor = Color.DarkBlue;

            textBox2.BackColor = Color.LightBlue;
            textBox2.ForeColor = Color.DarkBlue;

            // Optionally, make the text boxes borderless for a cleaner look
            textBox1.BorderStyle = BorderStyle.None;
            textBox2.BorderStyle = BorderStyle.None;
        }
    }
}
