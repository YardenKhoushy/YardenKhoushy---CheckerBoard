using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CheckersLogic;
using CheckerBoardUI.Properties;

namespace CheckerBoardUI
{
    public class CheckerBoard : Form
    {
        private const int k_ButtonSize = 60;
        private int m_BoardSize;
        private List<Button> m_ListOfButtons;
        private Button[,] m_ButtonMatrix;
        private Label m_Player1Score;
        private Label m_Player2Score;
        private string m_Player1Name;
        private string m_Player2Name;
        private Point m_CurrentPoint;
        private Point m_TargetPoint;
        private string m_CurrentText = string.Empty;
        private int m_PlayerOnePoints = 0;
        private int m_PlayerTwoPoints = 0;
        private Game m_Game;

        public Button[,] ButtonsMatrix
        {
            get
            {
                return this.m_ButtonMatrix;
            }
        }

        public CheckerBoard(int i_BoardSize, string i_Player1, string i_Player2)
        {
            this.Text = "Damka";
            m_BoardSize = i_BoardSize;
            m_Player1Name = i_Player1;
            m_Player2Name = i_Player2;
            m_ListOfButtons = new List<Button>();
            m_Game = new Game(i_BoardSize, m_Player1Name, m_Player2Name);
            m_CurrentPoint = new Point(-1, -1);
            BuildCheckerBoard();
            this.Width = 10 + (i_BoardSize * k_ButtonSize);
            this.Height = 10 + (i_BoardSize * k_ButtonSize);
            this.AutoSize = true;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void BuildButtonSettings(ButtonPositionOnBoard i_Button, bool i_isEnabled, Point i_Point, string i_TypeOfPiece, Color i_Color, Size i_Size)
        {
            i_Button.Enabled = i_isEnabled;
            i_Button.Location = i_Point;
            i_Button.BackColor = i_Color;
            i_Button.Text = i_TypeOfPiece;
            i_Button.Size = i_Size;
        }

        private void BuildCheckerBoard()
        {
            m_ButtonMatrix = new Button[m_BoardSize, m_BoardSize];
            BuildNames();
            BuildORows();
            BuildTwoRowSpace();
            BuildXRows();
            SwitchEnabledButtons("X");
            BuildLogicBoard();
        }

        private void BuildLogicBoard()
        {
            Piece[,] logicBoard = m_Game.Board.GetBoard();
            foreach (ButtonPositionOnBoard currentButton in m_ButtonMatrix)
            {
                Piece currentPiece = logicBoard[currentButton.X, currentButton.Y];
                if (currentPiece == null)
                {
                    currentButton.Text = string.Empty;
                    if (currentButton.BackColor == Color.White)
                    {
                        currentButton.Image = Properties.Resources.WhiteButton;
                    }
                }
                else
                {
                    string type = currentPiece.Type;
                    SetButton(type, currentButton);
                }
            }
        }

        private void BuildNames()
        {
            m_Player1Score = new Label();
            m_Player1Score.AutoSize = true;
            m_Player1Score.Text = m_Player1Name + ":" + m_PlayerOnePoints;
            m_Player1Score.Location = new Point((this.Width / 6), k_ButtonSize / 2 - 15);
            m_Player1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Controls.Add(m_Player1Score);
            m_Player2Score = new Label();
            m_Player2Score.AutoSize = true;
            m_Player2Score.Text = m_Player2Name + ": " + m_PlayerTwoPoints;
            m_Player2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_Player2Score.Location = new Point((this.Width / 5) * 3, k_ButtonSize / 2 - 15);
            Controls.Add(m_Player2Score);
        }

        private void BuildXRows()
        {
            int row = (m_BoardSize / 2) + 1;
            Point boardPosition = new Point(10, m_ButtonMatrix[row - 1, 0].Location.Y + 60);
            Size positionSize = new Size(60, 60);
            bool isEnabled;
            string pieceType = "X";
            string emptyString = string.Empty;
            Color buttonColor;
            for (int i = row; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    ButtonPositionOnBoard currentButton = new ButtonPositionOnBoard(i, j);
                    if (j % 2 != i % 2)
                    {
                        buttonColor = Color.White;
                        isEnabled = true;
                        BuildButtonSettings(currentButton, isEnabled, boardPosition, pieceType, buttonColor, positionSize);
                        currentButton.Image = Properties.Resources.BlackGoodPiece;
                        m_ButtonMatrix[i, j] = currentButton;
                        m_ButtonMatrix[i, j].Click += new EventHandler(SquareOneClick_Click_EventHandler);
                    }
                    else
                    {
                        buttonColor = Color.Gray;
                        isEnabled = false;
                        BuildButtonSettings(currentButton, isEnabled, boardPosition, emptyString, buttonColor, positionSize);
                        m_ButtonMatrix[i, j] = currentButton;
                    }

                    Controls.Add(m_ButtonMatrix[i, j]);
                    isEnabled = !isEnabled;
                    boardPosition.X += 60;
                }

                boardPosition.X = 10;
                boardPosition.Y += 60;
            }
        }

