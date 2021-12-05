using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BackEnd.Core.utilities.Common
{
    public static class PathTools
    {
        public static string GalleryServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/gallery/origin/");
        public static string LogoServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/logo/origin/");

    }
}
