using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;



namespace Praktika
{enum state
    {
        INIT,
        EDIT,
        ADD
    }
    public partial class Form1 : Form
    {
        private string currentTable = "";
        static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database\\baza.accdb";
        private ProgressBar progressBar;
        private bool isLoad = true;
        private DataRow? selectedRowFromDGV1;
        private Button acceptAddRecordBtn;
        private Button cancelAddRecordBtn;
        private state currentState;

        
        public Form1()
        {
            Console.WriteLine("LOADING FORM");
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 244, 244, 244);
            hideDataGrid1();
            hideDataGrid2();
            setShowAddRowBtn(false);
            dataGridView1.ReadOnly = true;
            createAcceptEditBtn();
            acceptAddRecordBtn = createAcceptAddRecordBtn();
            cancelAddRecordBtn = createCancelAddRecordBtn();
            setShowAddRecordBtns(false);
            setShowAcceptEditBtn(false);
            
            isLoad = false;

        }

        private void createAcceptEditBtn()
        {
            Button acceptEditBtn = new Button();
            acceptEditBtn.Name = "acceptEditBtn";
            acceptEditBtn.Text = "принять изменения";
            acceptEditBtn.Location = new Point(deleteBtn.Location.X, deleteBtn.Location.Y+30);
            acceptEditBtn.Size = new Size(deleteBtn.Size.Width, deleteBtn.Size.Height+20);
            acceptEditBtn.Click += acceptEditBtn_Click;
            this.Controls.Add(acceptEditBtn);
        }

        private Button createAcceptAddRecordBtn()
        {
            Button acceptAddRecordBtn = new Button();
            acceptAddRecordBtn.Name = "acceptAddRecordBtn";
            acceptAddRecordBtn.Text = "добавить запись";
            acceptAddRecordBtn.Location = new Point(deleteBtn.Location.X, deleteBtn.Location.Y);
            acceptAddRecordBtn.Size = new Size(deleteBtn.Size.Width, deleteBtn.Size.Height+20);
            acceptAddRecordBtn.Click += acceptAddRecordBtn_Click;
            this.Controls.Add(acceptAddRecordBtn);
            return acceptAddRecordBtn;
        }

        private Button createCancelAddRecordBtn()
        {
            Button cancelAddRecordBtn = new Button();
            cancelAddRecordBtn.Name = "cancelAddRecordBtn";
            cancelAddRecordBtn.Text = "отмена";
            cancelAddRecordBtn.Location = new Point(deleteBtn.Location.X, deleteBtn.Location.Y+50);
            cancelAddRecordBtn.Size = new Size(deleteBtn.Size.Width, deleteBtn.Size.Height+20);
            cancelAddRecordBtn.Click += cancelAddRecordBtn_Click;
            this.Controls.Add(cancelAddRecordBtn);
            return cancelAddRecordBtn;

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
        
        private void setShowDeleteBtn(bool isShow)
        {
            if (isShow)
            {
                deleteBtn.Show();
            }
            else
            {
                deleteBtn.Hide();    
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            setShowDeleteBtn(false);
            
        }

        private void setShowAddRowBtn(bool isShow)
        {
            if (isShow)
            {
                addRowBtn.Show();
            }
            else
            {
                addRowBtn.Hide();
            }
        }

        private void setShowAcceptEditBtn(bool isShow)
        {
            if(isShow)
            {
                this.Controls.Find("acceptEditBtn", false)[0].Show();
            }
            else
            {
                this.Controls.Find("acceptEditBtn", false)[0].Hide();
            }
        }
        
        private void setShowAcceptAddRecordBtn(bool isShow)
        {
            if (isShow)
            {
                acceptAddRecordBtn.Show();
            }
            else
            {
                acceptAddRecordBtn.Hide();
            }
        }
        
        private void setShowCancelAddRecordBtn(bool isShow)
        {
            if (isShow)
            {
                cancelAddRecordBtn.Show();
            }
            else
            {
                cancelAddRecordBtn.Hide();
            }
        }

        private void setShowAddRecordBtns(bool isShow)
        {
            setShowCancelAddRecordBtn(isShow);
            setShowAcceptAddRecordBtn(isShow);
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
                connection.Close();
            }

        }

        private void create_empty_copy_current_table_in_datagrid2()
        {
            setShowDeleteBtn(false);
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
                Console.WriteLine($"{dataset.Tables[0].Rows[0].ItemArray[0]}");
                dataset.Clear();
                dataGridView2.DataSource = dataset.Tables[0];
                dataGridView2.Columns[0].ReadOnly = true;
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
                setShowAddRowBtn(true);
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
                setShowAddRowBtn(true);
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
                setShowAddRowBtn(true);
                setLoadFormState(false);
            }
        }

