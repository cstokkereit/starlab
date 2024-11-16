namespace StarLab.Application.Workspace.Documents
{
    partial class AddDocumentView
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
            buttonAdd = new Button();
            buttonCancel = new Button();
            label1 = new Label();
            textDescription = new RichTextBox();
            listDocumentTypes = new ListView();
            imageList = new ImageList(components);
            textName = new TextBox();
            SuspendLayout();
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonAdd.Location = new Point(550, 496);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(85, 25);
            buttonAdd.TabIndex = 5;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(645, 496);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(85, 25);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(10, 473);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 6;
            label1.Text = "Name:";
            // 
            // textDescription
            // 
            textDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            textDescription.BackColor = SystemColors.ControlLight;
            textDescription.BorderStyle = BorderStyle.None;
            textDescription.Location = new Point(430, 31);
            textDescription.Name = "textDescription";
            textDescription.Size = new Size(300, 426);
            textDescription.TabIndex = 7;
            textDescription.Text = "";
            // 
            // listDocumentTypes
            // 
            listDocumentTypes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listDocumentTypes.BorderStyle = BorderStyle.None;
            listDocumentTypes.FullRowSelect = true;
            listDocumentTypes.HeaderStyle = ColumnHeaderStyle.None;
            listDocumentTypes.Location = new Point(68, 31);
            listDocumentTypes.MultiSelect = false;
            listDocumentTypes.Name = "listDocumentTypes";
            listDocumentTypes.Size = new Size(362, 426);
            listDocumentTypes.SmallImageList = imageList;
            listDocumentTypes.TabIndex = 8;
            listDocumentTypes.UseCompatibleStateImageBehavior = false;
            listDocumentTypes.View = View.Details;
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(32, 32);
            imageList.TransparentColor = Color.Transparent;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(68, 470);
            textName.Name = "textName";
            textName.Size = new Size(362, 23);
            textName.TabIndex = 9;
            // 
            // AddDocumentView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textName);
            Controls.Add(listDocumentTypes);
            Controls.Add(textDescription);
            Controls.Add(label1);
            Controls.Add(buttonAdd);
            Controls.Add(buttonCancel);
            Name = "AddDocumentView";
            Size = new Size(742, 533);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAdd;
        private Button buttonCancel;
        private Label label1;
        private RichTextBox textDescription;
        private ListView listDocumentTypes;
        private TextBox textName;
        private ImageList imageList;
    }
}
