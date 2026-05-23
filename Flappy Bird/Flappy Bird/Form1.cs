using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Flappy_Bird
{
    public partial class Form1 : Form
    {
        // --- Физика и состояние птицы ---
        private float birdY = 250;
        private float birdVelocity = 0;
        private float gravity = 0.6f;
        private const int BirdSize = 30;

        // --- Игровая логика ---
        private int score = 0;
        private bool isGameOver = false;
        private Random rnd = new Random();
        private List<Pipe> pipes = new List<Pipe>();
        private int highScore = 0;
        private string highScoreFile = "highscore.txt";

        // --- Уровни сложности ---
        private int currentLevel = 1;
        private int pipeSpeed = 4;
        private int currentGap = 160;

        // --- Таймер ---
        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();

            // Загрузка рекорда
            if (System.IO.File.Exists(highScoreFile))
            {
                string savedScore = System.IO.File.ReadAllText(highScoreFile);
                int.TryParse(savedScore, out highScore);
            }

            // Настройка окна
            this.Text = "Flappy Bird: Курсовая работа";
            this.ClientSize = new Size(400, 600); // Оптимальный размер для Flappy
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            gameTimer.Interval = 20;
            gameTimer.Tick += UpdateGame;

            ResetGame();
        }

        private void ResetGame()
        {
            birdY = 250;
            birdVelocity = 0;
            score = 0;
            currentLevel = 1;
            pipeSpeed = 4;
            currentGap = 160;
            isGameOver = false;

            pipes.Clear();
            SpawnPipe(400);

            gameTimer.Start();
        }

        private void SpawnPipe(int startX)
        {
            int minHeight = 50;
            int maxHeight = this.ClientSize.Height - currentGap - 100;
            int topHeight = rnd.Next(minHeight, maxHeight);
            pipes.Add(new Pipe(startX, topHeight, currentGap));
        }

        private void UpdateGame(object sender, EventArgs e)
        {
            birdVelocity += gravity;
            birdY += birdVelocity;

            UpdateDifficulty();

            for (int i = pipes.Count - 1; i >= 0; i--)
            {
                pipes[i].X -= pipeSpeed;

                if (!pipes[i].Passed && pipes[i].X < 50)
                {
                    score++;
                    pipes[i].Passed = true;
                    SpawnPipe(this.ClientSize.Width + 50);
                }

                if (pipes[i].X < -100) pipes.RemoveAt(i);
            }

            CheckCollisions();
            this.Invalidate();
        }

        private void UpdateDifficulty()
        {
            currentLevel = (score / 5) + 1;
            if (currentLevel == 1) { pipeSpeed = 4; currentGap = 160; }
            else if (currentLevel == 2) { pipeSpeed = 6; currentGap = 140; }
            else if (currentLevel == 3) { pipeSpeed = 8; currentGap = 125; }
            else if (currentLevel == 4) { pipeSpeed = 10; currentGap = 110; }
            else { pipeSpeed = 12; currentGap = 100; }
        }

        private void CheckCollisions()
        {
            if (birdY <= 0 || birdY + BirdSize >= this.ClientSize.Height) GameOver();

            Rectangle birdRect = new Rectangle(50, (int)birdY, BirdSize, BirdSize);
            foreach (var p in pipes)
            {
                Rectangle topPipe = new Rectangle(p.X, 0, 60, p.TopHeight);
                Rectangle bottomPipe = new Rectangle(p.X, p.TopHeight + p.Gap, 60, this.Height);

                if (birdRect.IntersectsWith(topPipe) || birdRect.IntersectsWith(bottomPipe))
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();
            isGameOver = true;

            // Логика рекорда перенесена сюда (правильное место)
            if (score > highScore)
            {
                highScore = score;
                System.IO.File.WriteAllText(highScoreFile, highScore.ToString());
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (isGameOver) ResetGame();
                else birdVelocity = -9;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Color skyColor = (currentLevel % 2 == 0) ? Color.LightSteelBlue : Color.SkyBlue;
            g.Clear(skyColor);

            foreach (var p in pipes)
            {
                Brush pipeBrush = currentLevel >= 4 ? Brushes.Firebrick : Brushes.ForestGreen;
                g.FillRectangle(pipeBrush, p.X, 0, 60, p.TopHeight);
                g.FillRectangle(pipeBrush, p.X, p.TopHeight + p.Gap, 60, this.Height);
                g.DrawRectangle(Pens.Black, p.X, 0, 60, p.TopHeight);
                g.DrawRectangle(Pens.Black, p.X, p.TopHeight + p.Gap, 60, this.Height);
            }

            g.FillEllipse(Brushes.Yellow, 50, birdY, BirdSize, BirdSize);
            g.DrawEllipse(Pens.Black, 50, birdY, BirdSize, BirdSize);

            using (Font uiFont = new Font("Arial", 12, FontStyle.Bold))
            {
                g.DrawString($"Очки: {score}", uiFont, Brushes.Black, 10, 10);
                g.DrawString($"Уровень: {currentLevel}", uiFont, Brushes.DarkBlue, 10, 30);
                g.DrawString($"Рекорд: {highScore}", uiFont, Brushes.DarkRed, 10, 50);
            }

            if (isGameOver)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.Black)), 0, 0, this.Width, this.Height);
                using (Font overFont = new Font("Arial", 24, FontStyle.Bold))
                {
                    g.DrawString("ИГРА ОКОНЧЕНА", overFont, Brushes.White, 50, 200);
                    g.DrawString($"Ваш счет: {score}", new Font("Arial", 16), Brushes.Gold, 120, 250);
                    g.DrawString("Пробел - Рестарт", new Font("Arial", 14), Brushes.White, 110, 300);
                }
            }
        }
    }

    public class Pipe
    {
        public int X { get; set; }
        public int TopHeight { get; set; }
        public int Gap { get; set; }
        public bool Passed { get; set; } = false;

        public Pipe(int x, int topHeight, int gap)
        {
            X = x;
            TopHeight = topHeight;
            Gap = gap;
        }
    }
}