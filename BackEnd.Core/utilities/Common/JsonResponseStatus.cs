using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

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

        public static JsonResult Error()
        {
            return new JsonResult(new { status = "Error" });
        }
        public static JsonResult Error(object returnData)
        {
            return new JsonResult(new { status = "Error", data = returnData });
        }
    }
}
