namespace StarLab.UI.Controls
{
    partial class SplitContainer
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
            toolStripContainer = new ToolStripContainer();
            splitContainer = new System.Windows.Forms.SplitContainer();
            toolStrip = new UI.Controls.ToolStrip();
            toolStripContainer.ContentPanel.SuspendLayout();
            toolStripContainer.TopToolStripPanel.SuspendLayout();
            toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            toolStripContainer.ContentPanel.Controls.Add(splitContainer);
            toolStripContainer.ContentPanel.Size = new Size(642, 542);
            toolStripContainer.Dock = DockStyle.Fill;
            toolStripContainer.Location = new Point(0, 0);
            toolStripContainer.Name = "toolStripContainer";
            toolStripContainer.Size = new Size(642, 567);
            toolStripContainer.TabIndex = 0;
            toolStripContainer.Text = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Size = new Size(642, 542);
            splitContainer.Paint += SplitContainer_Paint;
            splitContainer.SplitterDistance = 317;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 0;
            // 
            // toolStrip
            // 
            toolStrip.Dock = DockStyle.None;
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(642, 25);
            toolStrip.Stretch = true;
            toolStrip.TabIndex = 0;
            // 
            // SplitView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(toolStripContainer);
            Name = "SplitView";
            Size = new Size(642, 567);
            toolStripContainer.ContentPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.PerformLayout();
            toolStripContainer.ResumeLayout(false);
            toolStripContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer toolStripContainer;
        private System.Windows.Forms.SplitContainer splitContainer;
        private UI.Controls.ToolStrip toolStrip;
    }
}
