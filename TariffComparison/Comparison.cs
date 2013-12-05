using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TariffComparison
{
    public class Comparison
    {
        private readonly IQueryable<Product> _selectedProducts;

        public Comparison(IQueryable<Product> selectedProducts)
        {
            _selectedProducts = selectedProducts;
        }

        public ICollection<ComparisonResult> CompareProductsForConsumption(decimal consumptionAmount)
        {
            return _selectedProducts.Select(product=> product.Calculate)
        }
    }
}