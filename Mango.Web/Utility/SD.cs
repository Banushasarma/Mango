namespace Mango.Web.Utility
{
    public class SD
    {
        //Base URL for Service APIs
        public static string AuthAPIBase { get; set; }
        public static string CouponAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }


        //Add static role
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

        //Set cookie name for token
        public const string TokenCookie = "JWTToken";



        //Add Enum for ApiTypes
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
            OPTIONS,
            PATCH,
        }
    }
}
