using Client.Decorator;
using Client.Flyweight;
using System;
using System.Drawing;
using System.Windows.Forms;

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
        Character player;
        public ImageList imageList1;
        public Control.ControlCollection control;
        public Size size;
        int score = 0;
        int money = 0;
        bool effectIsGranted = false;

        public FormsEditor(PictureBox pictureBox1, PictureBox pictureBox2, Label scoreLabel, ListBox buyMenu, Button buyMenuButton, ImageList imageList1, Character player, Control.ControlCollection control, Size size)
        {
            this.playerPictureBox = pictureBox1;
            this.enemyPictureBox = pictureBox2;
            this.scoreLabel = scoreLabel;
            this._buyMenu = buyMenu;
            this.buyMenuButton = buyMenuButton;
            this.imageList1 = imageList1;
            this.player = player;
            this.control = control;
            this.buyMenuButton.Click += new System.EventHandler(this.buyMenuButton_Click);
            this.size = size;
            initPlayerSkins();
        }
        public void initPlayerSkins()
        {
            PlayerSkin playerSkin;
            playerSkin = factory.PlayerSkin(1);
            white = playerSkin.ReturnPlayerSkin();

            playerSkin = factory.PlayerSkin(2);
            black = playerSkin.ReturnPlayerSkin();

            playerSkin = factory.PlayerSkin(3);
            diamond = playerSkin.ReturnPlayerSkin();
        }

        public void addPlayerScore()
        {
            score += 1;
            scoreLabel.Text = "Score: " + score;
        }

        public int getPlayerScore()
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

        private void buyMenuButton_Click(object sender, EventArgs e)
        {
            int str = 0;
            string item = _buyMenu.SelectedItem.ToString();
            int temp = checkBuyMenuValue(item);
            str = calcStrength(temp); // skinus pagal str skaičių užmest
            if (str == -1)
                return;
            setPlayerSkin(str);
            player.addStr(str);
            score -= temp;
            scoreLabel.Text = "Score: " + score;
        }
        /*
        *  čia toks truputį nesąmonė, nes neišėjo į listboxą dictionary ar keypair įmest, tai parsinimus darau debiliškus
        */
        private void setPlayerSkin(int skin)
        {
            switch (skin)
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
