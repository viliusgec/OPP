using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading;
using Client.Decorator;
using System.Collections.Generic;
using Client.Flyweight;

namespace Client
{
    public class FormsEditor
    {
        public PictureBox playerPictureBox;
        public PictureBox enemyPictureBox;
        public Label scoreLabel;
        PictureBox white;
        PictureBox black;
        PictureBox diamond;
        FlyweightFactory factory = new FlyweightFactory();
        ListBox _buyMenu;
        Button buyMenuButton;
        TextBox _moveMenu;
        Button moveMenuButton;
        Label movementLabel2;
        Character player;
        public ImageList imageList1;
        public Control.ControlCollection control;
        public Size size;
        int score = 0;
        int money = 0;
        bool effectIsGranted = false;

        public FormsEditor(
            PictureBox pictureBox1,
            PictureBox pictureBox2,
            Label scoreLabel,
            ListBox buyMenu,
            Button buyMenuButton,
            TextBox moveMenu,
            Button moveMenuButton,
            ImageList imageList1,
            Character player,
            Control.ControlCollection control,
            Size size,
            Label movementLabel2
            ) {
            this.playerPictureBox = pictureBox1;
            this.enemyPictureBox = pictureBox2;
            this.scoreLabel = scoreLabel;
            this._buyMenu = buyMenu;
            this.buyMenuButton = buyMenuButton;
            this._moveMenu = moveMenu;
            this.moveMenuButton = moveMenuButton;
            this.imageList1 = imageList1;
            this.player = player;
            this.control = control;
            this.buyMenuButton.Click += new System.EventHandler(this.buyMenuButton_Click);
           // this.moveMenuButton.Click += new System.EventHandler(this.moveMenuButton_Click);
            this.size = size;
            this.movementLabel2 = movementLabel2;
            initSkins();
        }
        public void initSkins()
        {
            PlayerSkin skin;
            skin = factory.PlayerSkin(1);
            white = skin.ReturnPlayerSkin();

            skin = factory.PlayerSkin(2);
            black = skin.ReturnPlayerSkin();

            skin = factory.PlayerSkin(3);
            diamond = skin.ReturnPlayerSkin();
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

        public void addMoney(int _money)
        {
            money += _money;
            //scoreLabel.Text = "Score: " + money;
        }

        public int getMoney()
        {
            return money;
        }

        public bool getEffectIsGranted()
        {
            return effectIsGranted;
        }

        public void setEffectIsGranted(bool effectState)
        {
            effectIsGranted = effectState;
        }

        public void closeBuyMmenu()
        {
            _buyMenu.Enabled = false;
            _buyMenu.Hide();
            buyMenuButton.Enabled = false;
            buyMenuButton.Hide();
        }
        public void closeMoveMenu()
        {
            _moveMenu.Enabled = false;
            _moveMenu.Hide();
            movementLabel2.Enabled = false;
            movementLabel2.Hide();
            moveMenuButton.Enabled = false;
            moveMenuButton.Hide();
        }

        private void buyMenuButton_Click(object sender, EventArgs e)
        {
            int str = 0;
            string item = _buyMenu.SelectedItem.ToString();
            int temp = checkBuyMenuValue(item);
            str = calcStrength(temp); // skinus pagal str skaičių užmest
            if (str == -1)
                return ;
            setSkin(str);
            player.addStr(str);
            score -= temp;
            scoreLabel.Text = "Score: " + score;
        }

       // private void moveMenuButton_Click(object sender, EventArgs e)
       // {
          //  string item = _moveMenu.Text;
            //Tada cia sitoj vietos turi callint ta shit is facade
       // }

        /*
        *  čia toks truputį nesąmonė, nes neišėjo į listboxą dictionary ar keypair įmest, tai parsinimus darau debiliškus
        */
        private void setSkin(int skin)
        {
            switch(skin)
            {
                case 7:
                    this.playerPictureBox.Image = white.Image;
                    break;
                case 10:
                    this.playerPictureBox.Image = black.Image;
                    break;
                case 13:
                    this.playerPictureBox.Image = diamond.Image;
                    break;
                default:
                    break;
            }
        }
        /*
         *  čia toks truputį nesąmonė, nes neišėjo į listboxą dictionary ar keypair įmest, tai parsinimus darau debiliškus
         */
        public int calcStrength(int str) 
        {
            if (score < str)
                return -1;
            switch (str)
            {
                case 2:
                    return 7;
                case 5:
                    return 10;
                case 10:
                    return 13;
                default: 
                    return 5;
            }
        }

        public void buyMenu(Character player)
        {
            _buyMenu.Enabled = true;
            _buyMenu.Show();
            buyMenuButton.Text = "Buy";
            buyMenuButton.Enabled = true;
            buyMenuButton.Show();
        }

        public void moveMenu(Character player)
        {
            _moveMenu.Enabled = true;
            _moveMenu.Show();
            movementLabel2.Text = "Commands:\n moveLeft\n moveRight\n jump\n jumpUpLeft\n jumpUpRight\n digDown\n digLeft\n digRight\n After each command\n enter ';'";
            movementLabel2.Enabled = true;
            movementLabel2.Show(); 
            moveMenuButton.Text = "Move";
            moveMenuButton.Enabled = true;
            moveMenuButton.Show();
        }

        public int checkBuyMenuValue(string buff)
        {
            switch (buff)
            {
            case "White pickaxe - 2 score":
                    return 2;
            case "Black pickaxe - 5 score":
                    return 5;
            case "Diamond pickaxe - 10 score":
                    return 10;
                default: return 5;
            }
        }
        public void scoreZero()
        {
            scoreLabel.Text = "Score: 0";
        }
    }
}
