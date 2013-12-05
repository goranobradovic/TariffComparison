namespace TariffComparison.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class ComparisonTest
    {
        #region fields

        private readonly Dictionary<Product, List<ComparisonExpectation>> _testCases = new Dictionary<Product, List<ComparisonExpectation>>();

        private Product _basisElectricityTarrif;

        private Comparison _comparison;
        private Product _packagedTariff;

        private List<Product> _productsForComparison;
        #endregion fields

        #region test setup

        [TestInitialize]
        public void Setup()
        {
            SetUpProducts();
            SetUpProductsComparison();
            SetUpExpectations();
        }

        private void SetUpExpectations()
        {
            _testCases.Add(_basisElectricityTarrif, new List<ComparisonExpectation>
            {
                new ComparisonExpectation {ConsumedAmount = 3500m, ExpectedCost = 830m},
                new ComparisonExpectation {ConsumedAmount = 4500m, ExpectedCost = 1050m},
                new ComparisonExpectation {ConsumedAmount = 6000m, ExpectedCost = 1380m},
            });

            _testCases.Add(_packagedTariff, new List<ComparisonExpectation>
            {
                new ComparisonExpectation {ConsumedAmount = 3500m, ExpectedCost = 800m},
                new ComparisonExpectation {ConsumedAmount = 4500m, ExpectedCost = 950m},
                new ComparisonExpectation {ConsumedAmount = 6000m, ExpectedCost = 1400m},
            });
        }

        private void SetUpProducts()
        {
            _basisElectricityTarrif = new Product(name: "basic electricity tariff", costPerUnit: 0.22m,
                baseCostPerBillingPeriod: 5m, amountIncludedIntoBaseCost: 0, numberOfBillingPeriodsPerYear: 12);

            _packagedTariff = new Product(name: "Packaged  tariff", costPerUnit: 0.30m,
                baseCostPerBillingPeriod: 800m, amountIncludedIntoBaseCost: 4000m, numberOfBillingPeriodsPerYear: 1);

            _productsForComparison = new List<Product>
            {
                _basisElectricityTarrif,
                _packagedTariff
            };
        }

        private void SetUpProductsComparison()
        {
            _comparison = new Comparison(_productsForComparison);
        }
        #endregion test setup

        #region test methods

        [TestMethod]
        public void BasisElectricityTarrifMustSatisfyTestCases()
        {
            _testCases[_basisElectricityTarrif].ForEach(testCase => RunTestCase(_basisElectricityTarrif, testCase));
        }

        [TestMethod]
        public void ComparisonMustBeSortedAscending()
        {
            var testConsumptions = new List<decimal> { 2000m, 3500m, 4500m, 6000m, 8000m };
            testConsumptions.ForEach(consumption =>
            {
                var comparisonResults = _comparison.CompareProductsForAnnualConsumption(consumption).ToList();

                Assert.IsTrue(comparisonResults.First().AnnualCosts <= comparisonResults.Last().AnnualCosts);
            });
        }

        [TestMethod]
        public void ComparisonMustHaveSameNumberOfResultsAsNumberOfComparedProducts()
        {
            var comparisonResults = _comparison.CompareProductsForAnnualConsumption(0).ToList();

            Assert.IsNotNull(comparisonResults);
            Assert.AreEqual(_productsForComparison.Count(), comparisonResults.Count());
        }

        [TestMethod]
        public void PackagedTariffMustSatisfyTestCases()
        {
            _testCases[_packagedTariff].ForEach(testCase => RunTestCase(_packagedTariff, testCase));
        }

        [TestMethod]
        public void TariffComparisonMustGiveListWithNameAndAnnualCostForEachProduct()
        {
            Assert.AreNotEqual(_basisElectricityTarrif.Name, _packagedTariff.Name, "Tarrifs must have different names to be able to disticnt them in results!");

            const decimal consumption = 3500m,
                basisTarrifExpectedCost = 830m,
                packagedTarrifExpectedCost = 800m;

            var comparison = new Comparison(new List<Product> { _basisElectricityTarrif, _packagedTariff });
            var comparisonResults = comparison.CompareProductsForAnnualConsumption(consumption).ToList();

            Assert.AreEqual(1, comparisonResults.Count(result => result.Product == _basisElectricityTarrif));
            Assert.AreEqual(basisTarrifExpectedCost, comparisonResults.Single(result => result.ProductName == _basisElectricityTarrif.Name).AnnualCosts);

            Assert.AreEqual(1, comparisonResults.Count(result => result.Product == _packagedTariff));
            Assert.AreEqual(packagedTarrifExpectedCost, comparisonResults.Single(result => result.ProductName == _packagedTariff.Name).AnnualCosts);
        }
        #endregion test methods

        #region helpers

        private void RunTestCase(Product product, ComparisonExpectation testCase)
        {
            var actualResult = product.CalculateYearlyCostForConsumption(testCase.ConsumedAmount);
            Assert.AreEqual(testCase.ExpectedCost, actualResult,
                "For consumed amount {0:N} Product '{1}' has expected costs of {2:C} but actual result is {3:C}.",
                testCase.ConsumedAmount, product.Name, testCase.ExpectedCost, actualResult);
        }

        private class ComparisonExpectation
        {
            public decimal ConsumedAmount;
            public decimal ExpectedCost;
        }

        #endregion helpers
    }
}