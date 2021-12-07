﻿
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client
{
    partial class Facade
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Facade));
            this.enemyPictureBox = new System.Windows.Forms.PictureBox();
            this.playerPictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MovementLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.gameStateLabel = new System.Windows.Forms.Label();
            this.buyMenu = new System.Windows.Forms.ListBox();
            this.buyMenuButton = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.moneyLabel = new System.Windows.Forms.Label();
            this.buyMenuButtonMoney = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.enemyPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // enemyPictureBox
            // 
            this.enemyPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.enemyPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("enemyPictureBox.Image")));
            this.enemyPictureBox.Location = new System.Drawing.Point(388, 12);
            this.enemyPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.enemyPictureBox.Name = "enemyPictureBox";
            this.enemyPictureBox.Size = new System.Drawing.Size(64, 75);
            this.enemyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enemyPictureBox.TabIndex = 7;
            this.enemyPictureBox.TabStop = false;
            this.enemyPictureBox.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // playerPictureBox
            // 
            this.playerPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.playerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("playerPictureBox.Image")));
            this.playerPictureBox.Location = new System.Drawing.Point(318, 9);
            this.playerPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.playerPictureBox.Name = "playerPictureBox";
            this.playerPictureBox.Size = new System.Drawing.Size(64, 78);
            this.playerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playerPictureBox.TabIndex = 8;
            this.playerPictureBox.TabStop = false;
            this.playerPictureBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 22);
            this.button1.TabIndex = 18;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MovementLabel
            // 
            this.MovementLabel.AutoSize = true;
            this.MovementLabel.BackColor = System.Drawing.Color.Transparent;
            this.MovementLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MovementLabel.Location = new System.Drawing.Point(11, 199);
            this.MovementLabel.Name = "MovementLabel";
            this.MovementLabel.Size = new System.Drawing.Size(38, 15);
            this.MovementLabel.TabIndex = 19;
            this.MovementLabel.Text = "label1";
            this.MovementLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(509, 41);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 22);
            this.button2.TabIndex = 20;
            this.button2.Text = "Send Message";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(318, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Chat:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(509, 9);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(154, 23);
            this.textBox1.TabIndex = 22;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(508, 98);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 22);
            this.button3.TabIndex = 23;
            this.button3.Text = "Undo Message";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(318, 130);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(346, 143);
            this.textBox2.TabIndex = 25;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.ScoreLabel.Font = new System.Drawing.Font("Arial Narrow", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ScoreLabel.Location = new System.Drawing.Point(6, 40);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(0, 43);
            this.ScoreLabel.TabIndex = 26;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(510, 69);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(153, 22);
            this.button4.TabIndex = 27;
            this.button4.Text = "Send Emote";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // gameStateLabel
            // 
            this.gameStateLabel.AutoSize = true;
            this.gameStateLabel.Enabled = false;
            this.gameStateLabel.Font = new System.Drawing.Font("Arial Narrow", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gameStateLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gameStateLabel.Location = new System.Drawing.Point(262, 248);
            this.gameStateLabel.Name = "gameStateLabel";
            this.gameStateLabel.Size = new System.Drawing.Size(0, 57);
            this.gameStateLabel.TabIndex = 28;
            this.gameStateLabel.Click += new System.EventHandler(this.gameStateButton_Click);
            // 
            // buyMenu
            // 
            this.buyMenu.Enabled = false;
            this.buyMenu.Hide();
            this.buyMenu.FormattingEnabled = true;
            this.buyMenu.ItemHeight = 15;
            this.buyMenu.Items.AddRange(new object[] {
            "White pickaxe - 2 score",
            "Black pickaxe - 5 score",
            "Diamond pickaxe - 10 score"});
            this.buyMenu.Location = new System.Drawing.Point(170, 130);
            this.buyMenu.Name = "buyMenu";
            this.buyMenu.Size = new System.Drawing.Size(183, 139);
            this.buyMenu.TabIndex = 29;
            this.buyMenu.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buyMenuButton
            // 
            this.buyMenuButton.Enabled = false;
            this.buyMenuButton.Hide();
            this.buyMenuButton.Location = new System.Drawing.Point(278, 268);
            this.buyMenuButton.Name = "buyMenuButton";
            this.buyMenuButton.Size = new System.Drawing.Size(82, 33);
            this.buyMenuButton.TabIndex = 30;
            this.buyMenuButton.Text = "with score";
            this.buyMenuButton.UseVisualStyleBackColor = true;
            // 
            // buyMenuButtonMoney
            // 
            this.buyMenuButtonMoney.Enabled = false;
            this.buyMenuButtonMoney.Hide();
            this.buyMenuButtonMoney.Location = new System.Drawing.Point(190, 268);
            this.buyMenuButtonMoney.Name = "buyMenuButtonMoney";
            this.buyMenuButtonMoney.Size = new System.Drawing.Size(82, 33);
            this.buyMenuButtonMoney.TabIndex = 33;
            this.buyMenuButtonMoney.Text = "with money";
            this.buyMenuButtonMoney.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Hide();
            this.button5.Location = new System.Drawing.Point(10, 8);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 22);
            this.button5.TabIndex = 31;
            this.button5.Text = "Pause";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // moneyLabel
            // 
            this.moneyLabel.AutoSize = true;
            this.moneyLabel.BackColor = System.Drawing.Color.Transparent;
            this.moneyLabel.Font = new System.Drawing.Font("Arial Narrow", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.moneyLabel.Location = new System.Drawing.Point(6, 80);
            this.moneyLabel.Name = "moneyLabel";
            this.moneyLabel.Size = new System.Drawing.Size(0, 43);
            this.moneyLabel.TabIndex = 32;

            // 
            // Facade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(700, 338);
            this.Controls.Add(this.buyMenuButtonMoney);
            this.Controls.Add(this.moneyLabel);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.buyMenuButton);
            this.Controls.Add(this.buyMenu);
            this.Controls.Add(this.gameStateLabel);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.MovementLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.playerPictureBox);
            this.Controls.Add(this.enemyPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Facade";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.enemyPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox enemyPictureBox;
        private System.Windows.Forms.PictureBox playerPictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label MovementLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label gameStateLabel;
        private System.Windows.Forms.ListBox buyMenu;
        private System.Windows.Forms.Button buyMenuButton;
        private Button button5;
        private Label moneyLabel;
        private Button buyMenuButtonMoney;
    }
}