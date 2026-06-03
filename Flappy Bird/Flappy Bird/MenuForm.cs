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
        private Button btnMuteMenu;

        public MenuForm()
        {
            InitializeComponent();
        }

        // Аккуратное создание кнопки звука в меню
        private void InitializeMuteButton()
        {
            btnMuteMenu = new Button();
            btnMuteMenu.Text = "🔊 Звук: Вкл";
            btnMuteMenu.Font = new Font("Arial", 10, FontStyle.Bold);
            btnMuteMenu.Size = new Size(130, 35);

            // Позиционируешь под свои кнопки (например, в самый низ формы или рядом с Выходом)
            btnMuteMenu.Location = new Point(10, 10);
            btnMuteMenu.BackColor = Color.LightGreen;
            btnMuteMenu.FlatStyle = FlatStyle.Flat;
            btnMuteMenu.Cursor = Cursors.Hand;

            btnMuteMenu.Click += BtnMuteMenu_Click;
            this.Controls.Add(btnMuteMenu);
        }

        private void BtnMuteMenu_Click(object sender, EventArgs e)
        {
            isSoundMuted = !isSoundMuted;
            if (isSoundMuted)
            {
                btnMuteMenu.Text = "🔇 Звук: Выкл";
                btnMuteMenu.BackColor = Color.LightCoral;
            }
            else
            {
                btnMuteMenu.Text = "🔊 Звук: Вкл";
                btnMuteMenu.BackColor = Color.LightGreen;
            }
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
                "M — Включить/Выключить звук.\n" +
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