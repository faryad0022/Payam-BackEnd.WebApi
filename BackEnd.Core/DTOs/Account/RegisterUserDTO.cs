using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Core.DTOs.Account
{
    public class RegisterUserDTO
    {
        #region properties
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        [Compare(nameof(Password), ErrorMessage ="کلمه عبور و تکرار ان یکسان نیست")]
        public string ConfirmPassword { get; set; }

        #endregion

        public enum RegisterUserResult
        {
            EmailExist,
            EmailServerError,
            Success
        }
    }
}
