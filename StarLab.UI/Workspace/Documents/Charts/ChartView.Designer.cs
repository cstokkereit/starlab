namespace StarLab.UI.Workspace.Documents.Charts
{
    partial class ChartView
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
            chart = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // formsPlot
            // 
            chart.Dock = DockStyle.Fill;
            chart.Location = new Point(0, 0);
            chart.Name = "formsPlot";
            chart.Size = new Size(625, 570);
            chart.TabIndex = 0;
            // 
            // ChartView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(chart);
            Name = "ChartView";
            Size = new Size(625, 570);
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot chart;
    }
}
