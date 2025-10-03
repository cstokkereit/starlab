using StarLab.UI.Controls;

namespace StarLab.UI.Workspace.WorkspaceExplorer
{
    partial class WorkspaceExplorerView
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
            components = new System.ComponentModel.Container();
            toolStripContainer = new ToolStripContainer();
            treeView = new StarLab.UI.Controls.TreeView();
            imageList = new ImageList(components);
            toolStrip = new StarLab.UI.Controls.ToolStrip();
            toolStripContainer.ContentPanel.SuspendLayout();
            toolStripContainer.TopToolStripPanel.SuspendLayout();
            toolStripContainer.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer
            // 
            toolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer.ContentPanel
            // 
            toolStripContainer.ContentPanel.Controls.Add(treeView);
            toolStripContainer.ContentPanel.Size = new Size(300, 525);
            toolStripContainer.Dock = DockStyle.Fill;
            toolStripContainer.LeftToolStripPanelVisible = false;
            toolStripContainer.Location = new Point(0, 0);
            toolStripContainer.Name = "toolStripContainer";
            toolStripContainer.RightToolStripPanelVisible = false;
            toolStripContainer.Size = new Size(300, 550);
            toolStripContainer.TabIndex = 0;
            toolStripContainer.Text = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
            // 
            // treeView
            // 
            treeView.BorderStyle = BorderStyle.None;
            treeView.Dock = DockStyle.Fill;
            treeView.FullRowSelect = true;
            treeView.HideSelection = false;
            treeView.ImageIndex = 0;
            treeView.ImageList = imageList;
            treeView.Indent = 19;
            treeView.Location = new Point(0, 0);
            treeView.Name = "treeView";
            treeView.SelectedImageIndex = 0;
            treeView.ShowLines = false;
            treeView.ShowNodeToolTips = true;
            treeView.ShowRootLines = false;
            treeView.Size = new Size(300, 525);
            treeView.TabIndex = 5;
            treeView.AfterLabelEdit += TreeView_AfterLabelEdit;
            treeView.AfterCollapse += TreeView_AfterCollapse;
            treeView.AfterExpand += TreeView_AfterExpand;
            treeView.NodeMouseClick += TreeView_NodeMouseClick;
            treeView.NodeMouseDoubleClick += TreeView_NodeDoubleClick;
            treeView.Enter += TreeView_Enter;
            treeView.Leave += TreeView_Leave;
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Transparent;
            // 
            // toolStrip
            // 
            toolStrip.Dock = DockStyle.None;
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(0);
            toolStrip.Size = new Size(300, 25);
            toolStrip.Stretch = true;
            toolStrip.TabIndex = 0;
            // 
            // WorkspaceExplorerView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(toolStripContainer);
            Name = "WorkspaceExplorerView";
            Size = new Size(300, 550);
            toolStripContainer.ContentPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.PerformLayout();
            toolStripContainer.ResumeLayout(false);
            toolStripContainer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer toolStripContainer;
        private UI.Controls.ToolStrip toolStrip;
        private ImageList imageList;
        private UI.Controls.TreeView treeView;
    }
}
