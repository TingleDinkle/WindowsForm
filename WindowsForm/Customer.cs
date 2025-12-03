using System;
using System.Collections.Generic;

namespace WindowsForm
{
    // Abstract base class representing a generic Customer
    public abstract class Customer
    {
        // Encapsulated state
        public string Name { get; private set; }
        public int LastMonthReading { get; private set; }
        public int ThisMonthReading { get; private set; }

        protected Customer(string name, int lastMonth, int thisMonth)
        {
            ValidateReadings(lastMonth, thisMonth);
            Name = name;
            LastMonthReading = lastMonth;
            ThisMonthReading = thisMonth;
        }

        // Business rule validation encapsulated in the domain entity
        private void ValidateReadings(int last, int thisMonth)
        {
            if (last < 0 || thisMonth < 0)
            {
                throw new ArgumentException("Readings cannot be negative.");
            }
            if (thisMonth < last)
            {
                throw new ArgumentException("This month's reading cannot be less than last month's reading.");
            }
        }

        // Method to update state with validation
        public void UpdateReadings(int lastMonth, int thisMonth)
        {
            ValidateReadings(lastMonth, thisMonth);
            LastMonthReading = lastMonth;
            ThisMonthReading = thisMonth;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            Name = name;
        }

        public int Usage => ThisMonthReading - LastMonthReading;

        public abstract string CustomerType { get; }

        public abstract decimal CalculateBill();

        public decimal CalculateBillWithVAT()
        {
            return CalculateBill() * 1.1m;
        }

        public virtual string GetBillInfo()
        {
            decimal billAmount = CalculateBill();
            decimal billWithVAT = CalculateBillWithVAT();
            DateTime today = DateTime.Now;
            DateTime fiveDaysLater = today.AddDays(5);

            return $"Dear Customer {Name}\n" +
                   $"Customer Type: {CustomerType}\n" +
                   $"Last month's electricity meter readings: {LastMonthReading}\n" +
                   $"This month's electricity meter readings: {ThisMonthReading}\n" +
                   $"Electricity Usage This Month: {Usage} kWh\n" +
                   $"Electricity Bill: {billAmount:N0} VND\n" +
                   $"Electricity Bill (Including 10% VAT): {billWithVAT:N0} VND\n" +
                   $"Payment Due Date: {fiveDaysLater:dd/MM/yyyy}";
        }
    }

    // Concrete class for Household customers
    public class HouseholdCustomer : Customer
    {
        public HouseholdCustomer(string name, int lastMonth, int thisMonth) 
            : base(name, lastMonth, thisMonth) { }

        public override string CustomerType => "Household";

        public override decimal CalculateBill()
        {
            int eValue = Usage;
            const int PRICE_L1 = 1984, PRICE_L2 = 2050, PRICE_L3 = 2380, 
                      PRICE_L4 = 2998, PRICE_L5 = 3350, PRICE_L6 = 3460;
            const int MAX_VAL_L1 = 50, MAX_VAL_L2 = 100, MAX_VAL_L3 = 200, 
                      MAX_VAL_L4 = 300, MAX_VAL_L5 = 400;

            if (eValue < 0) return 0;

            if (eValue <= MAX_VAL_L1)
                return eValue * PRICE_L1;
            
            if (eValue <= MAX_VAL_L2)
                return (eValue - MAX_VAL_L1) * PRICE_L2 
                       + MAX_VAL_L1 * PRICE_L1;

            if (eValue <= MAX_VAL_L3)
                return (eValue - MAX_VAL_L2) * PRICE_L3 
                       + MAX_VAL_L1 * PRICE_L1 
                       + (MAX_VAL_L2 - MAX_VAL_L1) * PRICE_L2;

            if (eValue <= MAX_VAL_L4)
                return (eValue - MAX_VAL_L3) * PRICE_L4 
                       + MAX_VAL_L1 * PRICE_L1 
                       + (MAX_VAL_L2 - MAX_VAL_L1) * PRICE_L2 
                       + (MAX_VAL_L3 - MAX_VAL_L2) * PRICE_L3;

            if (eValue <= MAX_VAL_L5)
                return (eValue - MAX_VAL_L4) * PRICE_L5 
                       + MAX_VAL_L1 * PRICE_L1 
                       + (MAX_VAL_L2 - MAX_VAL_L1) * PRICE_L2 
                       + (MAX_VAL_L3 - MAX_VAL_L2) * PRICE_L3 
                       + (MAX_VAL_L4 - MAX_VAL_L3) * PRICE_L4;

            return (eValue - MAX_VAL_L5) * PRICE_L6 
                   + MAX_VAL_L1 * PRICE_L1 
                   + (MAX_VAL_L2 - MAX_VAL_L1) * PRICE_L2 
                   + (MAX_VAL_L3 - MAX_VAL_L2) * PRICE_L3 
                   + (MAX_VAL_L4 - MAX_VAL_L3) * PRICE_L4 
                   + (MAX_VAL_L5 - MAX_VAL_L4) * PRICE_L5;
        }
    }

    // Concrete class for Public Service customers
    public class PublicServiceCustomer : Customer
    {
        public PublicServiceCustomer(string name, int lastMonth, int thisMonth) 
            : base(name, lastMonth, thisMonth) { }

        public override string CustomerType => "Public Service";

        public override decimal CalculateBill()
        {
            return Usage * 2887;
        }
    }

    // Concrete class for Production Units
    public class ProductionUnitCustomer : Customer
    {
        public ProductionUnitCustomer(string name, int lastMonth, int thisMonth) 
            : base(name, lastMonth, thisMonth) { }

        public override string CustomerType => "Production Units";

        public override decimal CalculateBill()
        {
            return 0; // Logic not defined in original code
        }
    }

    // Concrete class for Business Services
    public class BusinessServiceCustomer : Customer
    {
        public BusinessServiceCustomer(string name, int lastMonth, int thisMonth) 
            : base(name, lastMonth, thisMonth) { }

        public override string CustomerType => "Business Services";

        public override decimal CalculateBill()
        {
            return 0; // Logic not defined in original code
        }
    }
}
