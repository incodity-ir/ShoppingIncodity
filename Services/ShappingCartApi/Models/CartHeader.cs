namespace ShappingCartApi.Models
{
    public class CartHeader
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }

        //navigation properties
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