        private void BuildTwoRowSpace()
        {
            int row = (m_BoardSize / 2) - 1;
            string emptyString = string.Empty;
            Color buttonColor = m_ButtonMatrix[row - 1, m_BoardSize - 1].BackColor;
            Point boardPosition = new Point(10, m_ButtonMatrix[row - 1, 0].Location.Y + 60);
            Size positionSize = new Size(60, 60);
            bool isEnabled = false;
            if (buttonColor == Color.White)
            {
                isEnabled = true;
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    ButtonPositionOnBoard currentButton = new ButtonPositionOnBoard(row, j);
                    BuildButtonSettings(currentButton, isEnabled, boardPosition, emptyString, buttonColor, positionSize);
                    m_ButtonMatrix[row, j] = currentButton;
                    m_ButtonMatrix[row, j].Click += new EventHandler(SquareOneClick_Click_EventHandler);
                    Controls.Add(m_ButtonMatrix[row, j]);
                    isEnabled = !isEnabled;
                    boardPosition.X += 60;
                    if (buttonColor == Color.Gray)
                    {
                        buttonColor = Color.White;
                    }
                    else
                    {
                        buttonColor = Color.Gray;
                    }
                }

                boardPosition.X = 10;
                boardPosition.Y += 60;
                buttonColor = m_ButtonMatrix[row, m_BoardSize - 1].BackColor;
                isEnabled = m_ButtonMatrix[row, m_BoardSize - 1].Enabled;
                row++;
            }
        }

