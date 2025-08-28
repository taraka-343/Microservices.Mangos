namespace mango.webPortal.Utilities
{
    public class DT
    {
        public static string coupanApiBaseUrl { get; set; }
        public static string AuthApiBaseUrl { get; set; }
        public static string productApiBaseUrl { get; set; }
        public static string shoppingCartApiBaseUrl { get; set; }


        public const string roleAdmin = "ADMIN";
        public const string roleCustomer = "CUSTOMER";
        public const string tokenCookie = "JWTToken";

        public enum apiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

    }
}
