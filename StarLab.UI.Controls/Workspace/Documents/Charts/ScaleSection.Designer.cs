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
            checkAutoScale = new CheckBox();
            SuspendLayout();
            // 
            // textMinimum
            // 
            textMinimum.BorderStyle = BorderStyle.FixedSingle;
            textMinimum.Location = new Point(0, 18);
            textMinimum.Name = "textMinimum";
            textMinimum.Size = new Size(60, 23);
            textMinimum.TabIndex = 2;
            // 
            // textMaximum
            // 
            textMaximum.BorderStyle = BorderStyle.FixedSingle;
            textMaximum.Location = new Point(87, 18);
            textMaximum.Name = "textMaximum";
            textMaximum.Size = new Size(60, 23);
            textMaximum.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 4;
            label1.Text = "Minimum";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(87, 0);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 5;
            label2.Text = "Maximum";
            // 
            // checkReversed
            // 
            checkReversed.AutoSize = true;
            checkReversed.Location = new Point(5, 48);
            checkReversed.Name = "checkReversed";
            checkReversed.Size = new Size(73, 19);
            checkReversed.TabIndex = 6;
            checkReversed.Text = "Reversed";
            checkReversed.UseVisualStyleBackColor = true;
            // 
            // checkAutoScale
            // 
            checkAutoScale.AutoSize = true;
            checkAutoScale.Location = new Point(175, 22);
            checkAutoScale.Name = "checkAutoScale";
            checkAutoScale.Size = new Size(82, 19);
            checkAutoScale.TabIndex = 7;
            checkAutoScale.Text = "Automatic";
            checkAutoScale.UseVisualStyleBackColor = true;
            // 
            // ScaleSection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textMinimum);
            Controls.Add(textMaximum);
            Controls.Add(checkAutoScale);
            Controls.Add(checkReversed);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ScaleSection";
            Size = new Size(278, 109);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textMinimum;
        private TextBox textMaximum;
        private Label label1;
        private Label label2;
        private CheckBox checkReversed;
        private CheckBox checkAutoScale;
    }
}