        private void BuildORows()
        {
            Point boardPosition = new Point(10, 60);
            Size positionSize = new Size(60, 60);
            bool isEnabled = false;
            string pieceType = "O";
            string emptyString = string.Empty;
            Color buttonColor = Color.Gray;

            for (int i = 0; i < (m_BoardSize / 2) - 1; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    ButtonPositionOnBoard currentButton = new ButtonPositionOnBoard(i, j);
                    if (j % 2 != i % 2)
                    {
                        buttonColor = Color.White;
                        BuildButtonSettings(currentButton, isEnabled, boardPosition, pieceType, buttonColor, positionSize);
                        currentButton.Image = Properties.Resources.RedButton;
                        m_ButtonMatrix[i, j] = currentButton;
                        m_ButtonMatrix[i, j].Click += new EventHandler(SquareOneClick_Click_EventHandler);
                    }
                    else
                    {
                        buttonColor = Color.Gray;
                        BuildButtonSettings(currentButton, isEnabled, boardPosition, emptyString, buttonColor, positionSize);
                        m_ButtonMatrix[i, j] = currentButton;
                    }

                    Controls.Add(m_ButtonMatrix[i, j]);
                    isEnabled = !isEnabled;
                    boardPosition.X += 60;
                }

                boardPosition.X = 10;
                boardPosition.Y = 60 + (60 * (i + 1));
                isEnabled = m_ButtonMatrix[i, m_BoardSize - 1].Enabled;
            }
        }

        private void SquareOneClick_Click_EventHandler(object sender, EventArgs e)
        {
            ButtonPositionOnBoard button = sender as ButtonPositionOnBoard;
            MakeBoardWhiteAndGray();

            if (m_CurrentPoint.X == -1 || m_CurrentPoint.Y == -1)
            {
                if (button.Text != string.Empty)
                {
                    m_CurrentPoint.X = button.X;
                    m_CurrentPoint.Y = button.Y;
                    button.BackColor = Color.LightBlue;
                    m_CurrentText = button.Text;
                }
            }
            else
            {
                m_TargetPoint = new Point(button.X, button.Y);
                if (m_CurrentPoint == m_TargetPoint)
                {
                    button.BackColor = Color.White;
                    m_CurrentPoint.X = -1;
                    m_CurrentPoint.Y = -1;
                }
                else
                {
                    m_ButtonMatrix[m_CurrentPoint.X, m_CurrentPoint.Y].BackColor = Color.White;
                    PlayerMakingMoveClick();
                    SetLabelUpdatedScore();
                }
            }

            if (m_Player2Name == "Computer" && m_Game.CurrentTurn == "O")
            {
                ComputerMove();
                CheckIfCanContinue();
                SetLabelUpdatedScore();
                m_CurrentPoint.X = -1;
                m_CurrentPoint.Y = -1;
                SwitchEnabledButtons(m_Game.CurrentTurn);
            }
        }

        private void SwitchEnabledButtons(string i_ButtonType)
        {
            foreach (ButtonPositionOnBoard button in m_ButtonMatrix)
            {
                if (button.Text != string.Empty)
                {
                    if (i_ButtonType == "K" || i_ButtonType == "X")
                    {
                        if (button.Text == "O" || button.Text == "U")
                        {
                            button.Enabled = false;
                        }
                        else
                        {
                            button.Enabled = true;
                        }
                    }
                    else
                    {
                        if (button.Text == "X" || button.Text == "K")
                        {
                            button.Enabled = false;
                        }
                        else
                        {
                            button.Enabled = true;
                        }
                    }
                }
                else
                {
                    if (button.BackColor != Color.Gray)
                    {
                        button.Enabled = true;
                    }
                }
            }
        }

        private void PlayerMakingMoveClick()
        {
            if (m_Game.CheckCurrentMoveAndMove(m_CurrentPoint, m_TargetPoint))
            {
                BuildLogicBoard();
                m_Game.SwitchTurn();
                SwitchEnabledButtons(m_Game.CurrentTurn);
                CheckIfCanContinue();
            }
            else
            {
                MessageBox.Show("Not a Valid Move", "Damka", MessageBoxButtons.OK);
            }

            m_CurrentPoint.X = -1;
            m_CurrentPoint.Y = -1;
            m_TargetPoint.X = -1;
            m_TargetPoint.Y = -1;
        }

        private void CheckIfCanContinue()
        {
            m_Game.CheckGameStatus();
            if (m_Game.GameStatus == eGameStatus.Draw || m_Game.GameStatus == eGameStatus.PlayerOneWin || m_Game.GameStatus == eGameStatus.PlayerTwoWin)
            {
                GameResult();
            }
        }

        private void GameResult()
        {
            m_Game.CalculateGameScore(ref m_PlayerOnePoints, ref m_PlayerTwoPoints);
            DialogResult answer = DialogResult.No;
            switch (m_Game.GameStatus)
            {
                case eGameStatus.PlayerOneWin:
                    answer = MessageBox.Show(m_Player1Name + " " + "Won!\nAnother Round?", "Damka", MessageBoxButtons.YesNo);
                    break;
                case eGameStatus.PlayerTwoWin:
                    answer = MessageBox.Show(m_Player2Name + " " + "Won!\nAnother Round?", "Damka", MessageBoxButtons.YesNo);
                    break;
                case eGameStatus.Draw:
                    answer = MessageBox.Show("Tie!\nAnother Round?", "Damka", MessageBoxButtons.YesNo);
                    break;
            }

            if (answer == DialogResult.Yes)
            {
                m_Game.NewGame();
                m_Game.PlayerOne.Points = m_PlayerOnePoints;
                m_Game.PlayerTwo.Points = m_PlayerTwoPoints;
                Controls.Remove(m_Player1Score);
                Controls.Remove(m_Player2Score);
                MakeBoardWhiteAndGray();
                BuildNames();
                BuildLogicBoard();
            }
            else
            {
                this.Close();
            }
        }

        private void ComputerMove()
        {
            Point targetPosition;
            targetPosition = m_Game.ComputerMove(out m_CurrentPoint);
            if (targetPosition.X != -1)
            {
                m_ButtonMatrix[m_CurrentPoint.X, m_CurrentPoint.Y].BackColor = Color.Yellow;
                m_ButtonMatrix[m_CurrentPoint.X, m_CurrentPoint.Y].Image = Properties.Resources.WhiteButton;
                m_ButtonMatrix[targetPosition.X, targetPosition.Y].BackColor = Color.Yellow;
            }

            BuildLogicBoard();
        }

        private void MakeBoardWhiteAndGray()
        {
            foreach (ButtonPositionOnBoard button in m_ButtonMatrix)
            {
                if (button.BackColor == Color.Yellow)
                {
                    button.BackColor = Color.White;
                }
            }
        }

        private void SetLabelUpdatedScore()
        {
            Controls.Remove(m_Player1Score);
            Controls.Remove(m_Player2Score);
            m_Game.CalculateGameScore(ref m_PlayerOnePoints, ref m_PlayerTwoPoints);
            BuildNames();
        }

        private void SetButton(string i_Type, ButtonPositionOnBoard currentButton)
        {
            switch (i_Type)
            {
                case "X":
                    currentButton.Text = i_Type;
                    currentButton.Image = Properties.Resources.BlackGoodPiece;
                    currentButton.ForeColor = Color.White;
                    break;
                case "K":
                    currentButton.Text = i_Type;
                    currentButton.Image = Properties.Resources.BlackGoodPiece;
                    currentButton.ForeColor = Color.White;
                    break;
                case "O":
                    currentButton.Text = i_Type;
                    currentButton.Image = Properties.Resources.RedButton;
                    currentButton.ForeColor = Color.White;
                    break;
                default:
                    currentButton.Text = i_Type;
                    currentButton.Image = Properties.Resources.RedButton;
                    currentButton.ForeColor = Color.White;
                    break;
            }
        }
    }
}
