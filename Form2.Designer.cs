namespace Yggdrasil
{
    partial class Form2
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
            this.yBtn = new System.Windows.Forms.Button();
            this.nBtn = new System.Windows.Forms.Button();
            this.txtLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // yBtn
            // 
            this.yBtn.BackColor = System.Drawing.Color.Transparent;
            this.yBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(138)))), ((int)(((byte)(105)))));
            this.yBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.yBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yBtn.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(241)))), ((int)(((byte)(227)))));
            this.yBtn.Location = new System.Drawing.Point(164, 269);
            this.yBtn.Name = "yBtn";
            this.yBtn.Size = new System.Drawing.Size(107, 40);
            this.yBtn.TabIndex = 0;
            this.yBtn.Text = "Ja";
            this.yBtn.UseVisualStyleBackColor = false;
            this.yBtn.Click += new System.EventHandler(this.yBtn_Click);
            // 
            // nBtn
            // 
            this.nBtn.BackColor = System.Drawing.Color.Transparent;
            this.nBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(138)))), ((int)(((byte)(105)))));
            this.nBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nBtn.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(241)))), ((int)(((byte)(227)))));
            this.nBtn.Location = new System.Drawing.Point(329, 269);
            this.nBtn.Name = "nBtn";
            this.nBtn.Size = new System.Drawing.Size(107, 40);
            this.nBtn.TabIndex = 1;
            this.nBtn.Text = "Nein";
            this.nBtn.UseVisualStyleBackColor = false;
            this.nBtn.Click += new System.EventHandler(this.nBtn_Click);
            // 
            // txtLbl
            // 
            this.txtLbl.AutoSize = true;
            this.txtLbl.BackColor = System.Drawing.Color.Transparent;
            this.txtLbl.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(241)))), ((int)(((byte)(227)))));
            this.txtLbl.Location = new System.Drawing.Point(176, 145);
            this.txtLbl.Name = "txtLbl";
            this.txtLbl.Size = new System.Drawing.Size(248, 56);
            this.txtLbl.TabIndex = 2;
            this.txtLbl.Text = "Möchtest du das Spiel\r\nwirklich beenden?";
            this.txtLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.BackgroundImage = global::Yggdrasil.Properties.Resources.custom_frame;
            this.ClientSize = new System.Drawing.Size(600, 447);
            this.Controls.Add(this.txtLbl);
            this.Controls.Add(this.nBtn);
            this.Controls.Add(this.yBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form2";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button yBtn;
        private System.Windows.Forms.Button nBtn;
        private System.Windows.Forms.Label txtLbl;
    }
}