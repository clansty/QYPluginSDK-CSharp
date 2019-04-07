namespace QYPlugin
{
    partial class SetForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.getAppDir = new System.Windows.Forms.Button();
            this.getLoginQQ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 221);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 217);
            this.textBox1.TabIndex = 0;
            // 
            // getAppDir
            // 
            this.getAppDir.Location = new System.Drawing.Point(12, 12);
            this.getAppDir.Name = "getAppDir";
            this.getAppDir.Size = new System.Drawing.Size(75, 23);
            this.getAppDir.TabIndex = 1;
            this.getAppDir.Text = "应用目录";
            this.getAppDir.UseVisualStyleBackColor = true;
            this.getAppDir.Click += new System.EventHandler(this.GetAppDir);
            // 
            // getLoginQQ
            // 
            this.getLoginQQ.Location = new System.Drawing.Point(93, 12);
            this.getLoginQQ.Name = "getLoginQQ";
            this.getLoginQQ.Size = new System.Drawing.Size(75, 23);
            this.getLoginQQ.TabIndex = 2;
            this.getLoginQQ.Text = "登录QQ";
            this.getLoginQQ.UseVisualStyleBackColor = true;
            this.getLoginQQ.Click += new System.EventHandler(this.GetLoginQQ);
            // 
            // SetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.getLoginQQ);
            this.Controls.Add(this.getAppDir);
            this.Controls.Add(this.textBox1);
            this.Name = "SetForm";
            this.Text = "SetForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button getAppDir;
        private System.Windows.Forms.Button getLoginQQ;
    }
}