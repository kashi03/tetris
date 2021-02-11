using System;
using System.Drawing;
using System.Windows.Forms;
using tetris.Views;

namespace tetris
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")] // この行を追加
        private static extern bool AllocConsole();
        Field field;
        BufferedGraphicsContext currentContext;
        BufferedGraphics myBuffer;
        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this.field = new Field(this.myBuffer.Graphics);
            this.draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 37:
                    this.field.move(-1, 0, 0);
                    break;
                case 38:
                    this.field.move(0, 0, 1);
                    break;
                case 39:
                    this.field.move(1, 0, 0);
                    break;
                case 40:
                    this.field.move(0, 0, -1);
                    //this.field.move(0, 1, 0);
                    break;
            }
            this.draw();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Field field = new Field(e.Graphics);
            //this.block = new Mino(e.Graphics, this.x, this.y);

            //this.field.draw();
            //this.block.draw();
            Console.WriteLine(Screen.AllScreens[0]);
            Console.WriteLine(Screen.AllScreens[1]);
            this.Location = Screen.AllScreens[1].Bounds.Location;


        }

        private void TickUpdateView(object sender, EventArgs e)
        {
            this.field.down();
            this.draw();
        }
        private void draw()
        {
            this.field.draw();
            this.myBuffer.Render();
        }
    }
}
