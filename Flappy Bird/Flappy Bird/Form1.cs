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

        // Визуальный размер птицы на экране (сделали крупнее!)
        private const int BirdVisualSize = 55;
        // Реальный размер хитбокса (чуть меньше визуального, чтобы прощать игроку мелкие задевания)
        private const int BirdHitboxSize = 40;

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

        // --- Отрисовка текстур ---
        private Image topPipe = Properties.Resources.pipe_top;
        private Image bottomPipe = Properties.Resources.pipe_bottom;
        private Image birdImg = Properties.Resources.bird;
        private Image BackGround = Properties.Resources.background;

        public Form1()
        {
            InitializeComponent();

            if (System.IO.File.Exists(highScoreFile))
            {
                string savedScore = System.IO.File.ReadAllText(highScoreFile);
                int.TryParse(savedScore, out highScore);
            }

            this.Text = "Flappy Bird: Курсовая работа";
            this.ClientSize = new Size(400, 600);
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
            // Проверка столкновения с полом и потолком с учетом нового размера
            if (birdY <= 0 || birdY + BirdHitboxSize >= this.ClientSize.Height) GameOver();

            // Точный хитбокс птицы, отцентрированный относительно рисунка
            Rectangle birdRect = new Rectangle(50 + (BirdVisualSize - BirdHitboxSize) / 2, (int)birdY + (BirdVisualSize - BirdHitboxSize) / 2, BirdHitboxSize, BirdHitboxSize);

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

            // Масштабируем пиксели БЕЗ размытия (сохраняем Pixel Art четким)
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            // 1. Небо
            try
            {
                g.DrawImage(BackGround, 0, 0, 400, 600);
            }
            catch
            {
                Color skyColor = (currentLevel % 2 == 0) ? Color.LightSteelBlue : Color.SkyBlue;
                g.Clear(skyColor);
            }

            // 2. Трубы
            foreach (var p in pipes)
            {
                try
                {

                    int topPipeY = p.TopHeight - 400;
                    g.DrawImage(topPipe, p.X, topPipeY, 60, 400);

                    // ФИКС НИЖНЕЙ ТРУБЫ (оставляем рабочим):
                    int botPipeStart = p.TopHeight + p.Gap;
                    g.DrawImage(bottomPipe, p.X, botPipeStart, 60, 400);
                }
                catch
                {
                    Brush pipeBrush = currentLevel >= 4 ? Brushes.Firebrick : Brushes.ForestGreen;
                    g.FillRectangle(pipeBrush, p.X, 0, 60, p.TopHeight);
                    g.FillRectangle(pipeBrush, p.X, p.TopHeight + p.Gap, 60, this.Height);
                }
            }

            // 3. Твоя птичка (Увеличенная и плавная)
            try
            {
                System.Drawing.Drawing2D.GraphicsState state = g.Save();

                // Центрируем поворот ровно по новому большому размеру
                g.TranslateTransform(50 + BirdVisualSize / 2, birdY + BirdVisualSize / 2);

                float angle = birdVelocity * 5;
                if (angle < -30) angle = -30;
                if (angle > 70) angle = 70;

                g.RotateTransform(angle);

                // Рисуем твой спрайт, растягивая его до 55х55 пикселей
                g.DrawImage(birdImg, -BirdVisualSize / 2, -BirdVisualSize / 2, BirdVisualSize, BirdVisualSize);

                g.Restore(state);
            }
            catch
            {
                g.FillEllipse(Brushes.Yellow, 50, birdY, BirdHitboxSize, BirdHitboxSize);
                g.DrawEllipse(Pens.Black, 50, birdY, BirdHitboxSize, BirdHitboxSize);
            }

            // 4. Интерфейс
            using (Font uiFont = new Font("Arial", 24, FontStyle.Bold))
            {
                string scoreStr = $"Очки: {score}";
                string levelStr = $"Уровень: {currentLevel}";
                string recordStr = $"Рекорд: {highScore}";

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Рисуем "Очки"
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddString(scoreStr, uiFont.FontFamily, (int)uiFont.Style, uiFont.Size, new Point(10, 10), StringFormat.GenericDefault);
                    g.DrawPath(new Pen(Color.White, 4) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round }, path);
                    g.FillPath(Brushes.Black, path);
                }

                // Рисуем "Уровень"
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddString(levelStr, uiFont.FontFamily, (int)uiFont.Style, uiFont.Size, new Point(10, 35), StringFormat.GenericDefault);
                    g.DrawPath(new Pen(Color.White, 4) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round }, path);
                    g.FillPath(Brushes.DarkBlue, path);
                }

                // Рисуем "Рекорд"
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddString(recordStr, uiFont.FontFamily, (int)uiFont.Style, uiFont.Size, new Point(10, 60), StringFormat.GenericDefault);
                    g.DrawPath(new Pen(Color.White, 4) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round }, path);
                    g.FillPath(Brushes.DarkRed, path);
                }

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
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