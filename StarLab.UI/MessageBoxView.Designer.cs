namespace StarLab.UI
{
    partial class MessageBoxView
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
            buttonRight = new Button();
            buttonCentre = new Button();
            buttonLeft = new Button();
            labelMessage = new Label();
            SuspendLayout();
            // 
            // buttonRight
            // 
            buttonRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonRight.Location = new Point(360, 75);
            buttonRight.Name = "buttonRight";
            buttonRight.Size = new Size(85, 25);
            buttonRight.TabIndex = 3;
            buttonRight.UseVisualStyleBackColor = true;
            buttonRight.Click += OnButtonClick;
            // 
            // buttonCentre
            // 
            buttonCentre.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCentre.Location = new Point(269, 75);
            buttonCentre.Name = "buttonCentre";
            buttonCentre.Size = new Size(85, 25);
            buttonCentre.TabIndex = 2;
            buttonCentre.UseVisualStyleBackColor = true;
            buttonCentre.Click += OnButtonClick;
            // 
            // buttonLeft
            // 
            buttonLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonLeft.Location = new Point(178, 75);
            buttonLeft.Name = "buttonLeft";
            buttonLeft.Size = new Size(85, 25);
            buttonLeft.TabIndex = 1;
            buttonLeft.UseVisualStyleBackColor = true;
            buttonLeft.Click += OnButtonClick;
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Location = new Point(60, 15);
            labelMessage.MaximumSize = new Size(385, 0);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(77, 15);
            labelMessage.TabIndex = 4;
            labelMessage.Text = "Message Text";
            // 
            // MessageBoxView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(459, 116);
            Controls.Add(labelMessage);
            Controls.Add(buttonLeft);
            Controls.Add(buttonCentre);
            Controls.Add(buttonRight);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MessageBoxView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "MessageBoxView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonRight;
        private Button buttonCentre;
        private Button buttonLeft;
        private Label labelMessage;
    }
}