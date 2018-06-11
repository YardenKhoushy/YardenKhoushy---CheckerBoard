using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CheckersLogic
{
    public class Validation
    {
        private bool eatingIsEnabled = false;

        public bool CanEat
        {
            get
            {
                return this.eatingIsEnabled;
            }

            set
            {
                this.eatingIsEnabled = value;
            }
        }

        private bool PositionIsOccupied(Point i_TargetPosition, Piece[,] i_Board)
        {
            return i_Board[i_TargetPosition.X, i_TargetPosition.Y] != null;
        }

        private bool IsMoveInBorders(Point i_Target, int i_SizeOfBoard)
        {
            bool moveIsInBorders = true;
            if (i_Target.X >= i_SizeOfBoard || i_Target.X < 0)
            {
                moveIsInBorders = false;
            }

            if (i_Target.Y >= i_SizeOfBoard || i_Target.Y < 0)
            {
                moveIsInBorders = false;
            }

            return moveIsInBorders;
        }

        private bool MoveIsDiagonal(Point i_currentPosition, Point i_TargetPosition, string i_PieceType)
        {
            bool validMove = false;
            if (i_PieceType == "X")
            {
                if (DiagonalUpLeft(i_currentPosition, i_TargetPosition) || DiagonalUpRight(i_currentPosition, i_TargetPosition))
                {
                    validMove = true;
                }
            }
            else
            {
                if (i_PieceType == "O")
                {
                    if (DiagonalDownRight(i_currentPosition, i_TargetPosition) || DiagonalDownLeft(i_currentPosition, i_TargetPosition))
                    {
                        validMove = true;
                    }
                }
                else
                {
                    if (DiagonalUpLeft(i_currentPosition, i_TargetPosition) || DiagonalUpRight(i_currentPosition, i_TargetPosition) || DiagonalDownRight(i_currentPosition, i_TargetPosition) || DiagonalDownLeft(i_currentPosition, i_TargetPosition))
                    {
                        validMove = true;
                    }
                }
            }

            return validMove;
        }

        private bool DiagonalUpLeft(Point i_currentPosition, Point i_TargetPosition)
        {
            bool moveIsDiagonal = false;
            if ((i_currentPosition.Y - 1 == i_TargetPosition.Y) && (i_currentPosition.X == i_TargetPosition.X + 1))
            {
                moveIsDiagonal = true;
            }

            return moveIsDiagonal;
        }

        private bool DiagonalUpRight(Point i_currentPosition, Point i_TargetPosition)
        {
            bool moveIsDiagonal = false;
            if ((i_currentPosition.Y + 1 == i_TargetPosition.Y) && (i_currentPosition.X == i_TargetPosition.X + 1))
            {
                moveIsDiagonal = true;
            }

            return moveIsDiagonal;
        }

        private bool DiagonalDownLeft(Point i_currentPosition, Point i_TargetPosition)
        {
            bool moveIsDiagonal = false;
            if ((i_currentPosition.Y - 1 == i_TargetPosition.Y) && (i_currentPosition.X + 1 == i_TargetPosition.X))
            {
                moveIsDiagonal = true;
            }

            return moveIsDiagonal;
        }

        private bool DiagonalDownRight(Point i_currentPosition, Point i_TargetPosition)
        {
            bool moveIsDiagonal = false;
            if ((i_currentPosition.Y + 1 == i_TargetPosition.Y) && (i_currentPosition.X + 1 == i_TargetPosition.X))
            {
                moveIsDiagonal = true;
            }

            return moveIsDiagonal;
        }

        public bool Can_Eat(Point i_CurrentPosition, Point i_TargetPosition, Piece[,] i_CurrentBoard, string i_Turn, int i_SizeOfBoard)
        {
            bool canEatPlayerPiece = false;
            if (IsMoveInBorders(i_TargetPosition, i_SizeOfBoard))
            {
                bool currentPieceNextPositionIsOccupied = PositionIsOccupied(i_TargetPosition, i_CurrentBoard);
                Point pieceToBeEatenPosition;
                int pieceToBeEatenColumn = (i_CurrentPosition.Y + i_TargetPosition.Y) / 2;
                int pieceToBeEatenRow = (i_CurrentPosition.X + i_TargetPosition.X) / 2;
                pieceToBeEatenPosition = new Point(pieceToBeEatenRow, pieceToBeEatenColumn);
                if (!currentPieceNextPositionIsOccupied)
                {
                    if (i_Turn == "X")
                    {
                        if (DiagonalUpLeftForEat(i_CurrentPosition, i_TargetPosition) || DiagonalUpRightForEat(i_CurrentPosition, i_TargetPosition))
                        {
                            if (i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn] != null)
                            {
                                if (i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn].Type == "O" || i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn].Type == "U")
                                {
                                    canEatPlayerPiece = true;
                                    this.eatingIsEnabled = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (i_Turn == "O")
                        {
                            if (DiagonalDownLeftForEat(i_CurrentPosition, i_TargetPosition) || DiagonalDownRightForEat(i_CurrentPosition, i_TargetPosition))
                            {
                                if (i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn] != null)
                                {
                                    if (i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn].Type == "X" || i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn].Type == "K")
                                    {
                                        canEatPlayerPiece = true;
                                        this.eatingIsEnabled = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (i_Turn == "K")
                            {
                                if (i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn] != null)
                                {
                                    if (i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn].Type == "O" || i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn].Type == "U")
                                    {
                                        canEatPlayerPiece = true;
                                        this.eatingIsEnabled = true;
                                    }
                                }
                            }
                            else
                            {
                                if (i_Turn == "U")
                                {
                                    if (i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn] != null)
                                    {
                                        if (i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn].Type == "X" || i_CurrentBoard[pieceToBeEatenRow, pieceToBeEatenColumn].Type == "K")
                                        {
                                            canEatPlayerPiece = true;
                                            this.eatingIsEnabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!canEatPlayerPiece)
            {
                this.eatingIsEnabled = false;
            }

            return canEatPlayerPiece;
        }

        public bool MoveIsValid(Point i_CurrentPosition, Point i_TargetPosition, Board i_Board, string i_PieceType)
        {
            bool isValidMove = true;
            string type = i_Board.GetBoard()[i_CurrentPosition.X, i_CurrentPosition.Y].Type;

            if (!IsMoveInBorders(i_TargetPosition, i_Board.GetsizeOfBoard))
            {
                isValidMove = false;
            }
            else
            {
                if (PositionIsOccupied(i_TargetPosition, i_Board.GetBoard()))
                {
                    isValidMove = false;
                }
                else
                {
                    if (!MoveIsDiagonal(i_CurrentPosition, i_TargetPosition, i_Board.GetBoard()[i_CurrentPosition.X, i_CurrentPosition.Y].Type) && isValidMove)
                    {
                        isValidMove = false;
                    }
                }

                if (type == "X")
                {
                    if (Can_Eat(i_CurrentPosition, i_TargetPosition, i_Board.GetBoard(), type, i_Board.GetsizeOfBoard))
                    {
                        isValidMove = true;
                    }
                }
                else
                {
                    if (type == "O")
                    {
                        if (Can_Eat(i_CurrentPosition, i_TargetPosition, i_Board.GetBoard(), type, i_Board.GetsizeOfBoard))
                        {
                            isValidMove = true;
                        }
                    }
                    else
                    {
                        if (Can_Eat(i_CurrentPosition, i_TargetPosition, i_Board.GetBoard(), type, i_Board.GetsizeOfBoard))
                        {
                            isValidMove = true;
                        }
                    }
                }
            }

            return isValidMove;
        }

        private bool DiagonalUpLeftForEat(Point i_currentPosition, Point i_TargetPosition)
        {
            bool moveIsDiagonal = false;
            if ((i_currentPosition.Y - 2 == i_TargetPosition.Y) && (i_currentPosition.X == i_TargetPosition.X + 2))
            {
                moveIsDiagonal = true;
            }

            return moveIsDiagonal;
        }

        private bool DiagonalUpRightForEat(Point i_currentPosition, Point i_TargetPosition)
        {
            bool moveIsDiagonal = false;
            if ((i_currentPosition.Y + 2 == i_TargetPosition.Y) && (i_currentPosition.X == i_TargetPosition.X + 2))
            {
                moveIsDiagonal = true;
            }

            return moveIsDiagonal;
        }

        private bool DiagonalDownLeftForEat(Point i_currentPosition, Point i_TargetPosition)
        {
            bool moveIsDiagonal = false;
            if ((i_currentPosition.Y - 2 == i_TargetPosition.Y) && (i_currentPosition.X + 2 == i_TargetPosition.X))
            {
                moveIsDiagonal = true;
            }

            return moveIsDiagonal;
        }

        private bool DiagonalDownRightForEat(Point i_currentPosition, Point i_TargetPosition)
        {
            bool moveIsDiagonal = false;
            if ((i_currentPosition.Y + 2 == i_TargetPosition.Y) && (i_currentPosition.X + 2 == i_TargetPosition.X))
            {
                moveIsDiagonal = true;
            }

            return moveIsDiagonal;
        }
    }
}