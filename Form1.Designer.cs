namespace Praktika
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            car_table = new Button();
            crime_scene_report_table = new Button();
            education_table = new Button();
            interview_table = new Button();
            orgnaziation_table = new Button();
            dataGridView2 = new DataGridView();
            label1 = new Label();
            deleteBtn = new Button();
            addRowBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Location = new Point(73, 174);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(674, 226);
            dataGridView1.TabIndex = 0;
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            // 
            // car_table
            // 
            car_table.Location = new Point(73, 22);
            car_table.Name = "car_table";
            car_table.Size = new Size(130, 60);
            car_table.TabIndex = 1;
            car_table.Text = "машины";
            car_table.UseVisualStyleBackColor = true;
            car_table.Click += car_table_Click;
            // 
            // crime_scene_report_table
            // 
            crime_scene_report_table.Location = new Point(209, 22);
            crime_scene_report_table.Name = "crime_scene_report_table";
            crime_scene_report_table.Size = new Size(130, 60);
            crime_scene_report_table.TabIndex = 2;
            crime_scene_report_table.Text = "осмотр места преступления";
            crime_scene_report_table.UseVisualStyleBackColor = true;
            crime_scene_report_table.Click += crime_scene_report_table_Click;
            // 
            // education_table
            // 
            education_table.Location = new Point(345, 22);
            education_table.Name = "education_table";
            education_table.Size = new Size(130, 60);
            education_table.TabIndex = 3;
            education_table.Text = "образование";
            education_table.UseVisualStyleBackColor = true;
            education_table.Click += education_table_Click;
            // 
            // interview_table
            // 
            interview_table.Location = new Point(481, 22);
            interview_table.Name = "interview_table";
            interview_table.Size = new Size(130, 60);
            interview_table.TabIndex = 4;
            interview_table.Text = "допрос";
            interview_table.UseVisualStyleBackColor = true;
            interview_table.Click += interview_table_Click;
            // 
            // orgnaziation_table
            // 
            orgnaziation_table.Location = new Point(617, 22);
            orgnaziation_table.Name = "orgnaziation_table";
            orgnaziation_table.Size = new Size(130, 60);
            orgnaziation_table.TabIndex = 5;
            orgnaziation_table.Text = "организация";
            orgnaziation_table.UseVisualStyleBackColor = true;
            orgnaziation_table.Click += orgnaziation_table_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(73, 88);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(674, 67);
            dataGridView2.TabIndex = 6;
            dataGridView2.CellValueChanged += dataGridView2_CellValueChanged;
            dataGridView2.CellValidating += dataGridView2_CellValidating;
            dataGridView2.DataError += dataGridVIew2_DataError;
            dataGridView2.CellEndEdit += dataGridView2_CellEndEdit;
            

            // 
            // label1
            // 
            label1.Location = new Point(163, 426);
            label1.Name = "label1";
            label1.Size = new Size(500, 225);
            label1.TabIndex = 7;
            label1.Text = resources.GetString("label1.Text");
            
            // 
            // deleteBtn
            // 
            deleteBtn.Location = new Point(760, 88);
            deleteBtn.Name = "deleteBtn";
            deleteBtn.Size = new Size(88, 27);
            deleteBtn.TabIndex = 10;
            deleteBtn.Text = "удалить";
            deleteBtn.UseVisualStyleBackColor = true;
            deleteBtn.Click += deleteBtn_Click;
            // 
            // addRowBtn
            // 
            addRowBtn.Location = new Point(760, 174);
            addRowBtn.Name = "addRowBtn";
            addRowBtn.Size = new Size(88, 40);
            addRowBtn.TabIndex = 11;
            addRowBtn.Text = "добавить строку";
            addRowBtn.UseVisualStyleBackColor = true;
            addRowBtn.Click += addRowBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1226, 660);
            Controls.Add(addRowBtn);
            Controls.Add(deleteBtn);
            Controls.Add(label1);
            Controls.Add(dataGridView2);
            Controls.Add(orgnaziation_table);
            Controls.Add(interview_table);
            Controls.Add(education_table);
            Controls.Add(crime_scene_report_table);
            Controls.Add(car_table);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button car_table;
        private Button crime_scene_report_table;
        private Button education_table;
        private Button interview_table;
        private Button orgnaziation_table;
        private DataGridView dataGridView2;
        private Label label1;
        private Button deleteBtn;
        private Button addRowBtn;
    }
}
