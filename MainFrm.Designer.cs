namespace Katalogi
{
    partial class MainFrm
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
            this.LogTB = new System.Windows.Forms.TextBox();
            this.SavePagesBtn = new System.Windows.Forms.Button();
            this.SaveTabTreeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LogTB
            // 
            this.LogTB.Location = new System.Drawing.Point(12, 12);
            this.LogTB.Multiline = true;
            this.LogTB.Name = "LogTB";
            this.LogTB.Size = new System.Drawing.Size(664, 551);
            this.LogTB.TabIndex = 0;
            // 
            // SavePagesBtn
            // 
            this.SavePagesBtn.Location = new System.Drawing.Point(12, 570);
            this.SavePagesBtn.Name = "SavePagesBtn";
            this.SavePagesBtn.Size = new System.Drawing.Size(130, 23);
            this.SavePagesBtn.TabIndex = 1;
            this.SavePagesBtn.Text = "Save Pages (0)";
            this.SavePagesBtn.UseVisualStyleBackColor = true;
            this.SavePagesBtn.Click += new System.EventHandler(this.SavePagesBtn_Click);
            // 
            // SaveTabTreeBtn
            // 
            this.SaveTabTreeBtn.Location = new System.Drawing.Point(148, 570);
            this.SaveTabTreeBtn.Name = "SaveTabTreeBtn";
            this.SaveTabTreeBtn.Size = new System.Drawing.Size(130, 23);
            this.SaveTabTreeBtn.TabIndex = 2;
            this.SaveTabTreeBtn.Text = "Save Tab Tree";
            this.SaveTabTreeBtn.UseVisualStyleBackColor = true;
            this.SaveTabTreeBtn.Click += new System.EventHandler(this.SaveTabTreeBtn_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 596);
            this.Controls.Add(this.SaveTabTreeBtn);
            this.Controls.Add(this.SavePagesBtn);
            this.Controls.Add(this.LogTB);
            this.Name = "MainFrm";
            this.ShowIcon = false;
            this.Text = "Katalogi - By Quackster";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogTB;
        private System.Windows.Forms.Button SavePagesBtn;
        private System.Windows.Forms.Button SaveTabTreeBtn;
    }
}

