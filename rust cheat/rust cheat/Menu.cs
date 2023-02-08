using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace covet.cc
{
    public partial class Menu : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Menu()
        {
            InitializeComponent();


            Cheat.EntityUpdater.EntityUpdater.InitializeGlobals();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                new Cheat.Visuals.ESP().Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Cheat.Aimbot.Aimbot.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Cheat.Misc.Misc.MiscThread();
            }).Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (Cheat.Settings.Visuals.Box)
            {
                button5.BackColor = Color.FromArgb(40, 40, 40);

            }
            else
            {
                button5.BackColor = Color.FromArgb(154, 197, 39);
            }
            Cheat.Settings.Visuals.Box = !Cheat.Settings.Visuals.Box;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Region = new Region(new RectangleF(tabPage1.Left, tabPage1.Top, tabPage1.Width, tabPage1.Height));

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (Cheat.Settings.Visuals.Snaplines)
            {
                button6.BackColor = Color.FromArgb(40, 40, 40);

            }
            else
            {
                button6.BackColor = Color.FromArgb(154, 197, 39);
            }
            Cheat.Settings.Visuals.Snaplines = !Cheat.Settings.Visuals.Snaplines;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Cheat.Settings.Visuals.Healthbar)
            {
                button7.BackColor = Color.FromArgb(40, 40, 40);

            }
            else
            {
                button7.BackColor = Color.FromArgb(154, 197, 39);
            }
            Cheat.Settings.Visuals.Healthbar = !Cheat.Settings.Visuals.Healthbar;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Cheat.Settings.Visuals.Crosshair)
            {
                button8.BackColor = Color.FromArgb(40, 40, 40);

            }
            else
            {
                button8.BackColor = Color.FromArgb(154, 197, 39);
            }
            Cheat.Settings.Visuals.Crosshair = !Cheat.Settings.Visuals.Crosshair;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSlider1_ValueChanged(object sender, EventArgs e)
        {
            FOVCount.Text = bunifuSlider1.Value.ToString();
            Cheat.Settings.Aimbot.FOV = bunifuSlider1.Value;

        }

        private void bunifuSlider2_ValueChanged(object sender, EventArgs e)
        {
            if(bunifuSlider2.Value == 0)
            {
                bunifuSlider2.Value = 1;

            }
            SmoothnessCount.Text = bunifuSlider2.Value.ToString();
            Cheat.Settings.Aimbot.Smoothness = bunifuSlider2.Value;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.GetItemText(comboBox2.SelectedItem) == "Top")
            {
                Cheat.Settings.Visuals.SnaplineStyle = Cheat.Settings.Visuals.SnaplineType.Top;
            }
            if (comboBox2.GetItemText(comboBox2.SelectedItem) == "Center")
            {
                Cheat.Settings.Visuals.SnaplineStyle = Cheat.Settings.Visuals.SnaplineType.Center;
            }
            if (comboBox2.GetItemText(comboBox2.SelectedItem) == "Bottom")
            {
                Cheat.Settings.Visuals.SnaplineStyle = Cheat.Settings.Visuals.SnaplineType.Bottom;
            }
        }

        private void bunifuSlider3_ValueChanged(object sender, EventArgs e)
        {
            if (bunifuSlider3.Value == 0)
            {
                bunifuSlider3.Value = 1;

            }
            label12.Text = bunifuSlider3.Value.ToString();
            Cheat.Settings.Visuals.MaxDistance = bunifuSlider3.Value;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.GetItemText(comboBox3.SelectedItem) == "Dot")
            {
                Cheat.Settings.Visuals.CrosshairStyle = Cheat.Settings.Visuals.CrosshairType.Dot;
            }
            if (comboBox3.GetItemText(comboBox3.SelectedItem) == "Plus")
            {
                Cheat.Settings.Visuals.CrosshairStyle = Cheat.Settings.Visuals.CrosshairType.Plus;
            }
            if (comboBox3.GetItemText(comboBox3.SelectedItem) == "Cross")
            {
                Cheat.Settings.Visuals.CrosshairStyle = Cheat.Settings.Visuals.CrosshairType.Cross;
            }
        }

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new UI_Framework.ESP_Preview().Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Cheat.Settings.Visuals.Distance)
            {
                button11.BackColor = Color.FromArgb(40, 40, 40);

            }
            else
            {
                button11.BackColor = Color.FromArgb(154, 197, 39);
            }
            Cheat.Settings.Visuals.Distance = !Cheat.Settings.Visuals.Distance;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();

            panel4.BackColor = dialog.Color;
            Cheat.Settings.Visuals.InsideBoxColor = dialog.Color;
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();

            panel5.BackColor = dialog.Color;
            Cheat.Settings.Visuals.OutsideBoxColor = dialog.Color;
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();

            panel6.BackColor = dialog.Color;
            Cheat.Settings.Visuals.SnaplineColor = dialog.Color;
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();

            panel7.BackColor = dialog.Color;
            Cheat.Settings.Visuals.TextColor = dialog.Color;
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();

            panel8.BackColor = dialog.Color;
            Cheat.Settings.Visuals.CrosshairColor = dialog.Color;
        }
    }
}
