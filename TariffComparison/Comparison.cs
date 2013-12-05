namespace TariffComparison
{
    using System.Collections.Generic;
    using System.Linq;

    public class Comparison
    {
        private readonly IEnumerable<Product> _selectedProducts;

        public Comparison(IEnumerable<Product> selectedProducts)
        {
            _selectedProducts = selectedProducts;
        }

        public IEnumerable<ComparisonResult> CompareProductsForAnnualConsumption(decimal consumption)
        {
            return (from product in _selectedProducts
                    select new ComparisonResult(product, product.CalculateYearlyCostForConsumption(consumption)))
                .OrderBy(comparisonResult => comparisonResult.AnnualCosts);
        }
    }
}