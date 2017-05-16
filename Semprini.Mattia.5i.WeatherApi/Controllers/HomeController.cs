using Newtonsoft.Json;
using Semprini.Mattia._5i.WeatherApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Semprini.Mattia._5i.WeatherApi.Models.JsonMeteo;

namespace Semprini.Mattia._5i.WeatherApi.Controllers      // Beta!! la lista non passa i parametri :(
{

    public class HomeController : Controller
    {
        public List<Jsonmeteo> metei = new List<Jsonmeteo>();
        Jsonmeteo Meteo;
        HttpCookie cookie = new HttpCookie("mybigcookie");
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Cerca(Jsonmeteo u)
        {
            try
            {

                HttpClient client = new HttpClient();

                string result = await client.GetStringAsync(
                   new Uri(@"http://api.wunderground.com/api/ff9622a1a7822d3a/conditions/q/IT/" + u.City + ".json"));
                Meteo = JsonConvert.DeserializeObject<Jsonmeteo>(result);
                metei.Add(Meteo);
                TempData["model"] = metei;
                return View("ViewMeteo", Meteo);
            }
            catch
            {
                return View("Error");
            }
        }



        
        public async Task<ActionResult> Aggiorna(string textbox)
        {
            try
            {

                HttpClient client1 = new HttpClient();
                string result1 = await client1.GetStringAsync(
                   new Uri(@"http://api.wunderground.com/api/ff9622a1a7822d3a/conditions/q/IT/" + textbox + ".json"));
                Meteo = JsonConvert.DeserializeObject<Jsonmeteo>(result1);
                metei = (List<Jsonmeteo>)TempData["model"];
                int i = 0;
                foreach (var x in metei)
                {

                    HttpClient client = new HttpClient();
                    string result = await client.GetStringAsync(
                       new Uri(@"http://api.wunderground.com/api/ff9622a1a7822d3a/conditions/q/IT/" + x.current_observation.display_location.city + ".json"));
                    Meteo = JsonConvert.DeserializeObject<Jsonmeteo>(result);
                    metei.Add(Meteo);
                    
                    i++;
                    
                }
                TempData["model"] = metei;

                return View("Viewmeteo2", metei);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update()
        {
            try
            {
                metei = (List<Jsonmeteo>)TempData["model"];
                int i = 0;
                foreach (var x in metei)
                {
                    HttpClient client = new HttpClient();
                    string result = await client.GetStringAsync(
                       new Uri(@"http://api.wunderground.com/api/ff9622a1a7822d3a/conditions/q/IT/" + x.ToString() + ".json"));
                    Meteo = JsonConvert.DeserializeObject<Jsonmeteo>(result);
                    
                    i++;
                }
                return View("Viewmeteo2", metei);
            }
            catch
            {
                return View("Error");
            }
        }

    public ActionResult About()
    {
        ViewBag.Message = "Condizioni del meteo";

        return View();
    }

    public ActionResult Contact()
    {
        ViewBag.Message = "Contatti";

        return View();
    }
}
}
