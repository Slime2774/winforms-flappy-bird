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
        // Храним состояние звука на уровне меню
        private bool isSoundMuted = false;

        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            // ПЕРЕДАЕМ состояние звука в игровую форму при старте
            Form1 game = new Form1(isSoundMuted);
            this.Hide();
            game.ShowDialog();
            this.Show();
        }


        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Flappy Bird: Special Edition\n\n" +
                "Разработчики:\n" +
                "- Slime\n" +
                "- DASH \n\n" +
                "Управление:\n" +
                "Пробел — Прыжок и Рестарт.\n" +
                "D — Режим разработчика.\n\n" +
                "Удачи в достижении новых рекордов!",
                "Об игре");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            isSoundMuted = !isSoundMuted;
            if (isSoundMuted)
            {
                btnMute.Text = "🔇 Звук: Выкл";
                btnMute.BackColor = Color.LightCoral;
            }
            else
            {
                btnMute.Text = "🔊 Звук: Вкл";
                btnMute.BackColor = Color.LightGreen;
            }
        }
    }
}