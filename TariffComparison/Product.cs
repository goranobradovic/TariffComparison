namespace TariffComparison
{
    using System;

    public class Product
    {
        public Product(string name, decimal costPerUnit, decimal baseCostPerBillingPeriod, decimal amountIncludedIntoBaseCost, int numberOfBillingPeriodsPerYear)
        {
            Name = name;
            CostPerUnit = costPerUnit;
            BaseCostPerBillingPeriod = baseCostPerBillingPeriod;
            AmountIncludedIntoBaseCost = amountIncludedIntoBaseCost;
            NumberOfBillingPeriodsPerYear = numberOfBillingPeriodsPerYear;
        }

        #region fields

        public decimal AmountIncludedIntoBaseCost { get; set; }

        public decimal BaseCostPerBillingPeriod { get; set; }

        public decimal CostPerUnit { get; set; }

        public string Name { get; set; }

        public int NumberOfBillingPeriodsPerYear { get; set; }

        #endregion fields

        #region cost calculation methods

        public decimal CalculateYearlyCostForConsumption(decimal consumption)
        {
            return GetBaseCostPerYear() + CalculateAdditionalConsumptionCostPerYear(consumption);
        }

        private decimal GetBaseCostPerYear()
        {
            return BaseCostPerBillingPeriod * NumberOfBillingPeriodsPerYear;
        }

        private decimal CalculateAdditionalConsumptionCostPerYear(decimal consumption)
        {
            var billableConsumption = Math.Max(consumption - AmountIncludedIntoBaseCost, 0);
            return billableConsumption * CostPerUnit;
        }

        #endregion cost calculation methods
    }
}