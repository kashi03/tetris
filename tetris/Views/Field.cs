using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace tetris.Views
{
    class Field
    {
        private int[][] stage = new int[22][] {
            new int[] { 1,1,1,0,0,0,0,0,0,1,1,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,0,0,0,0,0,0,0,0,0,0,1 },
            new int[] { 1,1,1,1,1,1,1,1,1,1,1,1 },
        };
        private Graphics g;
        public Mino mino;
        private Color[] colors = new Color[] { 
            Color.White,
            Color.Aqua,
            Color.LightGreen,
            Color.OrangeRed,
            Color.BlueViolet,
            Color.AliceBlue,
            Color.DarkCyan,
            Color.DarkOrange,
            Color.DarkSalmon,
        };
        public Field(Graphics g)
        {
            this.g = g;
            foreach (var item in this.stage)
            {
                Console.WriteLine(item.ToString());
            }
            Random rand = new System.Random();
            this.mino = new Mino(this.g, 5, 1, rand.Next(0,7));
        }

        public void draw()
        {
            this.g.Clear(Color.FromArgb(240, 240, 240));
            foreach (var item in this.checkLine())
            {
                this.removeLine(item);
            }
            for (int col = 0; col < stage.Length; col++)
            {
                for (int row = 0; row < stage[0].Length; row++)
                {
                    if (this.stage[col][row] == 0) continue;
                    Pen pen = new Pen(Color.Black, 1);
                    SolidBrush brush = new SolidBrush(this.colors[this.stage[col][row]]);
                    int i = 25;
                    Rectangle rect = new Rectangle(i * row, i * col, i, i);
                    this.g.FillRectangle(brush, rect);
                    this.g.DrawRectangle(pen, rect);
                    //pen.Dispose();
                    //brush.Dispose();
                }
            }
            this.mino.draw();
        }
        private List<int> checkLine()
        {
            List<int> voidLine = new List<int> { };
            for (int row = 1; row < stage.Length-1; row++)
            {
                int sum = 0;
                for (int col = 1; col < stage[0].Length-1; col++)
                {
                    sum += stage[row][col];
                }
                if (sum == 10) voidLine.Add(row);
            }
            return voidLine;
        }
        private bool checkLineVoid(int index)
        {
            Console.WriteLine(index);
            for (int col = 1; col < this.stage[index].Length - 1; col++)
            {
                if (this.stage[index][col] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private void removeLine(int index)
        {
            for (int col = 1; col < this.stage[index].Length - 1; col++)
            {
                this.stage[index][col] = 0;
            }
            for (int row = this.stage.Length-2; row > 1; row--)
            {
                if (this.checkLineVoid(row))
                {
                    for (int i = row; i > 1; i--)
                    {
                        if(this.checkLineVoid(i) == false)
                        {
                            this.stage[row] = this.stage[i].Clone() as int[];
                            for (int j = 1; j < this.stage[i].Length-1; j++)
                            {
                                this.stage[i][j] = 0;
                            }
                            break;
                        }
                    }
                }
            }
        }
        private bool canMove(int x, int y, int rot)
        {
            foreach (var point in this.mino.getBlockPoints(x, y, rot))
            {
                if (point.X <= 0) return false;
                if (this.stage[point.Y][point.X] >= 1 ) return false;
            }
            return true;
        }
        public void move(int x, int y, int rot)
        {
            if (this.canMove(x, y, rot))
            {
                this.mino.x += x;
                this.mino.y += y;
                this.mino.rot += rot;
            }
        }
        public void down()
        {
            if (this.canMove(0, 1, 0))
            {
                this.move(0, 1, 0);
            }
            else
            {
                this.nextMino();
            }
        }
        private void nextMino()
        {
            foreach (var point in this.mino.getBlockPoints())
            {
                this.stage[point.Y][point.X] = this.mino.shape + 2;
            }
            Random rand = new System.Random();
            this.mino = new Mino(this.g, 5, 1, rand.Next(0, 7));
        }
    }
}
