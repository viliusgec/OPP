using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading;

namespace Client
{
    public class FormsEditor
    {
        public PictureBox pictureBox1;
        public PictureBox pictureBox2;
        public Label scoreLabel;
        int score = 0;
        bool effectIsGranted = false;

        public FormsEditor(PictureBox pictureBox1, PictureBox pictureBox2, Label scoreLabel)
        {
            this.pictureBox1 = pictureBox1;
            this.pictureBox2 = pictureBox2;
            this.scoreLabel = scoreLabel;
        }

        public void addScore()
        {
            score += 1;
            scoreLabel.Text = "Score: " + score;
        }

        public int getScore()
        {
            return score;
        }

        public bool getEffectIsGranted()
        {
            return effectIsGranted;
        }

        public void setEffectIsGranted(bool effectState)
        {
            effectIsGranted = effectState;
        }

        public void buyMenu()
        {
            score += 10;
            scoreLabel.Text = "Score: " + score;
        }
        public void scoreZero()
        {
            scoreLabel.Text = "Score: 0";
        }
    }
}
