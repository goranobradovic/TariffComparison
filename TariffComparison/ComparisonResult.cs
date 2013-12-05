namespace TariffComparison
{
    public class ComparisonResult
    {
        public ComparisonResult(Product product, decimal annualCosts)
        {
            Product = product;
            AnnualCosts = annualCosts;
        }

        public Product Product { get; set; }

        public string ProductName { get { return Product.Name; } }

        public decimal AnnualCosts { get; set; }
    }
}