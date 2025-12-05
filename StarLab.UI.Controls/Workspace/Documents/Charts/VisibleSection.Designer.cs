namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    partial class VisibleSection
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
            checkBoxVisible = new CheckBox();
            SuspendLayout();
            // 
            // checkBoxVisible
            // 
            checkBoxVisible.AutoSize = true;
            checkBoxVisible.Location = new Point(0, 0);
            checkBoxVisible.Name = "checkBoxVisible";
            checkBoxVisible.Size = new Size(60, 19);
            checkBoxVisible.TabIndex = 0;
            checkBoxVisible.Text = "Visible";
            checkBoxVisible.UseVisualStyleBackColor = true;
            // 
            // VisibleSection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(checkBoxVisible);
            Name = "VisibleSection";
            Size = new Size(278, 18);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBoxVisible;
    }
}
