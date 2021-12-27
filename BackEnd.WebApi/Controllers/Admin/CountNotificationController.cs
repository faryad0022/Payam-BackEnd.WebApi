using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.WebApi.Controllers.Site;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Admin
{

    public class CountNotificationController : PanelBaseController
    {
        #region Constructor
        private readonly ICountNotificationService countNotificationService;
        public CountNotificationController(ICountNotificationService countNotificationService)
        {
            this.countNotificationService = countNotificationService;
        }
        #endregion

        #region Get Count
        [HttpGet("get-count-notification")]
        public async Task<IActionResult> GetCount()
        {
            var count = await countNotificationService.getCountNotifications();
            return JsonResponseStatus.Success(count);
        }
        #endregion
    }
}
