namespace SquaresGame.View
{
    partial class GameStarter
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
            this.p1NameEdit = new System.Windows.Forms.TextBox();
            this.p1NameLabel = new System.Windows.Forms.Label();
            this.p1ColorBtn = new System.Windows.Forms.Button();
            this.p2ColorBtn = new System.Windows.Forms.Button();
            this.p2NameLabel = new System.Windows.Forms.Label();
            this.p2NameEdit = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.radio3 = new System.Windows.Forms.RadioButton();
            this.radio5 = new System.Windows.Forms.RadioButton();
            this.radio9 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // p1NameEdit
            // 
            this.p1NameEdit.Location = new System.Drawing.Point(52, 73);
            this.p1NameEdit.MaxLength = 10;
            this.p1NameEdit.Name = "p1NameEdit";
            this.p1NameEdit.Size = new System.Drawing.Size(129, 20);
            this.p1NameEdit.TabIndex = 0;
            this.p1NameEdit.Tag = "";
            // 
            // p1NameLabel
            // 
            this.p1NameLabel.AutoSize = true;
            this.p1NameLabel.Location = new System.Drawing.Point(49, 57);
            this.p1NameLabel.Name = "p1NameLabel";
            this.p1NameLabel.Size = new System.Drawing.Size(62, 13);
            this.p1NameLabel.TabIndex = 1;
            this.p1NameLabel.Text = "Player One:";
            // 
            // p1ColorBtn
            // 
            this.p1ColorBtn.Location = new System.Drawing.Point(52, 99);
            this.p1ColorBtn.Name = "p1ColorBtn";
            this.p1ColorBtn.Size = new System.Drawing.Size(129, 28);
            this.p1ColorBtn.TabIndex = 2;
            this.p1ColorBtn.Text = "Choose Color";
            this.p1ColorBtn.UseVisualStyleBackColor = true;
            this.p1ColorBtn.Click += new System.EventHandler(this.p1ColorBtn_Click);
            // 
            // p2ColorBtn
            // 
            this.p2ColorBtn.Location = new System.Drawing.Point(52, 195);
            this.p2ColorBtn.Name = "p2ColorBtn";
            this.p2ColorBtn.Size = new System.Drawing.Size(129, 28);
            this.p2ColorBtn.TabIndex = 5;
            this.p2ColorBtn.Text = "Choose Color";
            this.p2ColorBtn.UseVisualStyleBackColor = true;
            this.p2ColorBtn.Click += new System.EventHandler(this.p2ColorBtn_Click);
            // 
            // p2NameLabel
            // 
            this.p2NameLabel.AutoSize = true;
            this.p2NameLabel.Location = new System.Drawing.Point(49, 153);
            this.p2NameLabel.Name = "p2NameLabel";
            this.p2NameLabel.Size = new System.Drawing.Size(62, 13);
            this.p2NameLabel.TabIndex = 4;
            this.p2NameLabel.Text = "Player One:";
            // 
            // p2NameEdit
            // 
            this.p2NameEdit.Location = new System.Drawing.Point(52, 169);
            this.p2NameEdit.MaxLength = 10;
            this.p2NameEdit.Name = "p2NameEdit";
            this.p2NameEdit.Size = new System.Drawing.Size(129, 20);
            this.p2NameEdit.TabIndex = 3;
            this.p2NameEdit.Tag = "";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(36, 281);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 6;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(128, 281);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // radio3
            // 
            this.radio3.AutoSize = true;
            this.radio3.Location = new System.Drawing.Point(36, 246);
            this.radio3.Name = "radio3";
            this.radio3.Size = new System.Drawing.Size(42, 17);
            this.radio3.TabIndex = 8;
            this.radio3.TabStop = true;
            this.radio3.Text = "3x3";
            this.radio3.UseVisualStyleBackColor = true;
            this.radio3.CheckedChanged += new System.EventHandler(this.radio3_CheckedChanged);
            // 
            // radio5
            // 
            this.radio5.AutoSize = true;
            this.radio5.Location = new System.Drawing.Point(97, 246);
            this.radio5.Name = "radio5";
            this.radio5.Size = new System.Drawing.Size(42, 17);
            this.radio5.TabIndex = 9;
            this.radio5.TabStop = true;
            this.radio5.Text = "5x5";
            this.radio5.UseVisualStyleBackColor = true;
            this.radio5.CheckedChanged += new System.EventHandler(this.radio5_CheckedChanged);
            // 
            // radio9
            // 
            this.radio9.AutoSize = true;
            this.radio9.Location = new System.Drawing.Point(161, 246);
            this.radio9.Name = "radio9";
            this.radio9.Size = new System.Drawing.Size(42, 17);
            this.radio9.TabIndex = 10;
            this.radio9.TabStop = true;
            this.radio9.Text = "9x9";
            this.radio9.UseVisualStyleBackColor = true;
            this.radio9.CheckedChanged += new System.EventHandler(this.radio9_CheckedChanged);
            // 
            // GameStarter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 316);
            this.Controls.Add(this.radio9);
            this.Controls.Add(this.radio5);
            this.Controls.Add(this.radio3);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.p2ColorBtn);
            this.Controls.Add(this.p2NameLabel);
            this.Controls.Add(this.p2NameEdit);
            this.Controls.Add(this.p1ColorBtn);
            this.Controls.Add(this.p1NameLabel);
            this.Controls.Add(this.p1NameEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameStarter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameStarter";
            this.Load += new System.EventHandler(this.GameStarter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox p1NameEdit;
        private System.Windows.Forms.Label p1NameLabel;
        private System.Windows.Forms.Button p1ColorBtn;
        private System.Windows.Forms.Button p2ColorBtn;
        private System.Windows.Forms.Label p2NameLabel;
        private System.Windows.Forms.TextBox p2NameEdit;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.RadioButton radio3;
        private System.Windows.Forms.RadioButton radio5;
        private System.Windows.Forms.RadioButton radio9;
    }
}