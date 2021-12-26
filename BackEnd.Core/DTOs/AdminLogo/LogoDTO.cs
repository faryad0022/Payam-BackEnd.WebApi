﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Core.DTOs.Logo
{
    public class LogoDTO
    {
        public long Id { get; set; }

        public bool IsDelete { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }


        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Description { get; set; }


        public string ImageName { get; set; }


        public string Base64Image { get; set; }
        public enum LogoResult
        {
            NotFount,
            ActiveLogoNotFound,
            ServerError,
            Successfull,
            MustHaveOneActiveLogo
        }
    }


}
