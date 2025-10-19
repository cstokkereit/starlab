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
            buttonBackground = new Button();
            comboForeground = new ComboBox();
            comboBackground = new ComboBox();
            buttonForeground = new Button();
            label1 = new Label();
            label2 = new Label();
            dialogCustomColour = new ColorDialog();
            SuspendLayout();
            // 
            // buttonBackground
            // 
            buttonBackground.Location = new Point(169, 75);
            buttonBackground.Name = "buttonBackground";
            buttonBackground.Size = new Size(85, 23);
            buttonBackground.TabIndex = 2;
            buttonBackground.Text = "Custom...";
            buttonBackground.UseVisualStyleBackColor = true;
            buttonBackground.Click += OnClick;
            // 
            // comboForeground
            // 
            comboForeground.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboForeground.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboForeground.FormattingEnabled = true;
            comboForeground.Location = new Point(3, 18);
            comboForeground.Name = "comboForeground";
            comboForeground.Size = new Size(159, 23);
            comboForeground.TabIndex = 3;
            // 
            // comboBackground
            // 
            comboBackground.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBackground.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBackground.FormattingEnabled = true;
            comboBackground.Location = new Point(3, 75);
            comboBackground.Name = "comboBackground";
            comboBackground.Size = new Size(160, 23);
            comboBackground.TabIndex = 4;
            // 
            // buttonForeground
            // 
            buttonForeground.Location = new Point(168, 18);
            buttonForeground.Name = "buttonForeground";
            buttonForeground.Size = new Size(85, 23);
            buttonForeground.TabIndex = 5;
            buttonForeground.Text = "Custom...";
            buttonForeground.UseVisualStyleBackColor = true;
            buttonForeground.Click += OnClick;
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
            Controls.Add(buttonForeground);
            Controls.Add(comboBackground);
            Controls.Add(comboForeground);
            Controls.Add(buttonBackground);
            Controls.Add(label1);
            Controls.Add(label2);
            Name = "ColourSection";
            Size = new Size(258, 100);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonBackground;
        private ComboBox comboForeground;
        private ComboBox comboBackground;
        private Button buttonForeground;
        private Label label1;
        private Label label2;
        private ColorDialog dialogCustomColour;
    }
}
