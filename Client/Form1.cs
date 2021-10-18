//using Microsoft.AspNet.SignalR.Client;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Client.Map;
using Client.Strategy;

namespace Client
{
    public partial class Form1 : Form
    {
        Form gameForm;
        Algorithm strategy;
        private HubConnection connection;
        public Form1()
        {
            InitializeComponent();

            SingletonConnection temp_connection = SingletonConnection.GetInstance();
            connection = temp_connection.GetConnection();

            gameForm = new GameForm();
            this.KeyPreview = true;
            this.KeyDown += sendBoxCoordinates;
            
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
                this.Hide();
                gameForm.ShowDialog();
                this.Show();
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
            int temp;

            switch(e.KeyCode)
            {
                case Keys.A:
                    strategy = new MoveLeft(x);
                    break;
                case Keys.D:
                    strategy = new MoveRight(x);
                    break;
                case Keys.W:
                    strategy = new Jump(y);
                    break;
                case Keys.LShiftKey:
                    strategy = new Mine(x);
                    break;
            }

            temp = strategy.Behave(x);
            // TODO: jump ir mine
            pictureBox1.Location = new Point(temp, y);
            _ = SendGetCoordinatesAsync(temp, y);
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
