namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    partial class ColourSection
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
            buttonCustom2 = new Button();
            comboBoxForeground = new ComboBox();
            comboBoxBackground = new ComboBox();
            buttonCustom1 = new Button();
            label1 = new Label();
            label2 = new Label();
            dialogCustomColour = new ColorDialog();
            SuspendLayout();
            // 
            // buttonCustom2
            // 
            buttonCustom2.Location = new Point(169, 75);
            buttonCustom2.Name = "buttonCustom2";
            buttonCustom2.Size = new Size(85, 23);
            buttonCustom2.TabIndex = 2;
            buttonCustom2.Text = "Custom...";
            buttonCustom2.UseVisualStyleBackColor = true;
            // 
            // comboBoxForeground
            // 
            comboBoxForeground.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxForeground.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxForeground.FormattingEnabled = true;
            comboBoxForeground.Location = new Point(3, 18);
            comboBoxForeground.Name = "comboBoxForeground";
            comboBoxForeground.Size = new Size(159, 23);
            comboBoxForeground.TabIndex = 3;
            // 
            // comboBoxBackground
            // 
            comboBoxBackground.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxBackground.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxBackground.FormattingEnabled = true;
            comboBoxBackground.Location = new Point(3, 75);
            comboBoxBackground.Name = "comboBoxBackground";
            comboBoxBackground.Size = new Size(160, 23);
            comboBoxBackground.TabIndex = 4;
            // 
            // buttonCustom1
            // 
            buttonCustom1.Location = new Point(168, 18);
            buttonCustom1.Name = "buttonCustom1";
            buttonCustom1.Size = new Size(85, 23);
            buttonCustom1.TabIndex = 5;
            buttonCustom1.Text = "Custom...";
            buttonCustom1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 6;
            label1.Text = "Foreground Colour";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 57);
            label2.Name = "label2";
            label2.Size = new Size(110, 15);
            label2.TabIndex = 7;
            label2.Text = "Background Colour";
            // 
            // ColourSection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonCustom1);
            Controls.Add(comboBoxBackground);
            Controls.Add(comboBoxForeground);
            Controls.Add(buttonCustom2);
            Controls.Add(label1);
            Controls.Add(label2);
            Name = "ColourSection";
            Size = new Size(258, 100);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonCustom2;
        private ComboBox comboBoxForeground;
        private ComboBox comboBoxBackground;
        private Button buttonCustom1;
        private Label label1;
        private Label label2;
        private ColorDialog dialogCustomColour;
    }
}
