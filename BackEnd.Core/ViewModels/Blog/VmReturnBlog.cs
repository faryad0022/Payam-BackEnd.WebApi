

namespace BackEnd.Core.ViewModels.Blog
{
    public class VmReturnBlog
    {


        public long Id { get; set; }
        public string UserName { get; set; }
        public string BlogGroupName { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string ImageName { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public bool IsDelete { get; set; }
        public long ViewCount { get; set; } = 0;
        public string Base64Image { get; set; }



        public long BlogGroupId { get; set; }
        public long UserId { get; set; }




    }
}
