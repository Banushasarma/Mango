﻿namespace Mango.Web.Utility
{
    public class SD
    {
        //Base URL for Auth API
        public static string AuthAPIBase { get; set; }

        //Base URL for coupon API
        public static string CouponAPIBase { get; set; }

        //Base URL for Product API
        public static string ProductAPIBase { get; set; }


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
