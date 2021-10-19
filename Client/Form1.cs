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
       // Player player;
        private HubConnection connection;
        public Form1()
        {
            InitializeComponent();

            SingletonConnection temp_connection = SingletonConnection.GetInstance();
            connection = temp_connection.GetConnection();
            listBox2.Items.Add("Default pickaxe");
            listBox2.Items.Add("Red pickaxe");
            listBox2.Items.Add("Black pickaxe");
            listBox2.Items.Add("Blue pickaxe");
            listBox2.SelectedIndex = 0;
        
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
