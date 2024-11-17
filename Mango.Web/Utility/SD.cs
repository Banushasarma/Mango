namespace Mango.Web.Utility
{
    public class SD
    {
        //Base URL for coupon API
        public static string CouponAPIBase { get; set; }

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
