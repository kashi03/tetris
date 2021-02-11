using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using tetris.Views;

namespace tetris.Views
{
    class Mino
    {
        private Graphics g;
        public int x, y;
        public int rot;
        public int shape;
        Point[][] blocks_shape = new Point[7][] {
            new Point[] { new Point(-1, 0), new Point(0, 0), new Point(0, 1), new Point(1, 1) },  // Z
            new Point[] { new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(-1, 1) },  // S
            new Point[] { new Point(0, -2), new Point(0, -1), new Point(0, 0), new Point(0, 1) }, // I
            new Point[] { new Point(-1, 0), new Point(0, 0), new Point(0, 1), new Point(-1, 1) }, // O
            new Point[] { new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(1, 0) },// L
            new Point[] { new Point(0, 0), new Point(-1, 0), new Point(1, 0), new Point(1, -1) }, // J
            new Point[] { new Point(0, 0), new Point(1, 0), new Point(-1, 0), new Point(0, -1) }, // T
        };

        public Mino(Graphics g, int x, int y, int shape)
        {
            this.g = g;
            this.x = x;
            this.y = y;
            this.shape = shape;
            /*
            this.blocks = new Block[1][]{
                new Block[] { new Block(this.g, -1, 0), new Block(this.g, 0, 0), new Block(this.g, 0, 1), new Block(this.g, 1, 1) }
            };
             */
        }
        private Block[] calcBlock()
        {
            Block[] blocks = new Block[4];
            for (var i=0; i<blocks_shape[this.shape].Length; i++)
            {
                blocks[i] = new Block(this.g, blocks_shape[this.shape][i].X, blocks_shape[this.shape][i].Y);
            }

            for (var r = 0; r < (400000000+this.rot) % 4; r++)
            {
                for (var i = 0; i < blocks.Length; i++)
                {
                    int temp_x = blocks[i].x;
                    int temp_y = blocks[i].y;
                    blocks[i].x = -temp_y;
                    blocks[i].y = temp_x;
                }
            }
            return blocks;
        }
        public void draw()
        {
            Point[] points = getBlockPoints();
            foreach (var point in points)
            {
                Block block = new Block(this.g, point.X, point.Y);
                block.draw();
            }
            /*
            Block[] blocks = calcBlock();
            foreach (var block in blocks)
            {
                block.x += this.x;
                block.y += this.y;
                block.draw();
                //block.Dispose();
            }
             */
        }
        public Point[] getBlockPoints(int x=0, int y=0, int rot=0)
        {
            Point[] points = new Point[4];
            rot += this.rot;
            for (int i = 0; i < blocks_shape[this.shape].Length; i++)
            {
                int tx = blocks_shape[this.shape][i].X;
                int ty = blocks_shape[this.shape][i].Y;
                for (var r = 0; r < (400000000 + rot) % 4; r++)
                {
                    int temp_x = tx;
                    int temp_y = ty;
                    tx = -temp_y;
                    ty = temp_x;
                }
                points[i].X = tx + x + this.x;
                points[i].Y = ty + y + this.y;
            }
            return points;
        }
    }
}
