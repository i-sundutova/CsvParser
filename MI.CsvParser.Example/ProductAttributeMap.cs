namespace MI.CsvParser.Example
{
    public class ProductAttributeMap : CsvClassMap<ProductAttribute>
    {
        public ProductAttributeMap()
        {
            Map(x => x.ProductId);
            Map(x => x.Category);
            Map(x => x.Name);
            Map(x => x.Price);
        }
    }
}