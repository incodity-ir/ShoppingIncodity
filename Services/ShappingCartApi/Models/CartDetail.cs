using System.ComponentModel.DataAnnotations.Schema;

namespace ShappingCartApi.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        //navigation properties
        public CartHeader CartHeader { get; set;}
        public Product Product { get; set; }


    }
}
