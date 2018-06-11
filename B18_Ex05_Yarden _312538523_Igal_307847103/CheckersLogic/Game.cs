using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CheckersLogic
{
    public class Game
    {
        private eGameStatus m_CurrentGameStatus;
        private string m_FirstPlayerName;
        private string m_SecondPlayerName;
        private int m_SizeOfBoard;
        private string m_currentTurn = "X";
        private Validation m_validator;
        public bool m_JustAte = false;
        private Board m_Board;
        private Player m_firstPlayer;
        private Player m_opponent;
        private bool isComputerPlaying = false;

        public Game(int i_BoardSize, string i_FirstPlayer, string i_SecondPlayer)
        {
            m_SizeOfBoard = i_BoardSize;
            m_FirstPlayerName = i_FirstPlayer;
            m_SecondPlayerName = i_SecondPlayer;
            m_firstPlayer = new Player(m_FirstPlayerName, "X", m_SizeOfBoard);
            m_opponent = new Player(m_SecondPlayerName, "O", m_SizeOfBoard);
            m_Board = new Board(i_BoardSize, m_firstPlayer.GetPieces(), m_opponent.GetPieces());
            m_validator = new Validation();
            if (i_SecondPlayer == "Computer")
            {
                this.isComputerPlaying = true;
            }

            m_CurrentGameStatus = eGameStatus.Playing;
        }

        public void NewGame()
        {
            m_firstPlayer = new Player(m_FirstPlayerName, "X", m_SizeOfBoard);
            m_opponent = new Player(m_SecondPlayerName, "O", m_SizeOfBoard);
            m_Board = new Board(m_SizeOfBoard, m_firstPlayer.GetPieces(), m_opponent.GetPieces());
            m_currentTurn = "X";
            m_CurrentGameStatus = eGameStatus.Playing;
        }

        public eGameStatus GameStatus
        {
            get
            {
                return this.m_CurrentGameStatus;
            }

            set
            {
                this.m_CurrentGameStatus = value;
            }
        }

        public bool CheckCurrentMoveAndMove(Point i_CurrentPoint, Point i_TargetPoint)
        {
            bool moveIsValid = true;
            if (m_currentTurn == "X" || (m_currentTurn == "O" && !isComputerPlaying))
            {
                moveIsValid = m_validator.MoveIsValid(i_CurrentPoint, i_TargetPoint, m_Board, m_currentTurn);
                if (moveIsValid)
                {
                    m_Board.MovePieceOnBoard(i_CurrentPoint, i_TargetPoint, m_currentTurn);
                    if (m_validator.CanEat)
                    {
                        Point toEat = PositionToEat(i_CurrentPoint, i_TargetPoint);
                        m_Board.EatFormBoard(toEat, m_currentTurn);
                        Point targetAfterDoubleEat;
                        if (IsAbleToEatAgain(i_TargetPoint, m_Board, out targetAfterDoubleEat))
                        {
                            m_Board.MovePieceOnBoard(i_TargetPoint, targetAfterDoubleEat, m_currentTurn);
                            toEat = PositionToEat(i_TargetPoint, targetAfterDoubleEat);
                            m_Board.EatFormBoard(toEat, m_currentTurn);
                        }
                    }
                }
            }

            return moveIsValid;
        }

        public void ComputerEat(Point i_CurrentPoint, Point i_TargetPoint)
        {
            string pieceType = m_Board.GetBoard()[i_CurrentPoint.X, i_CurrentPoint.Y].Type;
            bool canEat = m_validator.Can_Eat(i_CurrentPoint, i_TargetPoint, m_Board.GetBoard(), pieceType, m_Board.GetsizeOfBoard);
            if (canEat)
            {
                Point toEat = PositionToEat(i_CurrentPoint, i_TargetPoint);
                m_Board.EatFormBoard(toEat, pieceType);
            }
        }

        public void SwitchTurn()
        {
            if (m_currentTurn == "X")
            {
                m_currentTurn = "O";
            }
            else
            {
                m_currentTurn = "X";
            }
        }

        private Point PositionToEat(Point i_CurrentPosition, Point i_TargetPosition)
        {
            Point positionToEat;
            int pieceToBeEatenColumn = (i_CurrentPosition.Y + i_TargetPosition.Y) / 2;
            int pieceToBeEatenRow = (i_CurrentPosition.X + i_TargetPosition.X) / 2;
            positionToEat = new Point(pieceToBeEatenRow, pieceToBeEatenColumn);
            return positionToEat;
        }

        public void CheckGameStatus()
        {
            m_firstPlayer.PlayerPossibleMoves(m_Board);
            int PlayerOneMoveCounter = m_firstPlayer.GetPlayerPossibleMoves().Count;
            m_opponent.PlayerPossibleMoves(m_Board);
            int PlayerTwoMoveCount = m_opponent.GetPlayerPossibleMoves().Count;

            if (PlayerOneMoveCounter == 0 && PlayerTwoMoveCount == 0)
            {
                m_CurrentGameStatus = eGameStatus.Draw;
            }
            else
            {
                if (PlayerOneMoveCounter > 0 && PlayerTwoMoveCount == 0)
                {
                    m_CurrentGameStatus = eGameStatus.PlayerOneWin;
                }
                else
                {
                    if (PlayerOneMoveCounter == 0 && PlayerTwoMoveCount > 0)
                    {
                        m_CurrentGameStatus = eGameStatus.PlayerTwoWin;
                    }
                }
            }
        }

        public void CalculateGameScore(ref int i_PlayerOneScore, ref int i_PlayerTwoScore)
        {
            m_firstPlayer.Points = 0;
            m_opponent.Points = 0;
            foreach (Piece currentPiece in m_Board.GetBoard())
            {
                if (currentPiece != null)
                {
                    switch (currentPiece.Type)
                    {
                        case "X":
                            m_firstPlayer.Points++;
                            break;
                        case "K":
                            m_firstPlayer.Points += 4;
                            break;
                        case "O":
                            m_opponent.Points++;
                            break;
                        default:
                            m_opponent.Points += 4;
                            break;
                    }
                }
            }

            i_PlayerOneScore = m_firstPlayer.Points - m_opponent.Points;
            i_PlayerTwoScore = m_opponent.Points - m_firstPlayer.Points;
        }

        public bool IsAbleToEatAgain(Point currentPositionOfPiece, Board i_playingBoard, out Point i_TargetPosition)
        {
            bool canEat = false;
            int currentRow = currentPositionOfPiece.X;
            int currentColumn = currentPositionOfPiece.Y;
            int possibleRowDestiantion;
            int possibleColumnDestiantion;
            Point target = new Point();
            if (m_currentTurn == "X")
            {
                possibleRowDestiantion = currentRow - 2;
                possibleColumnDestiantion = currentColumn + 2;
                target.X = possibleRowDestiantion;
                target.Y = possibleColumnDestiantion;
                canEat = m_validator.Can_Eat(currentPositionOfPiece, target, i_playingBoard.GetBoard(), m_currentTurn, m_Board.GetsizeOfBoard);
                if (!canEat)
                {
                    possibleRowDestiantion = currentRow - 2;
                    possibleColumnDestiantion = currentColumn - 2;
                    target.X = possibleRowDestiantion;
                    target.Y = possibleColumnDestiantion;
                    canEat = m_validator.Can_Eat(currentPositionOfPiece, target, i_playingBoard.GetBoard(), m_currentTurn, m_Board.GetsizeOfBoard);
                }
            }
            else
            {
                possibleRowDestiantion = currentRow + 2;
                possibleColumnDestiantion = currentColumn + 2;
                target.X = possibleRowDestiantion;
                target.Y = possibleColumnDestiantion;
                canEat = m_validator.Can_Eat(currentPositionOfPiece, target, i_playingBoard.GetBoard(), m_currentTurn, m_Board.GetsizeOfBoard);
                if (!canEat)
                {
                    possibleRowDestiantion = currentRow + 2;
                    possibleColumnDestiantion = currentColumn - 2;
                    target.X = possibleRowDestiantion;
                    target.Y = possibleColumnDestiantion;
                    canEat = m_validator.Can_Eat(currentPositionOfPiece, target, i_playingBoard.GetBoard(), m_currentTurn, m_Board.GetsizeOfBoard);
                }
            }

            i_TargetPosition = target;
            return canEat;
        }

        private Point RandomMove(Player i_computer, Board i_currentBoard)
        {
            Dictionary<Point, List<Point>> moves = new Dictionary<Point, List<Point>>();
            i_computer.PlayerPossibleMoves(i_currentBoard);
            moves = i_computer.GetPlayerPossibleMoves();
            Point randomMove = new Point(-1, -1);
            if (moves.Count > 0)
            {
                Random random = new Random();
                List<Point> listOfKeys = new List<Point>();
                foreach (Point keyPoint in moves.Keys)
                {
                    if (moves[keyPoint].Count != 0)
                    {
                        listOfKeys.Add(keyPoint);
                    }
                }

                Point randomKey = listOfKeys[(int)random.Next(0, listOfKeys.Count)];
                List<Point> listOfValues = moves[randomKey];
                randomMove = listOfValues[random.Next(0, listOfValues.Count)];
            }

            return randomMove;
        }

        public Point GetRandomComputerMove()
        {
            Point targetPoint = RandomMove(m_opponent, m_Board);
            return targetPoint;
        }

        public Point GetComputerKey(Point i_Target)
        {
            Dictionary<Point, List<Point>> computerMoves = m_opponent.GetPlayerPossibleMoves();
            Point key = new Point();
            foreach (Point PointKey in computerMoves.Keys)
            {
                if (computerMoves[PointKey].Contains(i_Target))
                {
                    key = PointKey;
                }
            }

            return key;
        }

        public Point ComputerMove(out Point i_CurrentPosition)
        {
            Point targetPosition = GetRandomComputerMove();
            Point currentPosition = new Point(-1, -1);
            if (targetPosition.X != -1)
            {
                currentPosition = GetComputerKey(targetPosition);
                ComputerEat(currentPosition, targetPosition);
                Board.MovePieceOnBoard(currentPosition, targetPosition, "O");
                SwitchTurn();
            }

            i_CurrentPosition = currentPosition;
            return targetPosition;
        }

        public Board Board
        {
            get
            {
                return this.m_Board;
            }
        }

        public string CurrentTurn
        {
            get
            {
                return this.m_currentTurn;
            }

            set
            {
                this.m_currentTurn = value;
            }
        }

        public Player PlayerOne
        {
            get
            {
                return this.m_firstPlayer;
            }
        }

        public Player PlayerTwo
        {
            get
            {
                return this.m_opponent;
            }
        }
    }
}
