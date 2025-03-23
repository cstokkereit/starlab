namespace StarLab.UI.Workspace
{
    partial class ApplicationView
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
            menuStrip = new UI.Controls.ManagedMenuStrip();
            statusStrip = new StatusStrip();
            toolStripContainer = new ToolStripContainer();
            dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            toolStrip = new UI.Controls.ToolStrip();
            toolStripContainer.ContentPanel.SuspendLayout();
            toolStripContainer.TopToolStripPanel.SuspendLayout();
            toolStripContainer.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(902, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "sMenuStrip1";
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 484);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(902, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            toolStripContainer.ContentPanel.Controls.Add(dockPanel);
            toolStripContainer.ContentPanel.Size = new Size(902, 435);
            toolStripContainer.Dock = DockStyle.Fill;
            toolStripContainer.Location = new Point(0, 24);
            toolStripContainer.Name = "toolStripContainer";
            toolStripContainer.Size = new Size(902, 460);
            toolStripContainer.TabIndex = 3;
            toolStripContainer.Text = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
            // 
            // dockPanel
            // 
            dockPanel.Dock = DockStyle.Fill;
            dockPanel.Location = new Point(0, 0);
            dockPanel.Name = "dockPanel";
            dockPanel.Size = new Size(902, 435);
            dockPanel.TabIndex = 0;
            dockPanel.ContentRemoved += DockPanel_DockContentRemoved;
            dockPanel.ActiveDocumentChanged += DockPanel_ActiveDocumentChanged;
            // 
            // toolStrip
            // 
            toolStrip.Dock = DockStyle.None;
            toolStrip.Location = new Point(3, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(111, 25);
            toolStrip.TabIndex = 0;
            // 
            // WorkspaceView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(902, 506);
            Controls.Add(toolStripContainer);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "WorkspaceView";
            Text = "StarLab";
            FormClosing += Form_Closing;
            toolStripContainer.ContentPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.PerformLayout();
            toolStripContainer.ResumeLayout(false);
            toolStripContainer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UI.Controls.ManagedMenuStrip menuStrip;
        private StatusStrip statusStrip;
        private ToolStripContainer toolStripContainer;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private UI.Controls.ToolStrip toolStrip;
    }
}