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
        private Color bgpic, text, nest, tail, bg;
        public Theme()
        {
            bgpic = Color.FromArgb(255, 255, 255);
            text = Color.FromArgb(0, 0, 0);
            nest = Color.FromArgb(150, 150, 150);
            tail = Color.FromArgb(200, 200, 200);
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
        }
        public Color BackGroundPic
        {
            get
            {
                return bgpic;
            }
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
    }
}
