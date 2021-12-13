using Client.Decorator;
using Client.Flyweight;
using Client.Visitor;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class FormsEditor
    {
        public PictureBox playerPictureBox;
        public PictureBox enemyPictureBox;
        public Label scoreLabel;
        public Label moneyLabel;
        private readonly HubConnection connection;
        private readonly string room;
        private PictureBox white;
        private PictureBox black;
        private PictureBox diamond;
        private PictureBox enemyWhite;
        private PictureBox enemyBlack;
        private PictureBox enemyDiamond;
        private readonly FlyweightFactory factory = new();
        private readonly ListBox _buyMenu;
        private readonly Button buyMenuButton;
        private readonly Button buyMenuButtonMoney;
        private readonly TextBox _moveMenu;
        private readonly Button moveMenuButton;
        private readonly Label movementLabel2;
        private readonly Character player;
        public ImageList imageList1;
        public Control.ControlCollection control;
        public Size size;
        private int score = 0;
        private int money = 0;
        private bool effectIsGranted = false;
        private ConcreteVisitor visitor;
        private ScoreComponent compscore;
        private MoneyComponent compmoney;

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
            )
        {
            playerPictureBox = pictureBox1;
            enemyPictureBox = pictureBox2;
            this.scoreLabel = scoreLabel;
            this.moneyLabel = moneyLabel;
            _buyMenu = buyMenu;
            this.buyMenuButton = buyMenuButton;
            this.buyMenuButtonMoney = buyMenuButtonMoney;
            _moveMenu = moveMenu;
            this.moveMenuButton = moveMenuButton;
            this.imageList1 = imageList1;
            this.player = player;
            this.control = control;
            this.size = size;
            this.movementLabel2 = movementLabel2;
            this.buyMenuButton.Click += new System.EventHandler(buyMenuButtonScore_Click);
            this.buyMenuButtonMoney.Click += new System.EventHandler(buyMenuButtonMoney_Click);
            this.room = room;
            this.connection = connection;

            InitPlayerSkins();
            InitVisitor();
            ReceiveSkin();
        }
        public void InitPlayerSkins()
        {
            PlayerSkin playerSkin;
            playerSkin = factory.PlayerSkin(1);
            white = playerSkin.ReturnPlayerSkin();

            playerSkin = factory.PlayerSkin(2);
            black = playerSkin.ReturnPlayerSkin();

            playerSkin = factory.PlayerSkin(3);
            diamond = playerSkin.ReturnPlayerSkin();

            playerSkin = factory.PlayerSkin(4);
            enemyWhite = playerSkin.ReturnPlayerSkin();

            playerSkin = factory.PlayerSkin(5);
            enemyBlack = playerSkin.ReturnPlayerSkin();

            playerSkin = factory.PlayerSkin(6);
            enemyDiamond = playerSkin.ReturnPlayerSkin();
        }

        public void InitVisitor()
        {
            compscore = new ScoreComponent();
            compmoney = new MoneyComponent();

            visitor = new ConcreteVisitor();
        }

        public void AddPlayerScore()
        {
            score += 1;
            scoreLabel.Text = "Score: " + score;
        }

        public void AddPlayerMoney(int _money)
        {
            money += _money;
            moneyLabel.Text = "Money: " + money;
        }

        public void CloseBuyMmenu()
        {
            _buyMenu.Enabled = false;
            _buyMenu.Hide();
            buyMenuButton.Enabled = false;
            buyMenuButton.Hide();

            buyMenuButtonMoney.Enabled = false;
            buyMenuButtonMoney.Hide();
        }
        public void CloseMoveMenu()
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
            int temp = CheckBuyMenuValue(item);
            str = CalcStrength(temp); // skinus pagal str skaičių užmest
            if (str == -1)
            {
                return;
            }

            SetPlayerSkin(str);
            player.AddStr(str);
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
            int temp = CheckBuyMenuValueMoney(item);
            str = CalcStrengthMoney(temp); // skinus pagal str skaičių užmest
            if (str == -1)
            {
                return;
            }

            SetPlayerSkin(str);
            player.AddStr(str);
            compmoney.Accept(visitor, moneyLabel, money, temp);
        }


        /*
        *  čia toks truputį nesąmonė, nes neišėjo į listboxą dictionary ar keypair įmest, tai parsinimus darau debiliškus
        */
        private void SetPlayerSkin(int playerSkin)
        {
            switch (playerSkin)
            {
                case 7:
                    playerPictureBox.Image = white.Image;
                    _ = SendSkin("a");
                    break;
                case 10:
                    playerPictureBox.Image = black.Image;
                    _ = SendSkin("b");
                    break;
                case 13:
                    playerPictureBox.Image = diamond.Image;
                    _ = SendSkin("c");
                    break;
                default:
                    break;
            }
        }

        private async Task SendSkin(string playerSkin)
        {
            await connection.InvokeAsync("SendSkin",
                playerSkin, room);

            connection.On<string>("ReceiveSkin", (playerSkin) =>
            {
                enemyPictureBox.Image = playerSkin switch
                {
                    "a" => enemyWhite.Image,
                    "b" => enemyBlack.Image,
                    "c" => enemyDiamond.Image,
                    _ => enemyWhite.Image,
                };
            });
        }
        public void ReceiveSkin()
        {
            connection.On<string>("ReceiveSkin", (playerSkin) =>
            {
                enemyPictureBox.Image = playerSkin switch
                {
                    "a" => enemyWhite.Image,
                    "b" => enemyBlack.Image,
                    "c" => enemyDiamond.Image,
                    _ => enemyWhite.Image,
                };
            });
        }
        /*
         *  čia toks truputį nesąmonė, nes neišėjo į listboxą dictionary ar keypair įmest, tai parsinimus darau debiliškus
         */
        public int CalcStrength(int str)
        {
            if (score < str)
            {
                return -1;
            }

            return str switch
            {
                2 => 7,
                5 => 10,
                10 => 13,
                _ => 5,
            };
        }

        /*
 *  čia toks truputį nesąmonė, nes neišėjo į listboxą dictionary ar keypair įmest, tai parsinimus darau debiliškus
 */
        public int CalcStrengthMoney(int str)
        {
            if (money < str)
            {
                return -1;
            }

            return str switch
            {
                10 => 7,
                20 => 10,
                30 => 13,
                _ => 5,
            };
        }

        public void BuyMenu()
        {
            _buyMenu.Enabled = true;
            _buyMenu.Show();
            buyMenuButton.Enabled = true;
            buyMenuButton.Show();

            buyMenuButtonMoney.Enabled = true;
            buyMenuButtonMoney.Show();
        }

        public void MoveMenu(Character player)
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

        public int CheckBuyMenuValue(string buff)
        {
            return buff switch
            {
                "White pickaxe - 2 score || 10 money" => 2,
                "Black pickaxe - 5 score || 20 money" => 5,
                "Diamond pickaxe - 10 score || 30 money" => 10,
                _ => 5,
            };
        }

        public int CheckBuyMenuValueMoney(string buff)
        {
            return buff switch
            {
                "White pickaxe - 2 score || 10 money" => 10,
                "Black pickaxe - 5 score || 20 money" => 20,
                "Diamond pickaxe - 10 score || 30 money" => 30,
                _ => 5,
            };
        }
        public void ScoreZero()
        {
            scoreLabel.Text = "Score: 0";
            moneyLabel.Text = "Money: 0";
        }
    }
}
