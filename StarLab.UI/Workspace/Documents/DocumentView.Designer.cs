using StarLab.UI.Controls;

namespace StarLab.UI.Workspace.Documents
{
    partial class DocumentView
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
            splitContainer = new StarLab.UI.Controls.SplitContainer();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.BorderStyle = BorderStyle.FixedSingle;
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Size = new Size(800, 450);
            splitContainer.TabIndex = 0;
            // 
            // DocumentView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer);
            HideOnClose = true;
            Name = "DocumentView";
            Text = "DocumentView";
            ResumeLayout(false);
        }

        #endregion

        private UI.Controls.SplitContainer splitContainer;
    }
}