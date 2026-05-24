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
            btnPlay = new Button();
            btnAbout = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(128, 255, 128);
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnPlay.Location = new Point(97, 147);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(175, 56);
            btnPlay.TabIndex = 0;
            btnPlay.Text = "Играть";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            // 
            // btnAbout
            // 
            btnAbout.BackColor = Color.FromArgb(128, 255, 128);
            btnAbout.FlatStyle = FlatStyle.Flat;
            btnAbout.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold);
            btnAbout.Location = new Point(97, 227);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(175, 56);
            btnAbout.TabIndex = 1;
            btnAbout.Text = "О игре";
            btnAbout.UseVisualStyleBackColor = false;
            btnAbout.Click += btnAbout_Click;
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
            Controls.Add(btnAbout);
            Controls.Add(btnPlay);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
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