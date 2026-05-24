namespace Flappy_Bird
{
    partial class MenuForm
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
            this.btnPlay = new Button();
            this.btnAbout = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = Color.FromArgb(128, 255, 128);
            this.btnPlay.FlatStyle = FlatStyle.Flat;
            this.btnPlay.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            this.btnPlay.Location = new Point(97, 147);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new Size(175, 56);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Играть";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += this.btnPlay_Click;
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = Color.FromArgb(128, 255, 128);
            this.btnAbout.FlatStyle = FlatStyle.Flat;
            this.btnAbout.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold);
            this.btnAbout.Location = new Point(97, 227);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new Size(175, 56);
            this.btnAbout.TabIndex = 1;
            this.btnAbout.Text = "О игре";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += this.btnAbout_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(128, 255, 128);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold);
            btnExit.Location = new Point(97, 305);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(175, 56);
            btnExit.TabIndex = 2;
            btnExit.Text = "Выйти";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(382, 553);
            Controls.Add(btnExit);
            Controls.Add(this.btnAbout);
            Controls.Add(this.btnPlay);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flappy Bird: Меню";
            ResumeLayout(false);
        }

        #endregion

        private Button btnPlay;
        private Button btnAbout;
        private Button btnExit;
    }
}