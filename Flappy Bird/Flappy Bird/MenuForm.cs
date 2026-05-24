using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1();
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Flappy Bird: Special Edition\n\n" +
                "Разработчики:\n" +
                "- Slime (Графика и код)\n" +
                "- DASH (Идея и вдохновение)\n\n" +
                "Управление:\n" +
                "Пробел — Прыжок и Рестарт.\n\n" +
                "Удачи в достижении новых рекордов!", 
                "Об игре");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
