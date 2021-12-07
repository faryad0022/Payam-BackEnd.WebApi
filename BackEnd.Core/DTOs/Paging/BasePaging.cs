
namespace BackEnd.Core.DTOs.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            PageId = 1;
            TakeEntity = 16;
        }
        public int PageId { get; set; }// شماره صفحه
        public int PageCount { get; set; } //تعداد صفحات
        public int ActivePage { get; set; }// در کدام صفحه است
        public int StartPage { get; set; }// نقطه شروع از کجا باشه
        public int EndPage { get; set; }// نقطه پایان
        public int TakeEntity { get; set; }// در هر صفحه چندتا نمایش بده
        public int SkipEntity { get; set; }// از چندتا رد بشه



    }
}
