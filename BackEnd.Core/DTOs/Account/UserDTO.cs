using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Core.DTOs.Account
{
    public class UserDTO
    {
        #region properties

        public long Id { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Email { get; set; }


        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string LastName { get; set; }


        [Display(Name = "آدرس")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Address { get; set; }


        public string EmailActiveCode { get; set; }
        public string Token { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDelete { get; set; }
        public int ExpireTime { get; set; }


        #endregion
    }
}
