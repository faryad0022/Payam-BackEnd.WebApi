using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.ViewModels.MainPage
{
    public class VmReturnMainPageData
    {
        public List<VmReturnAddressMainPage> Addresses { get; set; }
        public List<VmReturnSocialMainPage> Socials { get; set; }
        public List<VmReturnSliders> Sliders { get; set; }
        public List<VmReturnLatestlog> LatestBlogs { get; set; }
    }
}
