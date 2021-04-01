namespace Chip_Chess_Engine
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.exit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.min = new System.Windows.Forms.Button();
            this.fullScrn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (25)))), ((int) (((byte) (25)))), ((int) (((byte) (25)))));
            this.exit.Dock = System.Windows.Forms.DockStyle.Right;
            this.exit.FlatAppearance.BorderSize = 0;
            this.exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int) (((byte) (255)))), ((int) (((byte) (103)))), ((int) (((byte) (92)))));
            this.exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int) (((byte) (255)))), ((int) (((byte) (103)))), ((int) (((byte) (92)))));
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.ForeColor = System.Drawing.Color.White;
            this.exit.Location = new System.Drawing.Point(574, 0);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(26, 23);
            this.exit.TabIndex = 0;
            this.exit.Text = "✕";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (25)))), ((int) (((byte) (25)))), ((int) (((byte) (25)))));
            this.panel1.Controls.Add(this.min);
            this.panel1.Controls.Add(this.fullScrn);
            this.panel1.Controls.Add(this.exit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 23);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // min
            // 
            this.min.Dock = System.Windows.Forms.DockStyle.Right;
            this.min.FlatAppearance.BorderSize = 0;
            this.min.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int) (((byte) (39)))), ((int) (((byte) (41)))), ((int) (((byte) (43)))));
            this.min.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int) (((byte) (39)))), ((int) (((byte) (41)))), ((int) (((byte) (43)))));
            this.min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min.ForeColor = System.Drawing.Color.White;
            this.min.Location = new System.Drawing.Point(522, 0);
            this.min.Name = "min";
            this.min.Size = new System.Drawing.Size(26, 23);
            this.min.TabIndex = 1;
            this.min.Text = "—";
            this.min.UseVisualStyleBackColor = true;
            this.min.Click += new System.EventHandler(this.min_Click);
            // 
            // fullScrn
            // 
            this.fullScrn.Dock = System.Windows.Forms.DockStyle.Right;
            this.fullScrn.FlatAppearance.BorderSize = 0;
            this.fullScrn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int) (((byte) (39)))), ((int) (((byte) (41)))), ((int) (((byte) (43)))));
            this.fullScrn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int) (((byte) (39)))), ((int) (((byte) (41)))), ((int) (((byte) (43)))));
            this.fullScrn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fullScrn.ForeColor = System.Drawing.Color.White;
            this.fullScrn.Location = new System.Drawing.Point(548, 0);
            this.fullScrn.Name = "fullScrn";
            this.fullScrn.Size = new System.Drawing.Size(26, 23);
            this.fullScrn.TabIndex = 2;
            this.fullScrn.Text = "❐";
            this.fullScrn.UseVisualStyleBackColor = true;
            this.fullScrn.Click += new System.EventHandler(this.fullScrn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (31)))), ((int) (((byte) (31)))), ((int) (((byte) (31)))));
            this.ClientSize = new System.Drawing.Size(600, 440);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button min;
        private System.Windows.Forms.Button fullScrn;

        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.Button exit;

        #endregion
    }
}