namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    partial class TickMarksSection
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
            labelSectionName = new Label();
            SuspendLayout();
            // 
            // labelSectionName
            // 
            labelSectionName.AutoSize = true;
            labelSectionName.Location = new Point(0, 6);
            labelSectionName.Name = "labelSectionName";
            labelSectionName.Size = new Size(63, 15);
            labelSectionName.TabIndex = 2;
            labelSectionName.Text = "Tick Marks";
            // 
            // TickMarksSection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelSectionName);
            Name = "TickMarksSection";
            Size = new Size(450, 155);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelSectionName;
    }
}
