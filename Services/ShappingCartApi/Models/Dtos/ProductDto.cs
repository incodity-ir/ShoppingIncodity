namespace ShappingCartApi.Models.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }
        public virtual int ProductId { get; set; }
        public virtual string Name { get; set; }
        public virtual double Price { get; set; }
        public virtual string Description { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual string ImageURL { get; set; }
        public int Count { get; set; } 
    }
}
