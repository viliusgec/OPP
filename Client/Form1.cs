using System;
using System.Drawing;
using System.Linq;
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
        Player player;
        private HubConnection connection;
        public Form1()
        {
            InitializeComponent();

            SingletonConnection temp_connection = SingletonConnection.GetInstance();
            connection = temp_connection.GetConnection();
          
         
            gameForm = new GameForm();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //connection button
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();           

                label1.Text = "Connection started";
                this.Hide();
                gameForm.ShowDialog();
                this.Show();
            }
            catch
            {
                label1.Text = "You can't connect second time.";//ex.ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}