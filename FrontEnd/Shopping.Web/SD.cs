namespace Shopping.Web
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartApiBase { get; set; }
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}