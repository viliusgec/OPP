//using Microsoft.AspNet.SignalR.Client;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client
{
    public partial class Form1 : Form
    {
        private HubConnection connection;
        public Form1()
        {
            InitializeComponent();

            SingletonConnection temp_connection = SingletonConnection.GetInstance();
            connection = temp_connection.GetConnection();

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
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.ToString();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
    }
}
