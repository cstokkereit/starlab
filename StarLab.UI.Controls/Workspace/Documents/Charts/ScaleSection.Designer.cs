namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    partial class ScaleSection
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
            textMinimum = new TextBox();
            textMaximum = new TextBox();
            label1 = new Label();
            label2 = new Label();
            checkReversed = new CheckBox();
            SuspendLayout();
            // 
            // textMinimum
            // 
            textMinimum.Location = new Point(124, 3);
            textMinimum.Name = "textMinimum";
            textMinimum.Size = new Size(74, 23);
            textMinimum.TabIndex = 2;
            // 
            // textMaximum
            // 
            textMaximum.Location = new Point(371, 4);
            textMaximum.Name = "textMaximum";
            textMaximum.Size = new Size(74, 23);
            textMaximum.TabIndex = 3;
            // 
            // label1
            // 
            label1.Location = new Point(24, 7);
            label1.Name = "label1";
            label1.Size = new Size(95, 19);
            label1.TabIndex = 4;
            label1.Text = "Minimum Value";
            // 
            // label2
            // 
            label2.Location = new Point(270, 7);
            label2.Name = "label2";
            label2.Size = new Size(95, 19);
            label2.TabIndex = 5;
            label2.Text = "Maximum Value";
            // 
            // checkReversed
            // 
            checkReversed.AutoSize = true;
            checkReversed.Location = new Point(24, 48);
            checkReversed.Name = "checkReversed";
            checkReversed.Size = new Size(73, 19);
            checkReversed.TabIndex = 6;
            checkReversed.Text = "Reversed";
            checkReversed.UseVisualStyleBackColor = true;
            // 
            // ScaleSection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(checkReversed);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textMaximum);
            Controls.Add(textMinimum);
            Name = "ScaleSection";
            Size = new Size(538, 150);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textMinimum;
        private TextBox textMaximum;
        private Label label1;
        private Label label2;
        private CheckBox checkReversed;
    }
}
