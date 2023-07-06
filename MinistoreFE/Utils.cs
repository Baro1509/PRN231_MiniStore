using MinistoreFE.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MinistoreFE {
    public static class Utils {
        public static ByteArrayContent ConvertForPost<T>(T test) {
            var myContent = JsonConvert.SerializeObject(test);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }

        public static void SetObjectInSession(this ISession session, string key, object value) {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetCustomObjectFromSession<T>(this ISession session, string key) {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static Cart CreateCart(string id) {
            return new Cart { CartItems = new List<CartItem>(), StaffId = id };
        }

        public static bool isManager(string role) {
            return role.Equals("MG");
        }
        
        public static bool isSalesman(string role) {
            return role.Equals("SA");
        }
        
        public static bool isLogin(string Id, string Role, string Token) {
            return Id != null && Role != null && Token != null;
        }
    }
}
