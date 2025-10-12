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
            container = new System.Windows.Forms.SplitContainer();
            toolStrip = new ToolStrip();
            toolStripContainer.ContentPanel.SuspendLayout();
            toolStripContainer.TopToolStripPanel.SuspendLayout();
            toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)container).BeginInit();
            container.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            toolStripContainer.ContentPanel.Controls.Add(container);
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
            // container
            // 
            container.Dock = DockStyle.Fill;
            container.Location = new Point(0, 0);
            container.Name = "container";
            container.Size = new Size(642, 542);
            container.SplitterDistance = 317;
            container.SplitterWidth = 5;
            container.TabIndex = 0;
            container.Paint += OnPaint;
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
            // SplitContainer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(toolStripContainer);
            Name = "SplitContainer";
            Size = new Size(642, 567);
            toolStripContainer.ContentPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.PerformLayout();
            toolStripContainer.ResumeLayout(false);
            toolStripContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)container).EndInit();
            container.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer toolStripContainer;
        private System.Windows.Forms.SplitContainer container;
        private UI.Controls.ToolStrip toolStrip;
    }
}
