﻿using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DataLayer.Entities.Access
{
    public class Role: BaseEntity
    {
        #region Properties
        [Display(Name ="نام سیستمی")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage ="تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Name { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        #endregion


        #region Relations
        public ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
