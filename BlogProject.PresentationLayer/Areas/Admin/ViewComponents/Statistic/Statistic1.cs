using BlogProject.BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using BlogProject.PresentationLayer.Areas.Admin.Models;

namespace BlogProject.PresentationLayer.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        private readonly IBlogService _blogService;
        private readonly IContactService _contactService;
        private readonly ICommentService _commentService;

        public Statistic1(IBlogService blogService, IContactService contactService, ICommentService commentService)
        {
            _blogService = blogService;
            _contactService = contactService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.BlogCount = _blogService.GetAllBL().Count;
            ViewBag.ContactCount = _contactService.GetAllBL().Count;
            ViewBag.CommentCount = _commentService.GetAllBL().Count;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://weather-api138.p.rapidapi.com/weather?city_name=Istanbul"),
                Headers =
    {
        { "x-rapidapi-key", "e71177d867mshfa37763bf5e2e10p1b4212jsn34b1dc08d068" },
        { "x-rapidapi-host", "weather-api138.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var weather = JsonSerializer.Deserialize<WeatherResponse>(body);
                if(weather != null)
                {
                    var tempCelsius = weather.main.temp - 273.15;
                    ViewBag.Weather = Math.Round(tempCelsius, 1);
                }
                return View();
            }
        }

    }

}
