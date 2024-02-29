namespace Shopping.Web.Models.Dtos
{
    public class CartDetailDto
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        //navigation properties
        public CartHeaderDto CartHeader { get; set; }
        public ProductDto Product { get; set; }
    }
}
