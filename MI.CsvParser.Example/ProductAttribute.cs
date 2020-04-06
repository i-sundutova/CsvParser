using System.ComponentModel.DataAnnotations;

namespace MI.CsvParser.Example
{
    public class ProductAttribute
    {
        public int? ProductId { get; set; }

        public ProductCategory? Category { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }

    public enum ProductCategory
    {
        [Display(Name = "Sport Nutrition")]
        SportNutrition,

        [Display(Name = "Kids Nutrition")]
        KidsNutrition,

        Vitamins
    }
}