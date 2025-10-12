namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    partial class FontSection
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
            comboFontFamilies = new ComboBox();
            label1 = new Label();
            checkBoxBold = new CheckBox();
            checkBoxItalic = new CheckBox();
            checkBoxUnderline = new CheckBox();
            label2 = new Label();
            comboFontSizes = new ComboBox();
            SuspendLayout();
            // 
            // comboFontFamilies
            // 
            comboFontFamilies.FormattingEnabled = true;
            comboFontFamilies.Location = new Point(37, 3);
            comboFontFamilies.Name = "comboFontFamilies";
            comboFontFamilies.Size = new Size(215, 23);
            comboFontFamilies.TabIndex = 0;
            comboFontFamilies.DropDown += OnDropDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 6);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "Font";
            // 
            // checkBoxBold
            // 
            checkBoxBold.AutoSize = true;
            checkBoxBold.Location = new Point(37, 38);
            checkBoxBold.Name = "checkBoxBold";
            checkBoxBold.Size = new Size(50, 19);
            checkBoxBold.TabIndex = 2;
            checkBoxBold.Text = "Bold";
            checkBoxBold.UseVisualStyleBackColor = true;
            // 
            // checkBoxItalic
            // 
            checkBoxItalic.AutoSize = true;
            checkBoxItalic.Location = new Point(103, 38);
            checkBoxItalic.Name = "checkBoxItalic";
            checkBoxItalic.Size = new Size(51, 19);
            checkBoxItalic.TabIndex = 3;
            checkBoxItalic.Text = "Italic";
            checkBoxItalic.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnderline
            // 
            checkBoxUnderline.AutoSize = true;
            checkBoxUnderline.Location = new Point(170, 38);
            checkBoxUnderline.Name = "checkBoxUnderline";
            checkBoxUnderline.Size = new Size(77, 19);
            checkBoxUnderline.TabIndex = 4;
            checkBoxUnderline.Text = "Underline";
            checkBoxUnderline.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(269, 6);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 5;
            label2.Text = "Size";
            // 
            // comboFontSizes
            // 
            comboFontSizes.FormattingEnabled = true;
            comboFontSizes.Location = new Point(302, 3);
            comboFontSizes.Name = "comboFontSizes";
            comboFontSizes.Size = new Size(45, 23);
            comboFontSizes.TabIndex = 6;
            // 
            // FontSection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(comboFontSizes);
            Controls.Add(label2);
            Controls.Add(checkBoxUnderline);
            Controls.Add(checkBoxItalic);
            Controls.Add(checkBoxBold);
            Controls.Add(label1);
            Controls.Add(comboFontFamilies);
            Name = "FontSection";
            Size = new Size(350, 56);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboFontFamilies;
        private Label label1;
        private CheckBox checkBoxBold;
        private CheckBox checkBoxItalic;
        private CheckBox checkBoxUnderline;
        private Label label2;
        private ComboBox comboFontSizes;
    }
}
