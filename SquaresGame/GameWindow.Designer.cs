namespace SquaresGame
{
    partial class GameWindow
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
            this.canvas = new System.Windows.Forms.Panel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.p2PointLabel = new System.Windows.Forms.Label();
            this.p1PointLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.saveGameBtn = new System.Windows.Forms.Button();
            this.newGameBtn = new System.Windows.Forms.Button();
            this.p2NameLabel = new System.Windows.Forms.Label();
            this.p1NameLabel = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.Info;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.canvas.Location = new System.Drawing.Point(0, 161);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(800, 800);
            this.canvas.TabIndex = 0;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.Window;
            this.topPanel.Controls.Add(this.p2PointLabel);
            this.topPanel.Controls.Add(this.p1PointLabel);
            this.topPanel.Controls.Add(this.button1);
            this.topPanel.Controls.Add(this.saveGameBtn);
            this.topPanel.Controls.Add(this.newGameBtn);
            this.topPanel.Controls.Add(this.p2NameLabel);
            this.topPanel.Controls.Add(this.p1NameLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(800, 164);
            this.topPanel.TabIndex = 1;
            // 
            // p2PointLabel
            // 
            this.p2PointLabel.AutoSize = true;
            this.p2PointLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.p2PointLabel.Location = new System.Drawing.Point(600, 60);
            this.p2PointLabel.Name = "p2PointLabel";
            this.p2PointLabel.Size = new System.Drawing.Size(51, 55);
            this.p2PointLabel.TabIndex = 6;
            this.p2PointLabel.Text = "0";
            // 
            // p1PointLabel
            // 
            this.p1PointLabel.AutoSize = true;
            this.p1PointLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.p1PointLabel.Location = new System.Drawing.Point(148, 60);
            this.p1PointLabel.Name = "p1PointLabel";
            this.p1PointLabel.Size = new System.Drawing.Size(51, 55);
            this.p1PointLabel.TabIndex = 5;
            this.p1PointLabel.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "Load Game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveGameBtn
            // 
            this.saveGameBtn.Enabled = false;
            this.saveGameBtn.Location = new System.Drawing.Point(349, 60);
            this.saveGameBtn.Name = "saveGameBtn";
            this.saveGameBtn.Size = new System.Drawing.Size(120, 42);
            this.saveGameBtn.TabIndex = 3;
            this.saveGameBtn.Text = "Save Game";
            this.saveGameBtn.UseVisualStyleBackColor = true;
            // 
            // newGameBtn
            // 
            this.newGameBtn.Location = new System.Drawing.Point(349, 12);
            this.newGameBtn.Name = "newGameBtn";
            this.newGameBtn.Size = new System.Drawing.Size(120, 42);
            this.newGameBtn.TabIndex = 2;
            this.newGameBtn.Text = "New Game";
            this.newGameBtn.UseVisualStyleBackColor = true;
            this.newGameBtn.Click += new System.EventHandler(this.newGameBtn_Click);
            // 
            // p2NameLabel
            // 
            this.p2NameLabel.AutoSize = true;
            this.p2NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.p2NameLabel.Location = new System.Drawing.Point(555, 9);
            this.p2NameLabel.Name = "p2NameLabel";
            this.p2NameLabel.Size = new System.Drawing.Size(145, 42);
            this.p2NameLabel.TabIndex = 1;
            this.p2NameLabel.Text = "Player2";
            // 
            // p1NameLabel
            // 
            this.p1NameLabel.AutoSize = true;
            this.p1NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.p1NameLabel.Location = new System.Drawing.Point(105, 9);
            this.p1NameLabel.Name = "p1NameLabel";
            this.p1NameLabel.Size = new System.Drawing.Size(145, 42);
            this.p1NameLabel.TabIndex = 0;
            this.p1NameLabel.Text = "Player1";
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 961);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.canvas);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Squares Game";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button saveGameBtn;
        private System.Windows.Forms.Button newGameBtn;
        private System.Windows.Forms.Label p2NameLabel;
        private System.Windows.Forms.Label p1NameLabel;
        private System.Windows.Forms.Label p2PointLabel;
        private System.Windows.Forms.Label p1PointLabel;
    }
}

