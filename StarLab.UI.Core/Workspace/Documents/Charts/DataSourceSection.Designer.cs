namespace StarLab.UI.Workspace.Documents.Charts
{
    partial class DataSourceSection
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboX1Data = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            comboY1Data = new ComboBox();
            label3 = new Label();
            comboX2Data = new ComboBox();
            label4 = new Label();
            comboY2Data = new ComboBox();
            SuspendLayout();
            // 
            // comboX1Data
            // 
            comboX1Data.FormattingEnabled = true;
            comboX1Data.Location = new Point(0, 18);
            comboX1Data.Name = "comboX1Data";
            comboX1Data.Size = new Size(260, 23);
            comboX1Data.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(20, 15);
            label1.TabIndex = 2;
            label1.Text = "X1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 56);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 4;
            label2.Text = "Y1";
            // 
            // comboY1Data
            // 
            comboY1Data.FormattingEnabled = true;
            comboY1Data.Location = new Point(0, 74);
            comboY1Data.Name = "comboY1Data";
            comboY1Data.Size = new Size(260, 23);
            comboY1Data.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(0, 112);
            label3.Name = "label3";
            label3.Size = new Size(20, 15);
            label3.TabIndex = 6;
            label3.Text = "X2";
            // 
            // comboX2Data
            // 
            comboX2Data.FormattingEnabled = true;
            comboX2Data.Location = new Point(0, 130);
            comboX2Data.Name = "comboX2Data";
            comboX2Data.Size = new Size(260, 23);
            comboX2Data.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(0, 168);
            label4.Name = "label4";
            label4.Size = new Size(20, 15);
            label4.TabIndex = 8;
            label4.Text = "Y2";
            // 
            // comboY2Data
            // 
            comboY2Data.FormattingEnabled = true;
            comboY2Data.Location = new Point(0, 186);
            comboY2Data.Name = "comboY2Data";
            comboY2Data.Size = new Size(260, 23);
            comboY2Data.TabIndex = 7;
            // 
            // DataSourceSection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(comboY2Data);
            Controls.Add(comboX2Data);
            Controls.Add(comboY1Data);
            Controls.Add(comboX1Data);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "DataSourceSection";
            Size = new Size(266, 211);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboX1Data;
        private Label label1;
        private Label label2;
        private ComboBox comboY1Data;
        private Label label3;
        private ComboBox comboX2Data;
        private Label label4;
        private ComboBox comboY2Data;
    }
}
