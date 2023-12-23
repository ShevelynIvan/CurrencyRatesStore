using CurrencyRatesDAL.Models;
using Newtonsoft.Json;
using CurrencyRatesDAL.EF;

namespace CurrencyRatesStore
{
    internal class Program
    {
        private static readonly string _uri = "https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5"; 

        static async Task Main(string[] args)
        {
            string response = await HttpGetCurrencies();

            IEnumerable<Currency>? currencies = JsonDeserializeToCurrencies(response);

            if (currencies is not null)
            {
                SaveCurrenciesToDB(currencies);
            }
        }

        /// <summary>
        /// Sends http get request to appropriate URI
        /// </summary>
        /// <returns>Response content as string</returns>
        static async Task<string> HttpGetCurrencies()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_uri);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Deserializes Json to IEnumarable<Currency>
        /// </summary>
        /// <param name="json">Json data of currencies</param>
        /// <returns>Deserialized data</returns>
        static IEnumerable<Currency>? JsonDeserializeToCurrencies(string json)
        {
            if(json is null)
                return null;

            return JsonConvert.DeserializeObject<IEnumerable<Currency>>(json);
        }

        /// <summary>
        /// Saves all currencies from IEnumarable<Currency> to DB
        /// </summary>
        /// <param name="currencies"></param>
        static void SaveCurrenciesToDB(IEnumerable<Currency> currencies)
        {
            using (CurrencyDbContext context = new CurrencyDbContext())
            {
                foreach (Currency ccy in currencies)
                {
                    context.Currencies.Add(ccy);
                    context.SaveChanges();
                }
            }
        }
    }
}