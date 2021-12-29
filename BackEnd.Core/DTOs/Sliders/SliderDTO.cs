
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Core.DTOs.Sliders
{
    public class SliderDTO
    {
        #region Properties
        public long Id { get; set; }
        public bool IsDelete { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string ImageName { get; set; }

        [Display(Name = "لینک")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Link { get; set; }
        public string Base64Image { get; set; }

        #endregion

        public enum SliderResult
        {
            Success,
            ImageNotFound,
            ServerError,
            NotFound
        }
    }
}
