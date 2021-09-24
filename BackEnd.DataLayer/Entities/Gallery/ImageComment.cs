using BackEnd.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DataLayer.Entities.Gallery
{
    public class ImageComment: BaseEntity
    {
        #region Properties
        [Display(Name ="متن نظر")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage ="تعداد کاراکترهای {0} نمیتواند بیشتر از {1} باشد")]
        public string Comment { get; set; }
        public long ImageGalleryId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(ImageGalleryId))]
        public ImageGallery ImageGallery { get; set; }
        #endregion
    }
}
