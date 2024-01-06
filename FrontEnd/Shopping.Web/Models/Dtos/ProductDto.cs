namespace Shopping.Web;

public class ProductDto
{
    public virtual int ProductId { get; set; }
    public virtual string Name { get; set; }
    public virtual double Price { get; set; }
    public virtual string Description { get; set; }
    public virtual string CategoryName { get; set; }
    public virtual string ImageURL { get; set; }
}

//[Display(Name="برای اضافه کردن محصول")]
// Add new product
public class AddProductDto
{
    [Required]
    [Display(Name = "نام محصول")]
    public virtual string Name { get; set; }
    [Required]
    [Display(Name = "قیمت محصول")]
    public virtual double Price { get; set; }
    [Required]
    [Display(Name = "توضیحات محصول")]
    public virtual string Description { get; set; }
    [Required]
    [Display(Name = "نام آرشیو محصول")]
    public virtual string CategoryName { get; set; }
    [Required]
    [Display(Name = "تصویر محصول")]
    public virtual string ImageURL { get; set; }
}

//Edit product
public class EditProductDto
{
    [Required]
    [Display(Name = "شناسه محصول")]
    public virtual int ProductId { get; set; }

    [Required]
    [Display(Name = "نام محصول")]
    public virtual string Name { get; set; }

    [Required]
    [Display(Name = "قیمت محصول")]
    public virtual double Price { get; set; }

    [Display(Name = "توضیحات محصول")]
    public virtual string? Description { get; set; }

    [Required]
    [Display(Name = "نام آرشیو محصول")]
    public virtual string CategoryName { get; set; }

    [Required]
    [Display(Name = "تصویر محصول")]
    public virtual string ImageURL { get; set; }
}
