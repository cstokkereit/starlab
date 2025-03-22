namespace StarLab.UI.Workspace.Documents.Charts
{
    partial class ChartSettingsView
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
            treeView = new UI.Controls.TreeView();
            buttonCancel = new Button();
            buttonOK = new Button();
            panelSettings = new Panel();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeView.Location = new Point(10, 10);
            treeView.Name = "treeView";
            treeView.Size = new Size(220, 338);
            treeView.TabIndex = 1;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(503, 363);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(85, 25);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(408, 363);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(85, 25);
            buttonOK.TabIndex = 3;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // panelSettings
            // 
            panelSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelSettings.Location = new Point(250, 10);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(338, 338);
            panelSettings.TabIndex = 5;
            // 
            // ChartSettingsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelSettings);
            Controls.Add(buttonOK);
            Controls.Add(buttonCancel);
            Controls.Add(treeView);
            MinimumSize = new Size(440, 150);
            Name = "ChartSettingsView";
            Size = new Size(600, 400);
            ResumeLayout(false);
        }

        #endregion

        private UI.Controls.TreeView treeView;
        private Button buttonCancel;
        private Button buttonOK;
        private Panel panelSettings;
    }
}
