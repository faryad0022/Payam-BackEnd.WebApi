﻿using Microsoft.AspNetCore.Mvc;


namespace BackEnd.Core.utilities.Common
{
    public static class JsonResponseStatus
    {
        public static JsonResult Success()
        {
            return new JsonResult(new {status = "Success" });
        }
        public static JsonResult Success(object returnData)
        {
            return new JsonResult(new { status = "Success", data=returnData });
        }

        public static JsonResult ModelError()
        {
            return new JsonResult(new { status = "ModelError" });
        }
        public static JsonResult ModelError(object returnData)
        {
            return new JsonResult(new { status = "ModelError", data = returnData });
        }

        public static JsonResult ServerError()
        {
            return new JsonResult(new { status = "ServerError" });
        }
        public static JsonResult ServerError(object returnData)
        {
            return new JsonResult(new { status = "ServerError", data = returnData });
        }


        public static JsonResult NotFound()
        {
            return new JsonResult(new { status = "NotFound" });
        }
        public static JsonResult NotFound(object returnData)
        {
            return new JsonResult(new { status = "NotFound", data = returnData });
        }

        public static JsonResult Exist()
        {
            return new JsonResult(new { status = "Exist" });
        }
        public static JsonResult Exist(object returnData)
        {
            return new JsonResult(new { status = "Exist", data = returnData });
        }

        public static JsonResult MustHaveOneItem()
        {
            return new JsonResult(new { status = "MustHaveOneItem" });
        }
        public static JsonResult MustHaveOneItem(object returnData)
        {
            return new JsonResult(new { status = "MustHaveOneItem", data = returnData });
        }

        public static JsonResult Error()
        {
            return new JsonResult(new { status = "Error" });
        }
        public static JsonResult Error(object returnData)
        {
            return new JsonResult(new { status = "Error", data = returnData });
        }

        public static JsonResult Duplicate()
        {
            return new JsonResult(new { status = "Duplicate" });
        }
        public static JsonResult Duplicate(object returnData)
        {
            return new JsonResult(new { status = "Duplicate", data = returnData });
        }

        public static JsonResult NoAccess()
        {
            return new JsonResult(new { status = "NoAccess" });
        }
        public static JsonResult NoAccess(object returnData)
        {
            return new JsonResult(new { status = "NoAccess", data = returnData });
        }

        public static JsonResult NotActive()
        {
            return new JsonResult(new { status = "NotActive" });
        }
        public static JsonResult NotActive(object returnData)
        {
            return new JsonResult(new { status = "NotActive", data = returnData });
        }

        public static JsonResult BannedAccount()
        {
            return new JsonResult(new { status = "BannedAccount" });
        }
        public static JsonResult BannedAccount(object returnData)
        {
            return new JsonResult(new { status = "BannedAccount", data = returnData });
        }
    }
}
