using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CheckerBoardUI
{
    public delegate void ClickEventHandler(object sender, ClickEventHandler e);

    public class StartWindow : Form
    {
        private Button m_ButtonDone;
        private Label m_TopLabel;
        private Label m_Players;
        private Label m_Player1;
        private TextBox m_Player1TextBox;
        private Label m_Player2;
        private CheckBox m_Player2CheckBox;
        private TextBox m_Player2TextBox;
        private RadioButton m_Radio6X6;
        private RadioButton m_Radio8X8;
        private RadioButton m_Radio10X10;
        private string m_Player1Name;
        private int m_boardSize;
        private string m_Player2Name = "Computer";

        public StartWindow()
        {
            this.Text = "Game Settings";
            InitializeComponent();
            this.AutoSize = true;
        }

        public void DoneButton_Click_EventHandler(object sender, EventArgs e)
        {
            bool isValid = false;
            if (m_Radio6X6.Checked == true || m_Radio8X8.Checked == true || m_Radio10X10.Checked == true)
            {
                if (m_Player1TextBox.Text != string.Empty)
                {
                    if ((m_Player2CheckBox.Checked == true && m_Player2TextBox.Text != string.Empty) || m_Player2CheckBox.Checked == false)
                    {
                        isValid = true;
                    }
                }
            }

            if (!isValid)
            {
                MessageBox.Show("The Input you entered is not Valid Please try again", "Not Valid Message", MessageBoxButtons.OK);
            }
            else
            {
                BuildCheckerBoardGame(m_boardSize, m_Player1Name, m_Player2Name);
            }
        }

        private void BuildCheckerBoardGame(int i_boardSize, string i_firstPlayer, string i_secondPlayer)
        {
            this.Close();
            CheckerBoard playingBoard = new CheckerBoard(i_boardSize, i_firstPlayer, i_secondPlayer);
        }

        private void InitializeComponent()
        {
            this.m_ButtonDone = new System.Windows.Forms.Button();
            this.m_TopLabel = new System.Windows.Forms.Label();
            this.m_Radio6X6 = new System.Windows.Forms.RadioButton();
            this.m_Radio8X8 = new System.Windows.Forms.RadioButton();
            this.m_Radio10X10 = new System.Windows.Forms.RadioButton();
            this.m_Players = new System.Windows.Forms.Label();
            this.m_Player1 = new System.Windows.Forms.Label();
            this.m_Player1TextBox = new System.Windows.Forms.TextBox();
            this.m_Player2CheckBox = new System.Windows.Forms.CheckBox();
            this.m_Player2 = new System.Windows.Forms.Label();
            this.m_Player2TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            this.m_ButtonDone.Location = new System.Drawing.Point(187, 161);
            this.m_ButtonDone.Name = "m_ButtonDone";
            this.m_ButtonDone.Size = new System.Drawing.Size(85, 31);
            this.m_ButtonDone.TabIndex = 0;
            this.m_ButtonDone.Text = "Done";
            this.m_ButtonDone.Click += new System.EventHandler(this.DoneButton_Click_EventHandler);
            this.m_TopLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)(0));
            this.m_TopLabel.Location = new System.Drawing.Point(12, 13);
            this.m_TopLabel.Name = "m_TopLabel";
            this.m_TopLabel.Size = new System.Drawing.Size(84, 22);
            this.m_TopLabel.TabIndex = 1;
            this.m_TopLabel.Text = "Board Size:";
            this.m_Radio6X6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)(0));
            this.m_Radio6X6.Location = new System.Drawing.Point(18, 26);
            this.m_Radio6X6.Name = "m_Radio6X6";
            this.m_Radio6X6.Size = new System.Drawing.Size(62, 32);
            this.m_Radio6X6.TabIndex = 2;
            this.m_Radio6X6.Text = "6x6";
            this.m_Radio6X6.CheckedChanged += new System.EventHandler(this.Radio6X6_CheckedChanged);
            this.m_Radio8X8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_Radio8X8.Location = new System.Drawing.Point(97, 30);
            this.m_Radio8X8.Name = "m_Radio8X8";
            this.m_Radio8X8.Size = new System.Drawing.Size(65, 28);
            this.m_Radio8X8.TabIndex = 3;
            this.m_Radio8X8.Text = "8x8";
            this.m_Radio8X8.CheckedChanged += new System.EventHandler(this.Radio8X8_CheckedChanged);
            this.m_Radio10X10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)(0));
            this.m_Radio10X10.Location = new System.Drawing.Point(182, 30);
            this.m_Radio10X10.Name = "m_Radio10X10";
            this.m_Radio10X10.Size = new System.Drawing.Size(80, 28);
            this.m_Radio10X10.TabIndex = 4;
            this.m_Radio10X10.Text = "10x10";
            this.m_Radio10X10.CheckedChanged += new System.EventHandler(this.Radio10X10_CheckedChanged);
            this.m_Players.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)(0));
            this.m_Players.Location = new System.Drawing.Point(12, 60);
            this.m_Players.Name = "m_Players";
            this.m_Players.Size = new System.Drawing.Size(74, 23);
            this.m_Players.TabIndex = 5;
            this.m_Players.Text = "Players:";
            this.m_Player1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)(0));
            this.m_Player1.Location = new System.Drawing.Point(24, 84);
            this.m_Player1.Name = "m_Player1";
            this.m_Player1.Size = new System.Drawing.Size(72, 23);
            this.m_Player1.TabIndex = 6;
            this.m_Player1.Text = "Player 1:";
            this.m_Player1TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)(0));
            this.m_Player1TextBox.Location = new System.Drawing.Point(119, 84);
            this.m_Player1TextBox.MaxLength = 12;
            this.m_Player1TextBox.Name = "m_Player1TextBox";
            this.m_Player1TextBox.Size = new System.Drawing.Size(153, 23);
            this.m_Player1TextBox.TabIndex = 7;
            this.m_Player1TextBox.TextChanged += new System.EventHandler(this.Player1TextBox_TextChanged);
            this.m_Player2CheckBox.Location = new System.Drawing.Point(27, 113);
            this.m_Player2CheckBox.Name = "m_Player2CheckBox";
            this.m_Player2CheckBox.Size = new System.Drawing.Size(21, 28);
            this.m_Player2CheckBox.TabIndex = 8;
            this.m_Player2CheckBox.CheckedChanged += new System.EventHandler(this.Player2CheckBox_CheckedChanged);
            this.m_Player2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)(0));
            this.m_Player2.Location = new System.Drawing.Point(45, 118);
            this.m_Player2.Name = "m_Player2";
            this.m_Player2.Size = new System.Drawing.Size(67, 23);
            this.m_Player2.TabIndex = 9;
            this.m_Player2.Text = "Player 2:";
            this.m_Player2TextBox.Enabled = false;
            this.m_Player2TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)(0));
            this.m_Player2TextBox.Location = new System.Drawing.Point(119, 118);
            this.m_Player2TextBox.MaxLength = 12;
            this.m_Player2TextBox.Name = "m_Player2TextBox";
            this.m_Player2TextBox.Size = new System.Drawing.Size(153, 23);
            this.m_Player2TextBox.TabIndex = 10;
            this.m_Player2TextBox.Text = "[Computer]";
            this.m_Player2TextBox.TextChanged += new System.EventHandler(this.Player2TextBox_TextChanged);
            this.ClientSize = new System.Drawing.Size(279, 202);
            this.Controls.Add(this.m_ButtonDone);
            this.Controls.Add(this.m_TopLabel);
            this.Controls.Add(this.m_Radio6X6);
            this.Controls.Add(this.m_Radio8X8);
            this.Controls.Add(this.m_Radio10X10);
            this.Controls.Add(this.m_Players);
            this.Controls.Add(this.m_Player1);
            this.Controls.Add(this.m_Player1TextBox);
            this.Controls.Add(this.m_Player2CheckBox);
            this.Controls.Add(this.m_Player2);
            this.Controls.Add(this.m_Player2TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "StartWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Player2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.m_Player2TextBox.Enabled = true;
        }

        private void Radio6X6_CheckedChanged(object sender, EventArgs e)
        {
            m_boardSize = 6;
        }

        private void Radio8X8_CheckedChanged(object sender, EventArgs e)
        {
            m_boardSize = 8;
        }

        private void Radio10X10_CheckedChanged(object sender, EventArgs e)
        {
            m_boardSize = 10;
        }

        private void Player1TextBox_TextChanged(object sender, EventArgs e)
        {
            m_Player1Name = m_Player1TextBox.Text;
        }

        private void Player2TextBox_TextChanged(object sender, EventArgs e)
        {
            m_Player2Name = m_Player2TextBox.Text;
        }
    }
}