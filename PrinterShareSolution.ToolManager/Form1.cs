using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterShareSolution.ToolManager
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=.;Initial Catalog = PrinterShareSolutionVer1; 
                Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();
            getData();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
/*            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDtaUsers = new SqlDataAdapter("SELECT * FROM AppUsers", sqlCon);
                DataTable dtblUsers = new DataTable();
                sqlDtaUsers.Fill(dtblUsers);
                dgv1.DataSource = dtblUsers;

                SqlDataAdapter sqlDtaPrinters = new SqlDataAdapter("SELECT * FROM Printers", sqlCon);
                DataTable dtblPrinters = new DataTable();
                sqlDtaPrinters.Fill(dtblPrinters);
                dgv2.DataSource = dtblPrinters;

                SqlDataAdapter sqlDtaHistory = new SqlDataAdapter("SELECT * FROM HistoryOfUsers", sqlCon);
                DataTable dtblHistory = new DataTable();
                sqlDtaHistory.Fill(dtblHistory);
                dgv3.DataSource = dtblHistory;
            }*/
        }

        private void getData()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDtaUsers = new SqlDataAdapter("SELECT * FROM AppUsers", sqlCon);
                DataTable dtblUsers = new DataTable();
                sqlDtaUsers.Fill(dtblUsers);
                dgv1.AutoGenerateColumns = false;
                dgv1.DataSource = dtblUsers;

                SqlDataAdapter sqlDtaPrinters = new SqlDataAdapter("SELECT * FROM Printers", sqlCon);
                DataTable dtblPrinters = new DataTable();
                sqlDtaPrinters.Fill(dtblPrinters);
                dgv2.DataSource = dtblPrinters;

                SqlDataAdapter sqlDtaHistory = new SqlDataAdapter("SELECT * FROM HistoryOfUsers", sqlCon);
                DataTable dtblHistory = new DataTable();
                sqlDtaHistory.Fill(dtblHistory);
                dgv3.DataSource = dtblHistory;
            }
        }
    }
}
