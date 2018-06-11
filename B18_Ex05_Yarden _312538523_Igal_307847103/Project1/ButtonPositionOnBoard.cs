using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CheckerBoardUI
{
    public class ButtonPositionOnBoard : Button
    {
        private int m_X;
        private int m_Y;

        public ButtonPositionOnBoard(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }

        public int X
        {
            get
            {
                return this.m_X;
            }

            set
            {
                this.m_X = value;
            }
        }

        public int Y
        {
            get
            {
                return this.m_Y;
            }

            set
            {
                this.m_Y = value;
            }
        }
    }
}
