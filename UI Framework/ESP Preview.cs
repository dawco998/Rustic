using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace covet.cc.UI_Framework
{
    public partial class ESP_Preview : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public ESP_Preview()
        {
            InitializeComponent();
        }

        private void ESP_Preview_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Cheat.Settings.Visuals.InsideBoxColor;

            panel2.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;
            panel3.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;
            panel4.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;
            panel5.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;
            panel6.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;
            panel7.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;
            panel8.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;
            panel9.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;
            panel10.BackColor = Cheat.Settings.Visuals.OutsideBoxColor;


            panel1.Visible = Cheat.Settings.Visuals.Box;
            panel2.Visible = Cheat.Settings.Visuals.Box;
            panel3.Visible = Cheat.Settings.Visuals.Box;
            panel4.Visible = Cheat.Settings.Visuals.Box;
            panel5.Visible = Cheat.Settings.Visuals.Box;
            panel6.Visible = Cheat.Settings.Visuals.Box;
            panel7.Visible = Cheat.Settings.Visuals.Box;
            panel8.Visible = Cheat.Settings.Visuals.Box;
            panel9.Visible = Cheat.Settings.Visuals.Box;
            panel10.Visible = Cheat.Settings.Visuals.Box;

            label2.Visible = Cheat.Settings.Visuals.Distance;

            panel11.Visible = Cheat.Settings.Visuals.Healthbar;
            panel12.Visible = Cheat.Settings.Visuals.Healthbar;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ESP_Preview_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
