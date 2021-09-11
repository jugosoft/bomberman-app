using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace GXA
{
    public partial class Panel : Form
    {
        public Panel()
        {
            InitializeComponent();
            host = new Host(this.timer1);
            int w = 0;
            int h = 0;
            host.GetSize(ref w, ref h);
            this.Width = w;
            this.Height = h;
            host.MapSizeChanged += new MapSizeEventHandler(host_MapSizeChanged);
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        
        private Host host;        

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            host.Draw(e.Graphics);
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void host_MapSizeChanged()
        {
            int w = 0;
            int h = 0;
            host.GetSize(ref w, ref h);
            this.Width = w;
            this.Height = h;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            host.Update();
            this.Refresh();
        }

        private void Panel_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardState.Instance.PressKey(e.KeyCode);
        }

        private void Panel_KeyUp(object sender, KeyEventArgs e)
        {
            KeyboardState.Instance.ReleaseKey(e.KeyCode);
        }
    }
}
