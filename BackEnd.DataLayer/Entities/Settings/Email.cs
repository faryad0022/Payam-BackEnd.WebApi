using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DataLayer.Entities.Settings
{
    public class Email: BaseEntity
    {
        #region Properties
        [Display(Name = "Default Email Address")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string DefaultEmail { get; set; }


        [Display(Name = "Email Password")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string EmailPassword { get; set; }

        [Display(Name = "SSL")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public bool EnableSsl { get; set; } = true;


        [Display(Name = " Smtp Server")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string SmtpServer { get; set; }

        [Display(Name = " From")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string FromTitle { get; set; }


        [Display(Name = " Port")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public int Port { get; set; }
        #endregion
    }
}
