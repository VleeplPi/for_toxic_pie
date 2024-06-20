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
        private bool isLoad = true;
        private DataRow? selectedRowFromDGV1;

        public Form1()
        {
            Console.WriteLine("LOADING FORM");
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 244, 244, 244);
            hideDataGrid1();
            hideDataGrid2();
            addRowBtn.Hide();
            dataGridView1.ReadOnly = true;
            isLoad = false;

        }

        private void setLoadFormState(bool isLoad)
        {
            if(isLoad)
            {
                this.Cursor = Cursors.WaitCursor;
                
            }
            else
            {
                this.Cursor = Cursors.Default;
            }

            this.Enabled = !isLoad;
        }
        private void enableTableBtns(bool value)
        {
            car_table.Enabled = value;
            crime_scene_report_table.Enabled = value;
            education_table.Enabled = value;
            orgnaziation_table.Enabled = value;
            interview_table.Enabled = value;
            
        }
        private void hideDataGrid1()
        {
            dataGridView1.Hide();
        }

        private void showDataGrid1()
        {
            dataGridView1.Show();
        }
        
        private void hideDataGrid2()
        {
            dataGridView2.Hide();
        }

        private void showDataGrid2()
        {
            dataGridView2.Show();
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

        private void select_data_from_db_and_display_datagrid(string query)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            using (connection)
            {
                // Создание команды для выборки данных
                OleDbCommand command = new OleDbCommand(query, connection);
                // Открытие подключения
                connection.Open();

                // Создание адаптера данных для заполнения набора данных
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                // Привязка набора данных к компоненту DataGridView
                dataGridView1.DataSource = dataset.Tables[0];
                // connection.Close();
            }

        }

        private void create_empty_copy_current_table_in_datagrid2()
        {
            Console.WriteLine("SELECT DATA FROM DB AND DISPLAY DATA GRID");
            string query = "SELECT TOP 1 * FROM " + currentTable;
            OleDbConnection connection = new OleDbConnection(connectionString);
            using (connection)
            {
                // connection.Open();
                // Создание команды для выборки данных
                OleDbCommand command = new OleDbCommand(query, connection);
                // Открытие подключения
                
                // Создание адаптера данных для заполнения набора данных
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                dataset.Clear();
                dataGridView2.DataSource = dataset.Tables[0];
            }
        }

        private void car_table_Click(object sender, EventArgs e)
        {

            if(!isLoad) {
                setLoadFormState(true);
                enableTableBtns(false);
                string selectedTable = "car";
                currentTable = selectedTable;
                string query = "SELECT * FROM " + selectedTable; // твой супер мега запрос к базе данных
                select_data_from_db_and_display_datagrid(query);
                create_empty_copy_current_table_in_datagrid2();
                showDataGrid1();
                showDataGrid2();
                enableTableBtns(true);
                setLoadFormState(false);
            }
            
        }

        private void crime_scene_report_table_Click(object sender, EventArgs e)
        {
            if(!isLoad)
            {
                setLoadFormState(true);
                enableTableBtns(false);
                string selectedTable = "crime_scene_report";
                currentTable = selectedTable;
                string query = "SELECT * FROM crime_scene_report";
                select_data_from_db_and_display_datagrid(query);
                create_empty_copy_current_table_in_datagrid2();
                showDataGrid1();
                showDataGrid2();
                enableTableBtns(true);
                setLoadFormState(false);
            }
        }

        private void education_table_Click(object sender, EventArgs e)
        {
            if(!isLoad)
            {
                setLoadFormState(true);
                enableTableBtns(false);
                string selectedTable = "education";
                currentTable = selectedTable;
                string query = "SELECT * FROM education";
                select_data_from_db_and_display_datagrid(query);
                create_empty_copy_current_table_in_datagrid2();
                showDataGrid1();
                showDataGrid2();
                enableTableBtns(true);
                setLoadFormState(false);
            }
        }

        private void interview_table_Click(object sender, EventArgs e)
        {
            if(!isLoad)
            {
                setLoadFormState(true);
                enableTableBtns(false);
                string selectedTable = "interview";
                currentTable = selectedTable;
                string query = "SELECT * FROM interview";
                select_data_from_db_and_display_datagrid(query);
                create_empty_copy_current_table_in_datagrid2();
                enableTableBtns(true);
                setLoadFormState(false);
            }
        }

        private void orgnaziation_table_Click(object sender, EventArgs e)
        {
            if(!isLoad)
            {
                setLoadFormState(true);
                enableTableBtns(false);
                string selectedTable = "orgnaziation";
                currentTable = selectedTable;
                string query = "SELECT * FROM orgnaziation";
                select_data_from_db_and_display_datagrid(query);
                create_empty_copy_current_table_in_datagrid2();
                enableTableBtns(true);
                setLoadFormState(false);
            }
        }

        
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("DATA GRIED VIEW 1: CLICK");
            // Получаем индекс выбранной строки в dataGridView1
            int selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            Console.WriteLine(dataGridView2.Rows.Count);

            // Проверяем, что строка выбрана
            if (selectedRowIndex >= 0)
            {
                // Получаем DataTable, привязанный к dataGridView1
                DataTable sourceDataTable = (DataTable)dataGridView1.DataSource;
                
                // Получаем строку из исходного DataTable
                DataRow sourceRow = sourceDataTable.Rows[selectedRowIndex];
                selectedRowFromDGV1 = sourceRow;
                // Создаем новую строку в целевом DataTable
                DataTable targetDataTable = (DataTable)dataGridView2.DataSource;
                
                targetDataTable.Rows.Clear();
                DataRow targetRow = copy_row_value(sourceRow, targetDataTable.NewRow());
                // Копируем значения ячеек из исходной строки в новую строку
                // for (int i = 0; i < sourceRow.ItemArray.Length; i++)
                // {
                //     targetRow[i] = sourceRow[i];
                // }
                // Добавляем новую строку в целевой DataTable
                
                targetDataTable.Rows.InsertAt(targetRow, 1);
                
                showDeleteBtn();
            }
        }

        private DataRow copy_row_value(DataRow sourceRow, DataRow targetRow)
        {
            for (int i = 0; i < sourceRow.ItemArray.Length; i++)
            {
                targetRow[i] = sourceRow[i];
            }

            return targetRow;
        }
        
               
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine($"dataGridView2_CellContentClick {e.RowIndex}".ToUpper());
            Type cellType = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType();
            Console.WriteLine($"{cellType}");
            
        }

        private void dataGridView2_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            Console.WriteLine($"dataGridView2_CellValidating".ToUpper());
            Type columnType = dataGridView2.Columns[e.ColumnIndex].ValueType;
            Console.WriteLine($"{columnType}");
        }
        
        private void dataGridVIew2_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.GetType() == typeof(FormatException))
            {
                MessageBox.Show("неподходящий формат введенных данных");
            }
            else
            {
                MessageBox.Show("упс, что-то пошло не так..");
            }

            dataGridView2.CancelEdit();
            e.Cancel = true;
        }
        
        private void dataGridView2_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("dataGridView2_CellEndEdit".ToUpper());
            Button acceptEditBtn = new Button();
            acceptEditBtn.Name = "acceptEditBtn";
            acceptEditBtn.Text = "принять изменения";
            acceptEditBtn.Location = new Point(deleteBtn.Location.X, deleteBtn.Location.Y+30);
            acceptEditBtn.Size = new Size(deleteBtn.Size.Width, deleteBtn.Size.Height+20);
            acceptEditBtn.Click += acceptEditBtn_Click;
            this.Controls.Add(acceptEditBtn);
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
                            MessageBox.Show("Запись успешно удалена.");
                        }
                        else
                        {
                            MessageBox.Show("Не удалось найти запись для удаления.");
                        }
                    }
                }

                successDelete = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            return successDelete;
        }
        

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            setLoadFormState(true);
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
            setLoadFormState(false);
            
        }

        private void update_current_row_in_db()
        {
            // TODO(ADD UPDATE ROW IN DB);
            throw new NotImplementedException("ADD UPDATE ROW IN DB");
        }

        private void acceptEditBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"acceptEditBtn_Click".ToUpper());
            update_current_row_in_db();

        }


        
    }
}
