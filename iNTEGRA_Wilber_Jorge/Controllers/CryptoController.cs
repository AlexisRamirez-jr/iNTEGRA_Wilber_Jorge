using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using static iNTEGRA_Wilber_Jorge.DTO.APIcryptoDTO;

namespace iNTEGRA_Wilber_Jorge.Controllers
{
    public class CryptoController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            Bitcoin listadoObtener = new Bitcoin();
            using (var httpClient = new HttpClient())
            {
                using var result = await httpClient.GetAsync("https://api.coindesk.com/v1/bpi/currentprice.json");
                string apiResult = await result.Content.ReadAsStringAsync();
                listadoObtener = JsonConvert.DeserializeObject<Bitcoin>(apiResult);
            }
            return View(listadoObtener);
        }
       
    }
}
