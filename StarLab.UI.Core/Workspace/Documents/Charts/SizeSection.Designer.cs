namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    partial class SizeSection
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
            label1 = new Label();
            trackBarSize = new TrackBar();
            textSize = new TextBox();
            ((System.ComponentModel.ISupportInitialize)trackBarSize).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 1;
            label1.Text = "Size";
            // 
            // trackBarSize
            // 
            trackBarSize.LargeChange = 3;
            trackBarSize.Location = new Point(43, 7);
            trackBarSize.Minimum = 1;
            trackBarSize.Name = "trackBarSize";
            trackBarSize.Size = new Size(209, 45);
            trackBarSize.TabIndex = 3;
            trackBarSize.TickStyle = TickStyle.Both;
            trackBarSize.Value = 1;
            // 
            // textSize
            // 
            textSize.BorderStyle = BorderStyle.FixedSingle;
            textSize.Location = new Point(3, 16);
            textSize.Name = "textSize";
            textSize.Size = new Size(34, 23);
            textSize.TabIndex = 4;
            textSize.WordWrap = false;
            // 
            // SizeSection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textSize);
            Controls.Add(trackBarSize);
            Controls.Add(label1);
            Name = "SizeSection";
            Size = new Size(257, 49);
            ((System.ComponentModel.ISupportInitialize)trackBarSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TrackBar trackBarSize;
        private TextBox textSize;
    }
}
