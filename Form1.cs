using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;



namespace Praktika
{
    public partial class Form1 : Form
    {

        private string currentTable = "";
        static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database\\baza.accdb";
        private ProgressBar progressBar;

        public Form1()
        {
            Console.WriteLine("LOADING FORM");
            InitializeComponent();

        }
        
        private void showDeleteBtn()
        {
            Console.WriteLine("SHOW DELETE BTN");
            deleteBtn.Show();
        }

        private void hideDeleteBtn()
        {
            Console.WriteLine("HIDE DELETE BTN");
            deleteBtn.Hide();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            hideDeleteBtn();
            
        }

        private void change_label_1_text(string text)
        {

        }

        private void select_data_from_db_and_display_datagrid(string query)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            using (connection)
            {
                // �������� ������� ��� ������� ������
                OleDbCommand command = new OleDbCommand(query, connection);
                // �������� �����������
                connection.Open();

                // �������� �������� ������ ��� ���������� ������ ������
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                // �������� ������ ������ � ���������� DataGridView
                dataGridView1.DataSource = dataset.Tables[0];
                // connection.Close();
            }

        }

        private void create_empty_copy_current_table()
        {
            Console.WriteLine("SELECT DATA FROM DB AND DISPLAY DATA GRID");
            string query = "SELECT TOP 1 * FROM " + currentTable;
            OleDbConnection connection = new OleDbConnection(connectionString);
            using (connection)
            {
                // connection.Open();
                // �������� ������� ��� ������� ������
                OleDbCommand command = new OleDbCommand(query, connection);
                // �������� �����������
                
                // �������� �������� ������ ��� ���������� ������ ������
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                dataset.Clear();
                dataGridView2.DataSource = dataset.Tables[0];
            }
        }

        private void car_table_Click(object sender, EventArgs e)
        {

            string selectedTable = "car";
            currentTable = selectedTable;

            string query = "SELECT * FROM " + selectedTable; // ���� ����� ���� ������ � ���� ������
            select_data_from_db_and_display_datagrid(query);
            create_empty_copy_current_table();
            
        }

        private void crime_scene_report_table_Click(object sender, EventArgs e)
        {
            string selectedTable = "crime_scene_report";
            currentTable = selectedTable;
            string query = "SELECT * FROM crime_scene_report";
            select_data_from_db_and_display_datagrid(query);
            create_empty_copy_current_table();
            
        }

        private void education_table_Click(object sender, EventArgs e)
        {
            string selectedTable = "education";
            currentTable = selectedTable;
            string query = "SELECT * FROM education";
            select_data_from_db_and_display_datagrid(query);
            create_empty_copy_current_table();
            
        }

        private void interview_table_Click(object sender, EventArgs e)
        {
            string selectedTable = "interview";
            currentTable = selectedTable;
            string query = "SELECT * FROM interview";
            select_data_from_db_and_display_datagrid(query);
            create_empty_copy_current_table();
            
        }

        private void orgnaziation_table_Click(object sender, EventArgs e)
        {
            string selectedTable = "orgnaziation";
            currentTable = selectedTable;
            string query = "SELECT * FROM orgnaziation";
            select_data_from_db_and_display_datagrid(query);
            create_empty_copy_current_table();

        }
        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            Debug.WriteLine("DATA GRID VIEW VALIDATE");
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("DATA GRIED VIEW 1: CLICK");
            // �������� ������ ��������� ������ � dataGridView1
            int selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            Console.WriteLine(dataGridView2.Rows.Count);

            // ���������, ��� ������ �������
            if (selectedRowIndex >= 0)
            {
                // �������� DataTable, ����������� � dataGridView1
                DataTable sourceDataTable = (DataTable)dataGridView1.DataSource;
                
                // �������� ������ �� ��������� DataTable
                DataRow sourceRow = sourceDataTable.Rows[selectedRowIndex];
                

                // ������� ����� ������ � ������� DataTable
                DataTable targetDataTable = (DataTable)dataGridView2.DataSource;
                
                targetDataTable.Rows.Clear();
                DataRow targetRow = targetDataTable.NewRow();
                // �������� �������� ����� �� �������� ������ � ����� ������
                for (int i = 0; i < sourceRow.ItemArray.Length; i++)
                {
                    targetRow[i] = sourceRow[i];
                }
                // ��������� ����� ������ � ������� DataTable
                
                targetDataTable.Rows.InsertAt(targetRow, 1);
                
                showDeleteBtn();
            }
        }

        private void clear_dataGridView2()
        {
            DataTable targetDataTable = (DataTable)dataGridView2.DataSource;
            targetDataTable.Rows.Clear();
        }

        private void addRowBtn_Click(object sender, EventArgs e)
        {
            
            clear_dataGridView2();
            // TODO
            
            
        }
        
        private bool deleteRecord(int recordId, string idColumnName)
        {
            bool successDelete = false;
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = $"DELETE FROM {currentTable} WHERE {idColumnName} = @RecordId";

                    using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@RecordId", recordId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("������ ������� �������.");
                        }
                        else
                        {
                            MessageBox.Show("�� ������� ����� ������ ��� ��������.");
                        }
                    }
                }

                successDelete = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"��������� ������: {ex.Message}");
            }
            return successDelete;
        }
        

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DataTable targetDataTable = (DataTable)dataGridView2.DataSource;
            
            DataRow selectedRow = targetDataTable.Rows[0];
            String idSelectedRow = selectedRow.ItemArray[0].ToString();
            Console.WriteLine($"ID SELECTED ROW {idSelectedRow}");
            int recordId = Convert.ToInt32(idSelectedRow);
            String idColumnName = targetDataTable.Columns[0].ColumnName;
            bool isDeleted = deleteRecord(recordId,idColumnName);
            if (isDeleted)
            {
                select_data_from_db_and_display_datagrid($"SELECT * FROM {currentTable}");
                clear_dataGridView2();
            }
            
        }
    }
}
