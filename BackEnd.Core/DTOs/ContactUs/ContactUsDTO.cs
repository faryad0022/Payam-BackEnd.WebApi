using System;
using System.ComponentModel.DataAnnotations;


namespace BackEnd.Core.DTOs.ContactUs
{
    public class ContactUsDTO
    {


        #region Properties
        [Display(Name = "شماره")]
        public long Id { get; set; }

        [Display(Name = "وضعیت")]
        public string Status { get; set; }
        [Display(Name = "تاریخ ارسال پیام")]

        public DateTime CreateDate { get; set; }

        [Display(Name = "حذف شده")]
        public bool IsDelete { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Name { get; set; }

        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Telephone { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(60, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Email { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Description { get; set; }
        #endregion

        public enum ContactUsResult
        {
            Success,
            ServerError,
            NotFound,
            Error
        }
        public enum ContactUsStatus
        {
            None,
            NotSeen,
            Seen,
            Deleted,
            Answered
        }
    }
}
