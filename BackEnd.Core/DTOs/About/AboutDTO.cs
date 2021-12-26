using System.ComponentModel.DataAnnotations;

namespace BackEnd.Core.DTOs.About
{
    public class AboutDTO
    {
        #region Properties
        public long Id { get; set; }

        [Display(Name = " متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string Text { get; set; }

        [Display(Name = " تعداد بازدید")]
        public long ViewCount { get; set; } = 0;

        public enum AboutResult
        {
            SuccessFull,
            NotFound,
            ServerError,
            Exist
        }

        #endregion
    }
}
