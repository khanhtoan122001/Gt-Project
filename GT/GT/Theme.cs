using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GT
{
    public class Theme
    {
        private Color bgpic, text, nest, tail, bg1, bg2, bg3;
        public Theme()
        {
            bgpic = Color.FromArgb(255, 255, 255);
            text = Color.FromArgb(0, 0, 0);
            nest = Color.FromArgb(150, 150, 150);
            tail = Color.FromArgb(200, 200, 200);
            bg1 = Color.FromArgb(64, 64, 64);
            bg2 = Color.FromArgb(255, 255, 255);
            bg3 = Color.FromArgb(255, 255, 255);

        }
        private Color Change(Color a)
        {
            int r = a.R, g = a.G, b = a.B;
            a = Color.FromArgb(255 - r, 255 - g, 255 - b);
            return a;
        }
        public Color TextColor
        {
            get
            {
                return text;
            }
        }
        public void ChangeAll()
        {
            bgpic = Change(bgpic);
            text = Change(text);
            nest = Change(nest);
            tail = Change(tail);
            bg1 = Change(bg1);
            bg2 = Change(bg2);
            bg3 = Change(bg3);
        }
        public Color BackGroundPic
        {
            get
            {
                return bgpic;
            }
        }
        public Color Bg2
        {
            get => bg2;
            set => bg2 = value;
        }
        public Color Nest
        {
            get
            {
                return nest;
            }
        }
        public Color Tail
        {
            get
            {
                return tail;
            }
        }
        public Color Bg3
        {
            set => bg3 = value;
            get => bg3;
        }
        public Color Bg1
        {
            get
            {
                return bg1;
            }
        }
    }
}
