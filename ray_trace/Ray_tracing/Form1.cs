using System;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Text.RegularExpressions;
using System.IO;

namespace Ray_tracing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            glControl1.Invalidate();
        }

        float v1 = 0;
        float v2 = 0;
        float v3 = -4;

        float v1_v = 0;
        float v2_v = 0;
        float v3_v = -1;

        int x;
        int y;
        int z;
        int spheresVal = 3;
        int radius;

        void GlPaint()
        {
            Shaders m = new Shaders();

            Console.WriteLine(m.glslVersion);
            Console.WriteLine(m.glVersion);

            m.InitShaders(glControl1.Width / (float)glControl1.Height);

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);


            GL.EnableVertexAttribArray(m.attribute_vpos);


            Console.WriteLine("OK");
            GL.DrawArrays(PrimitiveType.Quads, 0, 4);


            GL.DisableVertexAttribArray(m.attribute_vpos);

            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }

        void Relorad()
        {
            string str = string.Empty;
            using (System.IO.StreamReader reader = System.IO.File.OpenText("..\\..\\ray_trace_example.frag"))
            {
                str = reader.ReadToEnd();
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\ray_trace.frag"))
            {
                file.Write(str);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Relorad();
        }
       
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GlPaint();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {  
            Regex v3_r_s = new Regex("v3="+Convert.ToString(v3));//вперед    
            Regex v3_r_w = new Regex("v3=" + Convert.ToString(v3));//назад

            Regex v1_r_d = new Regex("v1=" + Convert.ToString(v1));//вправо   
            Regex v1_r_a = new Regex("v1=" + Convert.ToString(v1));//влево

            Regex v2_r_Q = new Regex("v2=" + Convert.ToString(v2));//вверх
            Regex v2_r_E = new Regex("v2=" + Convert.ToString(v2));//вниз

            Regex v2_r_i = new Regex("v2_v=" + Convert.ToString(v2_v));//смотреть вверх   
            Regex v2_r_k = new Regex("v2_v=" + Convert.ToString(v2_v));//смотреть вниз

            Regex v1_r_j = new Regex("v1_v=" + Convert.ToString(v1_v));//смотреть налево 
            Regex v1_r_l = new Regex("v1_v=" + Convert.ToString(v1_v));//смотреть направо

            Regex v3_r_p = new Regex("v3_v=" + Convert.ToString(v3_v));

            string str = string.Empty;
            using (System.IO.StreamReader reader = System.IO.File.OpenText("..\\..\\ray_trace.frag"))
            {
                str = reader.ReadToEnd();
            }
            if (e.KeyData == Keys.S)
            {
                v3 = v3 - 1;
                str = v3_r_s.Replace(str, "v3=" + Convert.ToString(v3), 3); 
            }
            if (e.KeyData == Keys.W)
            {
                v3 = v3 + 1;
                str = v3_r_w.Replace(str, "v3=" + Convert.ToString(v3), 3); 
            }
            if (e.KeyData == Keys.D)
            {
                v1 = v1 + 1;
                str = v1_r_d.Replace(str, "v1=" + Convert.ToString(v1), 3); 
            }
            if (e.KeyData == Keys.A)
            {
                v1 = v1 - 1;
                str = v1_r_d.Replace(str, "v1=" + Convert.ToString(v1), 3); 
            }
            if (e.KeyData == Keys.Q)
            {
                v2 = v2 + 1;
                str = v2_r_Q.Replace(str, "v2=" + Convert.ToString(v2), 3); 
            }
            if (e.KeyData == Keys.E)
            {
                v2 = v2 - 1;
                str = v2_r_E.Replace(str, "v2=" + Convert.ToString(v2), 3);   
            }
            if (e.KeyData == Keys.I)
            {
                v2_v = v2_v + 1;
                str = v2_r_i.Replace(str, "v2_v=" + Convert.ToString(v2_v), 3);
            }
            if (e.KeyData == Keys.K)
            {
                v2_v = v2_v - 1;
                str = v2_r_k.Replace(str, "v2_v=" + Convert.ToString(v2_v), 3);
            }
            if (e.KeyData == Keys.J)
            {
                v1_v = v1_v - 1;
                str = v1_r_j.Replace(str, "v1_v=" + Convert.ToString(v1_v), 3);
            }
            if (e.KeyData == Keys.L)
            {
                v1_v = v1_v + 1;
                str = v1_r_l.Replace(str, "v1_v=" + Convert.ToString(v1_v), 3);
            }
            if (e.KeyData == Keys.P)
            {
                v3_v = v3_v * -1;
                str = v3_r_p.Replace(str, "v3_v=" + Convert.ToString(v3_v), 3);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\ray_trace.frag"))
            {
                file.Write(str);
            }
            GlPaint();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            radius = Convert.ToInt32(textBox1.Text);
            x = Convert.ToInt32(textBox2.Text);
            y = Convert.ToInt32(textBox3.Text);
            z = Convert.ToInt32(textBox4.Text);

            ////////////////

            Regex spherVal_r = new Regex("spheresVal=" + Convert.ToString(spheresVal));//изменение количества шаров
            spheresVal++;
            string str = string.Empty;
            using (System.IO.StreamReader reader = System.IO.File.OpenText("..\\..\\ray_trace.frag"))
            {
                str = reader.ReadToEnd();
            }
            str = spherVal_r.Replace(str, "spheresVal=" + Convert.ToString(spheresVal), 3);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\ray_trace.frag"))
            {
                file.Write(str);
            }
            ///////////////

              int spehersVal_tmp = spheresVal - 2;
              Regex sperCoord_r = new Regex("int kost;");//Ошибка где-то тут
              string str_coord = string.Empty;
              using (System.IO.StreamReader reader_coord = System.IO.File.OpenText("..\\..\\ray_trace.frag"))
              {
                  str_coord = reader_coord.ReadToEnd();
              }
              str_coord = sperCoord_r.Replace(str_coord, "spheres[" + Convert.ToString(spheresVal-1) + "].Center = vec3(" + Convert.ToString(x) + "," + Convert.ToString(y) + ", " + Convert.ToString(z) + ");spheres[" + Convert.ToString(spheresVal-1) + "].Radius = " + Convert.ToString(radius) + ";spheres[" + Convert.ToString(spheresVal-1) + "].Materialidx = 0; int kost;", 3);

              using (System.IO.StreamWriter file_spehers = new System.IO.StreamWriter("..\\..\\ray_trace.frag"))
              {
                  file_spehers.Write(str_coord);
              }
              GlPaint();
         
             
        }

    }
}
