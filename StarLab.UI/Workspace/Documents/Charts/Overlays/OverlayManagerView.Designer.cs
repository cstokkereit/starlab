namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    partial class OverlayManagerView
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
            labelDescription = new Label();
            buttonOK = new Button();
            buttonCancel = new Button();
            listOverlays = new ListView();
            label1 = new Label();
            SuspendLayout();
            // 
            // labelDescription
            // 
            labelDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelDescription.Location = new Point(236, 28);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(334, 317);
            labelDescription.TabIndex = 9;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(388, 363);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(85, 25);
            buttonOK.TabIndex = 8;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(483, 363);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(85, 25);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // listOverlays
            // 
            listOverlays.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listOverlays.BackColor = SystemColors.Window;
            listOverlays.CheckBoxes = true;
            listOverlays.FullRowSelect = true;
            listOverlays.Location = new Point(10, 28);
            listOverlays.Name = "listOverlays";
            listOverlays.Size = new Size(220, 317);
            listOverlays.TabIndex = 6;
            listOverlays.UseCompatibleStateImageBehavior = false;
            listOverlays.View = View.List;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 10);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 10;
            label1.Text = "Overlays";
            // 
            // OverlayManagerView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelDescription);
            Controls.Add(listOverlays);
            Controls.Add(label1);
            Controls.Add(buttonOK);
            Controls.Add(buttonCancel);
            Name = "OverlayManagerView";
            Size = new Size(580, 400);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDescription;
        private Button buttonOK;
        private Button buttonCancel;
        private ListView listOverlays;
        private Label label1;
    }
}
