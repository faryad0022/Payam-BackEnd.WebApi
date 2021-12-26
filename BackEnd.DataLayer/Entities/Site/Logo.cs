using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DataLayer.Entities.Site
{
    public class Logo : BaseEntity
    {
        #region Properties
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }


        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Description { get; set; }


        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string ImageName { get; set; }
        #endregion    
    }
}
