using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Common;
using BackEnd.Core.ViewModels.MainPage;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.WebApi.Controllers.Site
{
    public class MainPageSettingController : SiteBaseController
    {
        private readonly IAddressService addressService;
        private readonly ISocialService socialService;
        private readonly ISliderService sliderService;
        private readonly IBlogContentService blogContentService;


        public MainPageSettingController(
            IAddressService addressService,
            ISliderService sliderService,
            IBlogContentService blogContentService,
            ISocialService socialService)
        {
            this.blogContentService = blogContentService;
            this.sliderService = sliderService;
            this.socialService = socialService;
            this.addressService = addressService;
        }


        [HttpGet("get-details")]
        public async Task<IActionResult> GetDetails()
        {
            var addresses = await addressService.GetAllActiveAddressAsync();
            var socials = await socialService.GetAllActiveSocialsAsync();
            var sliders = await sliderService.GetAllActiveSliders();
            var blogs = await blogContentService.GetLatestBlogs();



            var blogDTO = new List<VmReturnLatestlog>();
            foreach (var blog in blogs)
            {
                var vm4 = new VmReturnLatestlog
                {
                    Title = blog.Title,
                    ImageName = blog.ImageName,
                    Id = blog.Id,
                    BlogGroupId = blog.BlogGroupId,
                    BlogGroupName = blog.BlogGroupName,
                    Tags = blog.Tags,
                    Text = blog.Text,
                    ViewCount = blog.ViewCount
                };
                blogDTO.Add(vm4);
            }


            var slidersDTO = new List<VmReturnSliders>();
            foreach (var slider in sliders)
            {
                var vm3 = new VmReturnSliders
                {
                    Description = slider.Description,
                    ImageName = slider.ImageName,
                    Title = slider.Title
                };
                slidersDTO.Add(vm3);
            }


            var addressesDTO = new List<VmReturnAddressMainPage>();
            foreach (var address in addresses)
            {
                var vm1 = new VmReturnAddressMainPage
                {
                    Address = address.Address,
                    CellPhone = address.CellPhone,
                    City = address.City,
                    Telephone = address.Telephone,
                    WorkHour = address.WorkHour
                };
                addressesDTO.Add(vm1);
            }


            var socialDTO = new List<VmReturnSocialMainPage>();
            foreach (var social in socials)
            {
                var vm2 = new VmReturnSocialMainPage
                {
                    Icon = social.Icon,
                    Link = social.Link,
                    Name = social.Name
                };
                socialDTO.Add(vm2);
            }

            var returnValues = new VmReturnMainPageData
            {
                Addresses = addressesDTO,
                Socials = socialDTO,
                LatestBlogs = blogDTO,
                Sliders = slidersDTO
                

            };

            return JsonResponseStatus.Success(returnValues);
        }
    }
}
