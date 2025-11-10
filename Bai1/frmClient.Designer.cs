namespace Bai1
{
    partial class frmClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Remote host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(506, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // textBoxIP
            // 
            this.textBoxIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxIP.Location = new System.Drawing.Point(69, 80);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(280, 31);
            this.textBoxIP.TabIndex = 2;
            // 
            // textBoxPort
            // 
            this.textBoxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPort.Location = new System.Drawing.Point(511, 80);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(151, 31);
            this.textBoxPort.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Message";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(69, 353);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(111, 46);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(69, 175);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(593, 156);
            this.textBoxMessage.TabIndex = 7;
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 452);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmClient";
            this.Text = "UDP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox textBoxMessage;
    }
}