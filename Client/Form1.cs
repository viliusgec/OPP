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
            listBox2.Items.Add("Default pickaxe");
            listBox2.Items.Add("Red pickaxe");
            listBox2.Items.Add("Black pickaxe");
            listBox2.Items.Add("Blue pickaxe");
            listBox2.SelectedIndex = 0;

            player = new Player();
            player.SetPickaxe("Default");
            pictureBox1.Image = player.GetPickaxe().Image;
        }

        //connection button
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();
                textBox1.Text = "Connection started";
                PlayerBoxClass.Image = pictureBox1.Image;

                label1.Text = "Connection started";
                this.Hide();
             //   gameForm.sen
                gameForm.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            player = new Player();
         
            player.SetPickaxe(listBox2.SelectedItem.ToString().Split(' ').First());
            pictureBox1.Image = player.GetPickaxe().Image;
        }
        
        private void label1_Click(object sender, EventArgs e)
        {


        }
    }
}

public static class PlayerBoxClass 
{  
    public static Image Image { get; set; }
}
