﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.utilities.Extensions.FileExtensions
{
    public static class ImageUploaderExtensions
    {
        public static void AddImageToServer(this Image image, string fileName, string originalPath, string deleteFileName = null)
        {
            if (image != null)
            {
                if (!Directory.Exists(originalPath)) Directory.CreateDirectory(originalPath);
                if (!string.IsNullOrEmpty(deleteFileName)) File.Delete(originalPath + deleteFileName);

                string imageName = originalPath + fileName;
                using (var stream = new FileStream(imageName, FileMode.Create))
                {
                    if (!Directory.Exists(imageName)) image.Save(stream, ImageFormat.Jpeg);
                }
            }
        }

        public static void DeleteImageFromServer(string fileName, string originalPath)
        {

            File.Delete(originalPath + fileName);

        }

        public static byte[] DecodeUrlBase64(string s)
        {
            return Convert.FromBase64String(s.Substring(s.LastIndexOf(',') + 1));
        }

        public static Image Base64ToImage(string base64String)
        {
            var res = DecodeUrlBase64(base64String);
            MemoryStream ms = new MemoryStream(res, 0, res.Length);
            ms.Write(res, 0, res.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }



        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }
}
