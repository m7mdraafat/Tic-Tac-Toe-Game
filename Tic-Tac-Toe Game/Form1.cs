using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
       Color color=Color.FromArgb(255, 255, 255,255);


        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1; 

        enum enPlayer
        {
            Player1,
            Player2

        }
        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            InProgress
        }
        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount; 

        }
        public bool CheckValue(Button btn1 , Button btn2 , Button btn3)
        {
             if(btn1.Tag.ToString()!="?" && btn1.Tag==btn2.Tag && btn1.Tag==btn3.Tag)
             {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor=Color.GreenYellow;
                btn3.BackColor=Color.GreenYellow;

                if(btn1.Tag.ToString()=="X")
                {
                    GameStatus.Winner = enWinner.Player1; 
                    GameStatus.GameOver = true;
                    EndGame();
                    return true; 
                }
                else
                {
                    GameStatus.Winner=enWinner.Player2;
                    GameStatus.GameOver=true;
                    EndGame();
                    return true; 

                }
             }
            GameStatus.GameOver = false; 
            return false; 
        }

        public void CheckWinner()
        {
            // Check Row1 

            if (CheckValue(button1, button2, button3))
                return;
            //Check Row2 
            if (CheckValue(button4, button5, button6))
                return;
            //Check Row3
            if (CheckValue(button7, button8, button9))
                return;

            //Check Coloumn1 
            if (CheckValue(button1, button4, button7))
                return;
            //Check Coloumn2
            if (CheckValue(button2, button5, button8))
                return;
            //Check Coloumn3
            if (CheckValue(button3, button6, button9))
                return;

            // Check Diagonal1 
            if (CheckValue(button1, button5, button9))
                return;

            // Check Diagonal1 
            if (CheckValue(button3, button5, button7))
                return;
        }
        void EndGame()
        {
            lblPlayerTurn.Text = "GameOver"; 
            switch(GameStatus.Winner) {
            
                case enWinner.Player1:
                    lblWinner.Text = "  Player 1";
                    break; 
                case enWinner.Player2:
                    lblWinner.Text = "  Player 2";
                    break;
                default: 
                    lblWinner.Text = "   Draw";
                    break; 
            }

            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
        void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?" && GameStatus.GameOver == false)
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        lblPlayerTurn.Text = "Player 2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lblPlayerTurn.Text = "Player 1";
                        GameStatus.PlayCount++;
                        btn.Tag = "O";
                        CheckWinner();
                        break;
                }
            }

            else if (GameStatus.GameOver == false)
            {
                MessageBox.Show("Wrong Choice", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("GameOver, Reset The Game ", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            if (GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            Pen whitePen = new Pen(color);

            whitePen.Width = 15;
            //whitePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //draw Horizental lines
            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);

            //draw Vertical lines
            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);



        }
        void ResetButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent; 
        }
        void ResetGame()
        {
           
            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

            PlayerTurn = enPlayer.Player1;
            lblPlayerTurn.Text = "Player 1";
            GameStatus.Winner = enWinner.InProgress;
            lblWinner.Text = "In Progress";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false; 


            
        }
        void Mode(RadioButton btn)
        {
            if(btn.Tag.ToString() == "D")
            {
                // Draw Lines 
                color=Color.FromArgb(255, 255, 255,255);

                this.BackColor = Color.Black;
                label1.ForeColor = Color.Yellow;
                lblPlayerTurn.ForeColor = Color.White;
                lable3.ForeColor = Color.Yellow;
                lblWinner.ForeColor = Color.White;
                button10.ForeColor = Color.White;
                lblGame.ForeColor = Color.White;
                gpMode.ForeColor = Color.White;
             
            }
            else if(btn.Tag.ToString() == "L")
            {
                color = Color.FromArgb(255, 0, 0); 

                this.BackColor = Color.White;
                label1.ForeColor = Color.Red;
                lblPlayerTurn.ForeColor = Color.Black;
                lable3.ForeColor = Color.Red;
                lblWinner.ForeColor = Color.Blue;
                button10.ForeColor = Color.Black;
                lblGame.ForeColor = Color.Black;
                gpMode.ForeColor = Color.Black;
 
            }
        }

        

        // sender who the button recently click
        private void button_Click(object sender , EventArgs e)
        {
            ChangeImage((Button)sender);
        }
     
        private void lblWinner_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            ResetGame(); 
        }

        private void rbDark_CheckedChanged(object sender, EventArgs e)
        {
            Mode(rbDark); 
        }

        private void rbLight_CheckedChanged(object sender, EventArgs e)
        {
            Mode(rbLight); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ResetGame();
            rbDark.Focus();
          
        }

       
    }
}
