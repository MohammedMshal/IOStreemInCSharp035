using System.Net.Http;

namespace IDisposapleDesignPattren
{
    class Program
    {
        static void Main(string[] args)
        {
            //CurrencyServic? currencyServic = null;
            //try
            //{
            //    currencyServic = new();
            //    string result = currencyServic.GetCurrencies();
            //    Console.WriteLine(result);
            //    currencyServic.Dispose();
            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine(e.Message);
            //}
            //finally
            //{
            //    currencyServic?.Dispose();
            //}

            // using recommended
            using (CurrencyServic currencyServic = new())
            {
                string result = currencyServic.GetCurrencies();
                Console.WriteLine(result);
            }
            
        }
    }

    class CurrencyServic: IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed = false;
        public CurrencyServic()
        {
            _httpClient = new HttpClient();
        }

        ~CurrencyServic()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            // Dispos logic
            if(disposing)
            {
                // dispos manged resouced
                _httpClient.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string GetCurrencies()
        {
            string url = "https://coinbase.com/api/v2/currencies";
            var result = _httpClient.GetStringAsync(url).Result;
            return result;
        }
    }
}