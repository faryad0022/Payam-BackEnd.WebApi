using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DataLayer.Entities.Site
{
    public class About: BaseEntity
    {
        #region Properties
        [Display(Name = " متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string Text { get; set; }


        [Display(Name = " تعداد بازدید")]
        public long ViewCount { get; set; } = 0;

        #endregion
    }
}
