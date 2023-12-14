using System.ComponentModel.DataAnnotations;

namespace Service.Catalog.API.Application.Dtos
{
    public class ProductDto
    {
        [Display(Name = "شناسه محصول")]
        public int ProductId { get; set; }
        [Display(Name = "نام محصول")]
        public string Name { get; set; }

        [Display(Name = "قیمت محصول")]
        public double Price { get; set; }

        [Display(Name = "توضیحات محصول")]
        public string Description { get; set; }
        [Display(Name = "دسته بندی محصول")]
        public string CategoryName { get; set; }
        [Display(Name = "آدرس تصویر")]
        public string ImageURL { get; set; }
    }

    [Display(Name = "برای افزودن محصول جدید")]
    public class AddProductDto
    {
        [Display(Name = "شناسه محصول")]
        public int ProductId { get; set; }

        [Display(Name = "نام محصول")]
        public string Name { get; set; }

        [Display(Name = "قیمت محصول")]
        public double Price { get; set; }

        [Display(Name = "توضیحات محصول")]
        public string? Description { get; set; }
        [Display(Name = "دسته بندی محصول")]
        public string CategoryName { get; set; }
        [Display(Name = "آدرس تصویر")]
        public string? ImageURL { get; set; }
    }

    [Display(Name = "برای ویرایش محصول")]
    public class EditProductDto
    {
        [Display(Name = "نام محصول")]
        public string Name { get; set; }

        [Display(Name = "قیمت محصول")]
        public double Price { get; set; }

        [Display(Name = "توضیحات محصول")]
        public string? Description { get; set; }
        [Display(Name = "دسته بندی محصول")]
        public string CategoryName { get; set; }
        [Display(Name = "آدرس تصویر")]
        public string? ImageURL { get; set; }
    }
}