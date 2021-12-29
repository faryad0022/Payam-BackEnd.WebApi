using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BackEnd.Core.utilities.Common
{
    public static class PathTools
    {
        public static string GalleryServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/gallery/origin/");
        public static string BlogImageServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog/origin/");
        public static string BlogContentImageServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blogcontent/origin/");
        public static string AboutImageServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about/origin/");
        public static string SliderServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/slider/origin/");

        public static string LogoServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/logo/origin/");

    }
}