        private void interview_table_Click(object sender, EventArgs e)
        {
            if (!isLoad)
            {
                setLoadFormState(true);
                enableTableBtns(false);
                string selectedTable = "interview";
                currentTable = selectedTable;
                string query = $"SELECT * FROM {selectedTable}";
                select_data_from_db_and_display_datagrid(query);
                create_empty_copy_current_table_in_datagrid2();
                showDataGrid1();
                showDataGrid2();
                enableTableBtns(true);
                setShowAddRowBtn(true);
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
                setShowAddRowBtn(true);
                setLoadFormState(false);
            }
        }

        
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("DATA GRIED VIEW 1: CLICK");
            setShowAddRecordBtns(false);
            setShowAddRowBtn(true);
            currentState = state.EDIT;
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
                               
                targetDataTable.Rows.InsertAt(targetRow, 1);

                setShowDeleteBtn(true);
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
        
        
        // DATA GRID 2 EVENTS
        
        
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
            // dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count-1);
            if (dataGridView2.Rows.Count > 1)
            {
                if(currentState == state.EDIT)
                {
                    setShowAcceptEditBtn(true);
                }
            }
            
            
        }
        
       
        
        private void clear_dataGridView2()
        {
            DataTable targetDataTable = (DataTable)dataGridView2.DataSource;
            targetDataTable.Rows.Clear();
        }


        private int get_last_not_empty_row_id(DataGridView targetDataTable)
        {
            int lastIdRow = targetDataTable.Rows.Count - 1;
            while (IsRowEmpty(targetDataTable.Rows[lastIdRow]))
            {
                lastIdRow -= 1;
            }

            return lastIdRow;
        }

        private void fill_dgv2_new_last_index_row_db()
        {
            DataTable sorceDataTable = (DataTable)dataGridView1.DataSource;
            DataTable targetDataTable = (DataTable)dataGridView2.DataSource;
            int lastIdNotEmptyRow = get_last_not_empty_row_id(dataGridView1);
            double newLastIdx = Convert.ToDouble(sorceDataTable.Rows[lastIdNotEmptyRow].ItemArray.GetValue(0)) + 1;
            dataGridView2.Rows[0].Cells[0].Value = newLastIdx;
        }

        private void addRowBtn_Click(object sender, EventArgs e)
        {
            // TODO
            currentState = state.ADD;
            setLoadFormState(true);
            setShowDeleteBtn(false);
            setShowAcceptEditBtn(false);
            clear_dataGridView2();
            setShowAddRowBtn(false);
            setShowAddRecordBtns(true);
            setLoadFormState(false);
            
        }
        
        private void acceptAddRecordBtn_Click(object sender, EventArgs e)
        {
            setLoadFormState(true);
            DataTable targetDataTable = (DataTable)dataGridView2.DataSource;
            DataGridViewRow rowToAdded = dataGridView2.Rows[0];
            // fill_dgv2_new_last_index_row_db();
            if (!IsRowEmpty(rowToAdded))
            {
                try
                {
                    insert_new_row_form_datagridview2_to_db(targetDataTable);
                    updateDataFromDataGrids();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении записи {ex.Message}");
                }
                finally
                {
                    currentState = state.INIT;
                }
                

            }
            else
            {
                MessageBox.Show("Строка не может содержать пустые ячейки");
            }
            setLoadFormState(false);
            
        }

        private void cancelAddRecordBtn_Click(object sender, EventArgs e)
        {
            
            setShowAddRecordBtns(false);
            setShowAddRowBtn(true);
            currentState = state.INIT;
        }
        
        private bool IsRowEmpty(DataGridViewRow row)
        {
            
            for (int i = 1; i < row.Cells.Count - 1; i++)
            {
                DataGridViewCell cell = row.Cells[i];
                if (string.IsNullOrWhiteSpace(cell.Value?.ToString()))
                {
                    return true;
                }
            }

            return false;
            // foreach (DataGridViewCell cell in row.Cells)
            // {
            //     if (string.IsNullOrWhiteSpace(cell.Value?.ToString()))
            //     {
            //         return true;
            //     }
            //     
            // }
            // return false;
        }

        private string generate_insert_row_query(DataTable targetDataTable)
        {
            string query = $"INSERT INTO {currentTable} (";
            string values = ") VALUES (";
            int countColumns = targetDataTable.Columns.Count;
            
            for (int i = 1; i <countColumns ; i++)
            {
                DataColumn column = targetDataTable.Columns[i];

                query += $"{column.ColumnName}";
                values += $"@{column.ColumnName}";
                
                if (i != countColumns - 1)
                {
                    query += ", ";
                    values += ", ";
                }
                else
                {
                    values += ")";
                }

            }
            
            return query + values;
        }

        private void updateDataFromDataGrids()
        {
            select_data_from_db_and_display_datagrid($"SELECT * FROM {currentTable}");
            clear_dataGridView2();
        }
        private void insert_new_row_form_datagridview2_to_db(DataTable targetDataTable)
        {
            // throw new NotImplementedException("ADD INSERT");
            string insertQuery = generate_insert_row_query(targetDataTable);
            Console.WriteLine(insertQuery);
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Создаем команду для выполнения запроса
                OleDbCommand command = new OleDbCommand(insertQuery, connection);
            
                // Привязываем параметры к команде
                for (int i = 1; i < targetDataTable.Columns.Count; i++)
                {                    
                    Console.WriteLine($"@{targetDataTable.Columns[i].ColumnName} = {targetDataTable.Rows[0].ItemArray[i]}");
                    command.Parameters.AddWithValue($"@{targetDataTable.Columns[i].ColumnName}", targetDataTable.Rows[0].ItemArray[i]);
                }
                                            
                // Открываем соединение и выполняем запрос
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
            
                // Выводим сообщение об успешном обновлении
                MessageBox.Show($"Строка успешно добавлена: {rowsAffected}");
            }


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
                updateDataFromDataGrids();
            }
            setLoadFormState(false);
            
        }

        
        
        private string generate_update_row_query(DataTable targetDataTable)
        {
            
            string query = $"UPDATE {currentTable} SET ";
            int countColumns = targetDataTable.Columns.Count;
            string idColumnName = targetDataTable.Columns[0].ColumnName;
            for (int i = 1; i <countColumns ; i++)
            {
                DataColumn column = targetDataTable.Columns[i];
                
                    query += $"{column.ColumnName} = @{column.ColumnName}";
                    if (i < countColumns - 1)
                    {
                        query += ", ";
                    }
            }
            query += $" WHERE {idColumnName} = @{idColumnName}";

            return query;
        }
        
        private void update_current_row_in_db()
        {
            // string query = "UPDATE Users SET Name = @Name, Email = @Email WHERE ID = @Id";
            DataTable targetDataTable = (DataTable)dataGridView2.DataSource;
            string query = generate_update_row_query(targetDataTable);
            Console.WriteLine(query);
            
            // Создаем подключение к базе данных
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Создаем команду для выполнения запроса
                OleDbCommand command = new OleDbCommand(query, connection);
            
                // Привязываем параметры к команде
                for (int i = 1; i < targetDataTable.Columns.Count; i++)
                {                    
                    command.Parameters.AddWithValue($"@{targetDataTable.Columns[i].ColumnName}", targetDataTable.Rows[0].ItemArray[i]);
                }
                command.Parameters.AddWithValue($"@{targetDataTable.Columns[0].ColumnName}", targetDataTable.Rows[0].ItemArray[0]);
                            
                // Открываем соединение и выполняем запрос
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
            
                // Выводим сообщение об успешном обновлении
                MessageBox.Show($"Строка успешно обновлена");
            }

        }

        private void acceptEditBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"acceptEditBtn_Click".ToUpper());
            try
            {
                setLoadFormState(true);
                update_current_row_in_db();
                clear_dataGridView2();
                select_data_from_db_and_display_datagrid($"SELECT * FROM {currentTable}");
                setShowAcceptEditBtn(false);
                currentState = state.INIT;
                setLoadFormState(false);
                
            }
            catch (Exception ex)
            {        
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }


        
    }
}
