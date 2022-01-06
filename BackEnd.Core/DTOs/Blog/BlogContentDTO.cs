using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Core.DTOs.Blog
{
    public class BlogContentDTO
    {
        #region Properties
       // [Required]
        public long Id { get; set; }
        public bool IsDelete { get; set; }

        [Display(Name = "نویسنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; }

        [Display(Name = "نام گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string BlogGroupName { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Tags { get; set; }

        [Display(Name = "نام فایل عکس بلاگ")]
        public string ImageName { get; set; }

        [Display(Name = " تصویر بلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        //[MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Base64Image { get; set; }


        [Display(Name = " متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string Text { get; set; }


        [Display(Name = " وضعیت")]
        public bool Status { get; set; } = false;

        [Display(Name = " پیشنهادی")]
        public bool IsSelected { get; set; } = false;

        [Display(Name = " تعداد بازدید")]
        public long ViewCount { get; set; } = 0;

        #endregion

        #region Foreign Key
        [Display(Name = " شناسه گروه بلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public long BlogGroupId { get; set; }

        [Display(Name = " شناسه کاربر")]
     //   [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public long UserId { get; set; }
        #endregion
    }
}
