namespace TariffComparison
{
    using System;

    public class Product
    {
        #region constructors

        public Product(string name, decimal costPerUnit)
            : this(name, costPerUnit, 0)
        {
        }

        private Product(string name, decimal costPerUnit, int baseCostPerBillingPeriod)
            : this(name, costPerUnit, baseCostPerBillingPeriod, 0)
        {
        }

        private Product(string name, decimal costPerUnit, int baseCostPerBillingPeriod, int amountIncludedIntoBaseCost)
            : this(name, costPerUnit, baseCostPerBillingPeriod, amountIncludedIntoBaseCost, 12)
        {
        }

        public Product(string name, decimal costPerUnit, decimal baseCostPerBillingPeriod, decimal amountIncludedIntoBaseCost, int numberOfBillingPeriodsPerYear)
        {
            Name = name;
            CostPerUnit = costPerUnit;
            BaseCostPerBillingPeriod = baseCostPerBillingPeriod;
            AmountIncludedIntoBaseCost = amountIncludedIntoBaseCost;
            NumberOfBillingPeriodsPerYear = numberOfBillingPeriodsPerYear;
        }

        #endregion constructors

        #region fields

        public decimal AmountIncludedIntoBaseCost { get; set; }

        public decimal BaseCostPerBillingPeriod { get; set; }

        public decimal CostPerUnit { get; set; }

        public string Name { get; set; }

        public int NumberOfBillingPeriodsPerYear { get; set; }

        #endregion fields

        #region calculated fields

        public decimal BaseCostPerYear
        {
            get
            {
                return BaseCostPerBillingPeriod * NumberOfBillingPeriodsPerYear;
            }
        }

        #endregion calculated fields

        public decimal CalculateYearlyCostForConsumption(decimal consumption)
        {
            return BaseCostPerYear + CalculateAdditionalConsumptionCostPerYear(consumption);
        }

        private decimal CalculateAdditionalConsumptionCostPerYear(decimal consumption)
        {
            var billableConsumption = Math.Max(AmountIncludedIntoBaseCost - consumption, 0);
            return billableConsumption * CostPerUnit;
        }
    }
}