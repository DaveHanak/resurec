using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace resurec.ViewModels.LvcSpecific
{
    public class ColorPalette
    {
        //TODO register this as a service or something
        public SKColor white = new(250, 250, 250);
        public SKColor gray_lightest = new(225, 225, 225);
        public SKColor gray_lighter = new(200, 200, 200);
        public SKColor gray_light = new(150, 150, 150);
        public SKColor gray = new(125, 125, 125);
        public SKColor gray_dark = new(100, 100, 100);
        public SKColor gray_darker = new(75, 75, 75);
        public SKColor gray_darkest = new(50, 50, 50);
        public SKColor black = new(0, 0, 0);
    }
}
