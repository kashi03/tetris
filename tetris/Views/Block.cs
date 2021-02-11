using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace tetris.Views
{
    class Block
    {
        private Graphics g;
        public int x, y;
        public Block(Graphics g, int x, int y)
        {
            this.g = g;
            this.x = x;
            this.y = y;
        }
        public void draw()
        {
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.LightGreen);

            int i = 25;
            Rectangle rect = new Rectangle(i * this.x, i * this.y, i, i);
            this.g.FillRectangle(brush, rect);
            this.g.DrawRectangle(pen, rect);

            //pen.Dispose();
            //brush.Dispose();
        }
    }
}
