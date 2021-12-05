using BackEnd.DataLayer.Entities.Access;
using BackEnd.DataLayer.Entities.Blog;
using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DataLayer.Entities.Account
{
    public class User: BaseEntity
    {
        #region properties
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage ="تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }


        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string LastName { get; set; }


        [Display(Name = "آدرس")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Address { get; set; }


        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string EmailActiveCode { get; set; }


        public bool IsActivated { get; set; }
        #endregion

        #region Relations
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<BlogContent> Blogs { get; set; }

        #endregion
    }
}
