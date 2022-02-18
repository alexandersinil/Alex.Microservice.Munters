using Alex.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Alex.MVC.Controllers
{
    public class GiphyController : Controller
    {
        readonly HttpClient _client;
        public GiphyController()
        {
            _client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["endpoint"]) };

        }
        // GET: Giphy
        [HttpGet]
        public ActionResult Trending()
        {
            string responseBody = _client.GetStringAsync("/api/giphy/trending").Result;

            IList<GiphyModel> model = new List<GiphyModel>();
            foreach (string u in responseBody.Split(','))
            {
                model.Add(new GiphyModel
                {
                    Url = u.Replace("[", string.Empty).Replace("\"", string.Empty).Replace("]", string.Empty)
                });
            }
            return View("GiphyView", model);
        }       
        [HttpGet]
        public ActionResult Search(string criteria)
        {
            string responseBody = _client.GetStringAsync("/api/giphy/search?criteria=" + criteria).Result;
           
            IList<GiphyModel> model = new List<GiphyModel>();
            foreach (string u in responseBody.Split(','))
            {
                model.Add(new GiphyModel
                {
                    Url = u.Replace("[", string.Empty).Replace("\"", string.Empty).Replace("]", string.Empty)
                });
            }
            return View("GiphyView", model);
        }
    }
}