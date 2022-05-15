using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        private int pipeSpeed = 8;
        private int gravity = 10;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            FlappyBird.Top += gravity;
            PipeTop.Left -= pipeSpeed;
            PipeBottom.Left -= pipeSpeed;
            labelScore.Text = $"Score: {score}";

            if (PipeTop.Left < -100)
            {
                PipeTop.Left = 600;
                pipeSpeed++;
            }

            if (PipeBottom.Left < -100)
            {
                PipeBottom.Left = 600;
                score++;
            }

            bool stopGame = FlappyBird.Bounds.IntersectsWith(PipeBottom.Bounds)
                || FlappyBird.Bounds.IntersectsWith(PipeTop.Bounds)
                || FlappyBird.Bounds.IntersectsWith(Ground.Bounds)
                || FlappyBird.Top < -25;

            if (stopGame)
            {
                EndGame();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -20;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 10;
            }
        }
        private void EndGame()
        {
            GameTimer.Stop();
            labelScore.Text = "GameOver";

            var result = MessageBox.Show($"Вы проиграли со счётом {score}\nПопробовать заново?", 
                "Game Over", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                FlappyBird.Location = new Point(67, 227);
                PipeTop.Location = new Point(433, -15);
                PipeBottom.Location = new Point(376, 373);

                pipeSpeed = 8;
                gravity = 10;
                score = 0;

                GameTimer.Start();
            }
            else
            {
                Close();
            }
            
        }
    }
}
