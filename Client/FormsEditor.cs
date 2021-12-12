using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading;
using Client.Decorator;
using System.Collections.Generic;
using Client.Flyweight;
using Client.Visitor;

namespace Client
{
    public class FormsEditor
    {
        public PictureBox playerPictureBox;
        public PictureBox enemyPictureBox;
        public Label scoreLabel;
        public Label moneyLabel;
        private HubConnection connection;
        string room;
        PictureBox white;
        PictureBox black;
        PictureBox diamond;
        PictureBox enemyWhite;
        PictureBox enemyBlack;
        PictureBox enemyDiamond;
        FlyweightFactory factory = new FlyweightFactory();
        ListBox _buyMenu;
        Button buyMenuButton;
        Button buyMenuButtonMoney;
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
        ConcreteVisitor visitor;
        ScoreComponent compscore;
        MoneyComponent compmoney;

        public FormsEditor(
            PictureBox pictureBox1,
            PictureBox pictureBox2,
            Label scoreLabel,
            Label moneyLabel,
            ListBox buyMenu,
            Button buyMenuButton,
            Button buyMenuButtonMoney,
            TextBox moveMenu,
            Button moveMenuButton,
            ImageList imageList1,
            Character player,
            Control.ControlCollection control,
            Size size,
            Label movementLabel2,
            string room,
            HubConnection connection
            ) {
            this.playerPictureBox = pictureBox1;
            this.enemyPictureBox = pictureBox2;
            this.scoreLabel = scoreLabel;
            this.moneyLabel = moneyLabel;
            this._buyMenu = buyMenu;
            this.buyMenuButton = buyMenuButton;
            this.buyMenuButtonMoney = buyMenuButtonMoney;
            this._moveMenu = moveMenu;
            this.moveMenuButton = moveMenuButton;
            this.imageList1 = imageList1;
            this.player = player;
            this.control = control;
            this.size = size;
            this.movementLabel2 = movementLabel2;
            this.buyMenuButton.Click += new System.EventHandler(this.buyMenuButtonScore_Click);
            this.buyMenuButtonMoney.Click += new System.EventHandler(this.buyMenuButtonMoney_Click);
            this.room = room;
            this.connection = connection;

            initSkins();
            initVisitor();
            ReceiveSkin();
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

            skin = factory.PlayerSkin(4);
            enemyWhite = skin.ReturnPlayerSkin();

            skin = factory.PlayerSkin(5);
            enemyBlack = skin.ReturnPlayerSkin();

            skin = factory.PlayerSkin(6);
            enemyDiamond = skin.ReturnPlayerSkin();
        }

        public void initVisitor()
        {
            compscore = new ScoreComponent();
            compmoney = new MoneyComponent();

            visitor = new ConcreteVisitor();
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
            moneyLabel.Text = "Money: " + money;
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

            buyMenuButtonMoney.Enabled = false;
            buyMenuButtonMoney.Hide();
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

        private void buyMenuButtonScore_Click(object sender, EventArgs e)
        {
            int str = 0;
            if (_buyMenu.SelectedIndex == -1)
            {
                return;
            }
            string item = _buyMenu.SelectedItem.ToString();
            int temp = checkBuyMenuValue(item);
            str = calcStrength(temp); // skinus pagal str skaičių užmest
            if (str == -1)
                return ;
            setSkin(str);
            player.addStr(str);
            compscore.Accept(visitor, scoreLabel, score, temp);
        }

        private void buyMenuButtonMoney_Click(object sender, EventArgs e)
        {
            int str = 0;
            if (_buyMenu.SelectedIndex == -1)
            {
                return;
            }
            string item = _buyMenu.SelectedItem.ToString();
            int temp = checkBuyMenuValueMoney(item);
            str = calcStrengthMoney(temp); // skinus pagal str skaičių užmest
            if (str == -1)
                return;
            setSkin(str);
            player.addStr(str);
            compmoney.Accept(visitor, moneyLabel, money, temp);
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
                    _ = SendSkin("a");
                    break;
                case 10:
                    this.playerPictureBox.Image = black.Image;
                    _= SendSkin("b");
                    break;
                case 13:
                    this.playerPictureBox.Image = diamond.Image;
                    _= SendSkin("c");
                    break;
                default:
                    break;
            }
        }

        private async Task SendSkin(string skin)
        {
            await connection.InvokeAsync("SendSkin",
                    skin, room);

            connection.On<string>("ReceiveSkin", (skin) =>
            {
                switch (skin)
                {
                    case "a":
                        this.enemyPictureBox.Image = enemyWhite.Image;
                        break;
                    case "b":
                        this.enemyPictureBox.Image = enemyBlack.Image;
                        break;
                    case "c":
                        this.enemyPictureBox.Image = enemyDiamond.Image;
                        break;
                    default:
                        this.enemyPictureBox.Image = enemyWhite.Image;
                        break;
                }
            });
        }
        public void ReceiveSkin()
        {
            this.connection.On<string>("ReceiveSkin", (skin) =>
            {
                switch(skin)
                {
                    case "a":
                        this.enemyPictureBox.Image = enemyWhite.Image;
                        break;
                    case "b":
                        this.enemyPictureBox.Image = enemyBlack.Image;
                        break;
                    case "c":
                        this.enemyPictureBox.Image = enemyDiamond.Image;
                        break;
                    default:
                        this.enemyPictureBox.Image = enemyWhite.Image;
                        break;
                }
            });
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

        /*
 *  čia toks truputį nesąmonė, nes neišėjo į listboxą dictionary ar keypair įmest, tai parsinimus darau debiliškus
 */
        public int calcStrengthMoney(int str)
        {
            if (money < str)
                return -1;
            switch (str)
            {
                case 10:
                    return 7;
                case 20:
                    return 10;
                case 30:
                    return 13;
                default:
                    return 5;
            }
        }

        public void buyMenu(Character player)
        {
            _buyMenu.Enabled = true;
            _buyMenu.Show();
            buyMenuButton.Enabled = true;
            buyMenuButton.Show();

            buyMenuButtonMoney.Enabled = true;
            buyMenuButtonMoney.Show();
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
            case "White pickaxe - 2 score || 10 money":
                    return 2;
            case "Black pickaxe - 5 score || 20 money":
                    return 5;
            case "Diamond pickaxe - 10 score || 30 money":
                    return 10;
                default: return 5;
            }
        }

        public int checkBuyMenuValueMoney(string buff)
        {
            switch (buff)
            {
                case "White pickaxe - 2 score || 10 money":
                    return 10;
                case "Black pickaxe - 5 score || 20 money":
                    return 20;
                case "Diamond pickaxe - 10 score || 30 money":
                    return 30;
                default: return 5;
            }
        }
        public void scoreZero()
        {
            scoreLabel.Text = "Score: 0";
            moneyLabel.Text = "Money: 0";
        }
    }
}
