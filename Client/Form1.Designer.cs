
namespace Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.roomNameBox = new System.Windows.Forms.TextBox();
            this.createRoomButton = new System.Windows.Forms.Button();
            this.isChatCheckBox = new System.Windows.Forms.CheckBox();
            this.roomListBox = new System.Windows.Forms.ListBox();
            this.joinRoomButton = new System.Windows.Forms.Button();
            this.roomPassBox = new System.Windows.Forms.TextBox();
            this.roomNameLabel = new System.Windows.Forms.Label();
            this.roomPasswordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-3, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 4;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // roomNameBox
            // 
            this.roomNameBox.Location = new System.Drawing.Point(324, 48);
            this.roomNameBox.Name = "roomNameBox";
            this.roomNameBox.Size = new System.Drawing.Size(125, 27);
            this.roomNameBox.TabIndex = 5;
            this.roomNameBox.Text = "Room";
            // 
            // createRoomButton
            // 
            this.createRoomButton.Location = new System.Drawing.Point(653, 46);
            this.createRoomButton.Name = "createRoomButton";
            this.createRoomButton.Size = new System.Drawing.Size(94, 29);
            this.createRoomButton.TabIndex = 6;
            this.createRoomButton.Text = "Create Room";
            this.createRoomButton.UseVisualStyleBackColor = true;
            this.createRoomButton.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // isChatCheckBox
            // 
            this.isChatCheckBox.AutoSize = true;
            this.isChatCheckBox.Location = new System.Drawing.Point(586, 48);
            this.isChatCheckBox.Name = "isChatCheckBox";
            this.isChatCheckBox.Size = new System.Drawing.Size(61, 24);
            this.isChatCheckBox.TabIndex = 7;
            this.isChatCheckBox.Text = "Chat";
            this.isChatCheckBox.UseVisualStyleBackColor = true;
            // 
            // roomListBox
            // 
            this.roomListBox.FormattingEnabled = true;
            this.roomListBox.ItemHeight = 20;
            this.roomListBox.Location = new System.Drawing.Point(324, 81);
            this.roomListBox.Name = "roomListBox";
            this.roomListBox.Size = new System.Drawing.Size(423, 284);
            this.roomListBox.TabIndex = 8;
            this.roomListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // joinRoomButton
            // 
            this.joinRoomButton.Location = new System.Drawing.Point(653, 371);
            this.joinRoomButton.Name = "joinRoomButton";
            this.joinRoomButton.Size = new System.Drawing.Size(94, 29);
            this.joinRoomButton.TabIndex = 9;
            this.joinRoomButton.Text = "Join Room";
            this.joinRoomButton.UseVisualStyleBackColor = true;
            this.joinRoomButton.Click += new System.EventHandler(this.joinRoomButton_Click);
            // 
            // roomPassBox
            // 
            this.roomPassBox.Location = new System.Drawing.Point(455, 48);
            this.roomPassBox.Name = "roomPassBox";
            this.roomPassBox.Size = new System.Drawing.Size(125, 27);
            this.roomPassBox.TabIndex = 10;
            // 
            // roomNameLabel
            // 
            this.roomNameLabel.AutoSize = true;
            this.roomNameLabel.Location = new System.Drawing.Point(324, 25);
            this.roomNameLabel.Name = "roomNameLabel";
            this.roomNameLabel.Size = new System.Drawing.Size(90, 20);
            this.roomNameLabel.TabIndex = 11;
            this.roomNameLabel.Text = "Room name";
            this.roomNameLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // roomPasswordLabel
            // 
            this.roomPasswordLabel.AutoSize = true;
            this.roomPasswordLabel.Location = new System.Drawing.Point(455, 25);
            this.roomPasswordLabel.Name = "roomPasswordLabel";
            this.roomPasswordLabel.Size = new System.Drawing.Size(261, 20);
            this.roomPasswordLabel.TabIndex = 12;
            this.roomPasswordLabel.Text = "Room password(leave blank for none)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 451);
            this.Controls.Add(this.roomPasswordLabel);
            this.Controls.Add(this.roomNameLabel);
            this.Controls.Add(this.roomPassBox);
            this.Controls.Add(this.joinRoomButton);
            this.Controls.Add(this.roomListBox);
            this.Controls.Add(this.isChatCheckBox);
            this.Controls.Add(this.createRoomButton);
            this.Controls.Add(this.roomNameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox roomNameBox;
        private System.Windows.Forms.Button createRoomButton;
        private System.Windows.Forms.CheckBox isChatCheckBox;
        private System.Windows.Forms.ListBox roomListBox;
        private System.Windows.Forms.Button joinRoomButton;
        private System.Windows.Forms.TextBox roomPassBox;
        private System.Windows.Forms.Label roomNameLabel;
        private System.Windows.Forms.Label roomPasswordLabel;
    }
}

