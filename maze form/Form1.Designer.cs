namespace maze_form
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.start1 = new maze_form.Start();
            this.problem_21 = new maze_form.Problem_2();
            this.problem_11 = new maze_form.Problem_1();
            this.SuspendLayout();
            // 
            // start1
            // 
            this.start1.Location = new System.Drawing.Point(-3, -4);
            this.start1.Name = "start1";
            this.start1.Size = new System.Drawing.Size(989, 569);
            this.start1.TabIndex = 0;
            this.start1.Load += new System.EventHandler(this.start1_Load);
            // 
            // problem_21
            // 
            this.problem_21.AutoSize = true;
            this.problem_21.Location = new System.Drawing.Point(0, 0);
            this.problem_21.Name = "problem_21";
            this.problem_21.Size = new System.Drawing.Size(986, 541);
            this.problem_21.TabIndex = 1;
            this.problem_21.Visible = false;
            this.problem_21.Load += new System.EventHandler(this.problem_21_Load);
            // 
            // problem_11
            // 
            this.problem_11.AutoSize = true;
            this.problem_11.Location = new System.Drawing.Point(0, 0);
            this.problem_11.Name = "problem_11";
            this.problem_11.Size = new System.Drawing.Size(1403, 983);
            this.problem_11.TabIndex = 2;
            this.problem_11.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 536);
            this.Controls.Add(this.problem_11);
            this.Controls.Add(this.problem_21);
            this.Controls.Add(this.start1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Start start1;
        private Problem_2 problem_21;
        private Problem_1 problem_11;
    }
}