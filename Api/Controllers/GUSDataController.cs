using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using System.Xml;

namespace Api.Controllers
{

    [Authorize]
    [Route("api/gus")]
    [ApiController]
    public class GUSDataController : ControllerBase
    {
        public static readonly string[] provinces = {
                "DOLNOŚLĄSKIE",
                "KUJAWSKO-POMORSKIE",
                "LUBELSKIE",
                "LUBUSKIE",
                "ŁÓDZKIE",
                "MAŁOPOLSKIE",
                "MAZOWIECKIE",
                "OPOLSKIE",
                "PODKARPACKIE",
                "PODLASKIE",
                "POMORSKIE",
                "ŚLĄSKIE",
                "ŚWIĘTOKRZYSKIE",
                "WARMIŃSKO-MAZURSKIE",
                "WIELKOPOLSKIE",
                "ZACHODNIOPOMORSKIE"
            };

        public IConfiguration _configuration;


        public GUSDataController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost("data")]
        public async Task<ActionResult<GUSDataResponse>> GetData([FromBody] GUSDataRequest request)
        {
            if (request != null && request.DataId != null && validProvinceName(request.ProvinceName) && request.Years != null)
            {
                string url = "https://bdl.stat.gov.pl/api/v1/data/by-variable/" + request.DataId;
                var param = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>( "format", "xml" ),
                    new KeyValuePair<string, string>("unit-level", "2" ),
                    new KeyValuePair<string, string>( "page-size", "100")
                };

                foreach (int year in request.Years)
                    param.Add(new KeyValuePair<string, string>("year", year.ToString()));

                var newUrl = new Uri(QueryHelpers.AddQueryString(url, param));


                try
                {
                using (HttpClient client = new HttpClient())
                {

                    string xmlData = await client.GetStringAsync(newUrl);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlData);

                    // values
                    XmlNodeList yearNodes = xmlDoc.SelectNodes("//unitData[name = '" + request.ProvinceName + "']//values//yearVal//year");
                    XmlNodeList valueNodes = xmlDoc.SelectNodes("//unitData[name = '" + request.ProvinceName + "']//values//yearVal//val");

                    GUSDataResponse response = new GUSDataResponse();

                    if (yearNodes.Count == 0 || yearNodes.Count != valueNodes.Count)
                        StatusCode(500, "Internal server error");

                    response.ProvinceName = request.ProvinceName;
                    response.Length = yearNodes.Count;
                    response.Values = new double[yearNodes.Count];
                    response.Years = new int[yearNodes.Count];

                    for (int i = 0; i < yearNodes.Count; i++)
                    {
                        response.Values[i] = Double.Parse(valueNodes[i].InnerText, CultureInfo.InvariantCulture);
                        response.Years[i] = Int16.Parse(yearNodes[i].InnerText);
                    }

                    return response;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500);
                }
            }
            else
            {
                if (request.DataId == null)
                    return StatusCode(400, "Data id needs to be provided");
                if (request.ProvinceName == null)
                    return StatusCode(400, "Province name needs to be provided");
                else if (request.Years == null)
                    return StatusCode(400, "Years needs to be provided");

                return StatusCode(400, "Province name is incorrect, possible names: " + string.Join(", ", provinces));
            }
        }

        private bool validProvinceName(string provinceName)
        {
            if (provinceName == null)
                return false;
            return provinces.Contains(provinceName);
        }
    }
}
