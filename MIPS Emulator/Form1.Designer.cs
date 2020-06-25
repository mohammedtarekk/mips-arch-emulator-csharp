namespace MIPS_Emulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.UserCodeTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RegistersDGV = new System.Windows.Forms.DataGridView();
            this.PipelineDGV = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PcTxtBox = new System.Windows.Forms.TextBox();
            this.InitializeBT = new System.Windows.Forms.Button();
            this.RunCycleBT = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Register = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RegistersDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipelineDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Code";
            // 
            // UserCodeTxtBox
            // 
            this.UserCodeTxtBox.Location = new System.Drawing.Point(15, 44);
            this.UserCodeTxtBox.Multiline = true;
            this.UserCodeTxtBox.Name = "UserCodeTxtBox";
            this.UserCodeTxtBox.Size = new System.Drawing.Size(263, 339);
            this.UserCodeTxtBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "MIPS Registers";
            // 
            // RegistersDGV
            // 
            this.RegistersDGV.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.RegistersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegistersDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Register,
            this.Value});
            this.RegistersDGV.Location = new System.Drawing.Point(301, 44);
            this.RegistersDGV.Name = "RegistersDGV";
            this.RegistersDGV.Size = new System.Drawing.Size(283, 339);
            this.RegistersDGV.TabIndex = 3;
            // 
            // PipelineDGV
            // 
            this.PipelineDGV.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.PipelineDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PipelineDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.PipelineDGV.Location = new System.Drawing.Point(605, 44);
            this.PipelineDGV.Name = "PipelineDGV";
            this.PipelineDGV.Size = new System.Drawing.Size(594, 339);
            this.PipelineDGV.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(602, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pipeline Registers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 408);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "PC";
            // 
            // PcTxtBox
            // 
            this.PcTxtBox.Location = new System.Drawing.Point(42, 405);
            this.PcTxtBox.Name = "PcTxtBox";
            this.PcTxtBox.Size = new System.Drawing.Size(100, 20);
            this.PcTxtBox.TabIndex = 7;
            // 
            // InitializeBT
            // 
            this.InitializeBT.Location = new System.Drawing.Point(175, 400);
            this.InitializeBT.Name = "InitializeBT";
            this.InitializeBT.Size = new System.Drawing.Size(104, 30);
            this.InitializeBT.TabIndex = 8;
            this.InitializeBT.Text = "Initialize";
            this.InitializeBT.UseVisualStyleBackColor = true;
            this.InitializeBT.Click += new System.EventHandler(this.InitializeBT_Click);
            // 
            // RunCycleBT
            // 
            this.RunCycleBT.Location = new System.Drawing.Point(285, 400);
            this.RunCycleBT.Name = "RunCycleBT";
            this.RunCycleBT.Size = new System.Drawing.Size(104, 30);
            this.RunCycleBT.TabIndex = 9;
            this.RunCycleBT.Text = "Run 1 Cycle";
            this.RunCycleBT.UseVisualStyleBackColor = true;
            this.RunCycleBT.Click += new System.EventHandler(this.RunCycleBT_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Register";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 430;
            // 
            // Register
            // 
            this.Register.HeaderText = "Register";
            this.Register.Name = "Register";
            this.Register.ReadOnly = true;
            this.Register.Width = 120;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 120;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 441);
            this.Controls.Add(this.RunCycleBT);
            this.Controls.Add(this.InitializeBT);
            this.Controls.Add(this.PcTxtBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PipelineDGV);
            this.Controls.Add(this.RegistersDGV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UserCodeTxtBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "MIPS Emulator";
            ((System.ComponentModel.ISupportInitialize)(this.RegistersDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipelineDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserCodeTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView RegistersDGV;
        private System.Windows.Forms.DataGridView PipelineDGV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PcTxtBox;
        private System.Windows.Forms.Button InitializeBT;
        private System.Windows.Forms.Button RunCycleBT;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Register;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}

