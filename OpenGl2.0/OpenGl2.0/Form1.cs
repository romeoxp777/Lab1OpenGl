using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace OpenGl2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OpenGlo.InitializeContexts();

            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, OpenGlo.Width, OpenGlo.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            //  if (OpenGlo.Width <= OpenGlo.Height)
            //     Glu.gluOrtho2D(-50, OpenGlo.Width, -50, 50.0 * (float)OpenGlo.Height / (float)OpenGlo.Width);
            // else
            //     Glu.gluOrtho2D(-50, 50.0 * (float)OpenGlo.Width / (float)OpenGlo.Height, -50, 50.0);


            double aspect = OpenGlo.Width / (OpenGlo.Height);
            if (OpenGlo.Width >= OpenGlo.Height)
            {
                Glu.gluOrtho2D(-50.0 * aspect, 50.0 * aspect, -50.0, 50.0);
            }
            else
            {
                Glu.gluOrtho2D(-50.0, 50.0, -50.0 / aspect, 50.0 / aspect);
            }

            Gl.glMatrixMode(Gl.GL_MODELVIEW);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenGlo_Load(object sender, EventArgs e)
        {

        }
        public class Shape
        {
            public Point MainPoint;
            public float[] ColorArray = new float[3];
            public string NameOfShape;
            public int KoefOfChange;
        }

        private List<Shape> Shapes = new List<Shape>();
        private Random MyColor = new Random();
        private void Draw(Shape s)
        {
            int CountOfVertex = 0;
            if (s.NameOfShape == "Triangle") CountOfVertex = 3;
            if (s.NameOfShape == "Quadrangle") CountOfVertex = 4;
            if (s.NameOfShape == "Pentagon") CountOfVertex = 5;
            if (s.NameOfShape == "Hexagon") CountOfVertex = 6;

            Gl.glColor3d(s.ColorArray[0], s.ColorArray[1], s.ColorArray[2]);
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glVertex2d(s.MainPoint.X, s.MainPoint.Y);
            Gl.glVertex2d(s.MainPoint.X + s.KoefOfChange, s.MainPoint.Y);
            Gl.glVertex2d(s.MainPoint.X + s.KoefOfChange, s.MainPoint.Y - s.KoefOfChange);
            if (CountOfVertex > 5)
            {
                Gl.glVertex2d(s.MainPoint.X + s.KoefOfChange * 0.7, s.MainPoint.Y - s.KoefOfChange * 1.7);
                Gl.glVertex2d(s.MainPoint.X + s.KoefOfChange * 0.3, s.MainPoint.Y - s.KoefOfChange * 1.7);
                Gl.glVertex2d(s.MainPoint.X, s.MainPoint.Y - s.KoefOfChange);
            }
            else
            {
                if (CountOfVertex == 4) Gl.glVertex2d(s.MainPoint.X, s.MainPoint.Y - s.KoefOfChange);
                if (CountOfVertex == 5) Gl.glVertex2d(s.MainPoint.X + s.KoefOfChange * 0.5, s.MainPoint.Y + s.KoefOfChange);
            }



            Gl.glEnd();

            Gl.glFlush();
            Gl.glRasterPos2f(s.MainPoint.X, s.MainPoint.Y);
            text(s.NameOfShape);
            OpenGlo.Invalidate();
        }
        static void text(string c)
        {
            for (int i = 0; i < c.Length; i++)
            {
                // Render bitmap character 
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_10, c[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Shape s = new Shape();
            Random rand = new Random();
            s.ColorArray[0] = (float)MyColor.Next(0, 256) / 255;
            s.ColorArray[1] = (float)MyColor.Next(0, 256) / 255;
            s.ColorArray[2] = (float)MyColor.Next(0, 256) / 255;
            int CountOfVertex = rand.Next(3, 7);
            if (CountOfVertex == 3) s.NameOfShape = "Triangle";
            if (CountOfVertex == 4) s.NameOfShape = "Quadrangle";
            if (CountOfVertex == 5) s.NameOfShape = "Pentagon";
            if (CountOfVertex == 6) s.NameOfShape = "Hexagon";
            s.MainPoint.X = rand.Next(-30, 30);
            s.MainPoint.Y = rand.Next(-30, 30);
            s.KoefOfChange = rand.Next(-10, 10);
            Shapes.Add(s);
            for (int i = 0; i < Shapes.Count; i++)
            {
                Draw(Shapes[i]);
            }

            // Glut.glutDisplayFunc(Draw);
            // OpenGlo.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            if (Shapes.Count != 0)
            {
                Shapes.RemoveAt(Shapes.Count - 1);

                for (int i = 0; i < Shapes.Count; i++)
                {
                    Draw(Shapes[i]);
                }
                OpenGlo.Invalidate();
            }
            else
            {
                //OpenGlo.Invalidate();
                MessageBox.Show("All shapes are removed");

            }
        }
    }
}
