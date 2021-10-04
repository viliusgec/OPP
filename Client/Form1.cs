//using Microsoft.AspNet.SignalR.Client;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Client.Map;

namespace Client
{
    public partial class Form1 : Form
    {
        private HubConnection connection;
        public Form1()
        {
            InitializeComponent();
            createMap();

            SingletonConnection temp_connection = SingletonConnection.GetInstance();
            connection = temp_connection.GetConnection();

            this.KeyPreview = true;
            this.KeyDown += sendBoxCoordinates;
            
        }
        private void createMap()
        {
            AbstractFactory factory = null;
            string[] blockTypes = { "Static", "Falling", "Unbreakable" };
            string[] blockNames = {"Cobble", "Sand", "Bedrock"};
            int x = 10;
            int y = x;
            MapBase map = new MapBase(x, y);
            Random rnd = new Random();
            Block[,] blocks = new Block[x,y];
            factory = map.GetL1Factory();
            //   string[,] mapNames = new string[x, y];
            /*
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j <y; j++)
                {
                    int r = rnd.Next(3);
                    blocks[i, j] = factory.GetBlock(blockTypes[r], blockNames[r]);
                    Effect.Effect effect = assignEffect();
                    if(effect != null)
                    {
                        listBox1.Items.Add(blocks[i, j].name + "  -  " + effect.EffectType);
                    } else
                    {
                        listBox1.Items.Add(blocks[i, j].name);
                    }
                }
                listBox1.Items.Add('\n');
            }
            */
        }

        private Effect.Effect assignEffect()
        {
            Random rnd = new Random();
            int rndNumber = rnd.Next(1, 10);
            Effect.EffectFactory factory = null; 
            switch (rndNumber)
            {
                case 1:
                    factory = new Effect.JumpFactory(50);
                    break;
                case 2:
                    factory = new Effect.BlindFactory(5);
                    break;
                default:
                    return null;
            }

            Effect.Effect effect = factory.GetEffect();

            return effect;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //connection button
        private async void button1_Click(object sender, EventArgs e)
        {
            connection.On<string, string>("ReceiveMessage", (s1, s2) =>
            {
                textBox1.AppendText(s2);
            });

            connection.On<string, string>("ReceiveCoordinates", (x, y) =>
            {
                pictureBox2.Location = new Point(int.Parse(x), int.Parse(y));
            });

            try
            {
                await connection.StartAsync();
                textBox1.Text = "Connection started";
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.ToString();
            }
        }


        private void sendBoxCoordinates(object sender, KeyEventArgs e)
        {
            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;

            if (e.KeyCode == Keys.D) x += 10;
            else if (e.KeyCode == Keys.A) x -= 10;
            else if (e.KeyCode == Keys.W) y -= 10;
            else if (e.KeyCode == Keys.S) y += 10;
            pictureBox1.Location = new Point(x, y);
            _ = SendGetCoordinatesAsync(x, y);
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
