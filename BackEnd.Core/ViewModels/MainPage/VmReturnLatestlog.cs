using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.ViewModels.MainPage
{
    public class VmReturnLatestlog
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string ImageName { get; set; }
        public string Text { get; set; }
        public long ViewCount { get; set; } = 0;
        public long BlogGroupId { get; set; }
        public string BlogGroupName { get; set; }
    }
}
