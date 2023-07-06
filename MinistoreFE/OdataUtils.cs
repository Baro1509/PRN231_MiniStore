using Simple.OData.Client;

namespace MinistoreFE {
    public static class OdataUtils {
        private static ODataClientSettings _settings = new ODataClientSettings(new Uri("https://localhost:7036/odata/"));
        public static ODataClient GetODataClient(string token) {
            var settings = new ODataClientSettings(new Uri("https://localhost:7036/odata/"));
            settings.BeforeRequest += delegate (HttpRequestMessage message) {
                message.Headers.Add("Authorization", "Bearer " + token);
            };
            return new ODataClient(settings);
        }
        
        public static ODataClient GetODataClient() {
            var settings = _settings;
            return new ODataClient(settings);
        }
    }
}
