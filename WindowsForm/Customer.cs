using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsForm
{
    // Abstract base class representing a generic Customer.
    // This acts as the 'Model' in the architecture, encapsulating data and business logic.
    public abstract class Customer
    {
        // Encapsulated state for customer details and meter readings.
        public string Name { get; private set; }
        public int LastMonthReading { get; private set; }
        public int ThisMonthReading { get; private set; }

        // Protected constructor forces creation through specific subclasses.
        protected Customer(string name, int lastMonth, int thisMonth)
        {
            ValidateReadings(lastMonth, thisMonth);
            Name = name;
            LastMonthReading = lastMonth;
            ThisMonthReading = thisMonth;
        }

        // Centralized validation logic ensures data integrity across all customer types.
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

        // Updates reading data, re-applying validation rules.
        public void UpdateReadings(int lastMonth, int thisMonth)
        {
            ValidateReadings(lastMonth, thisMonth);
            LastMonthReading = lastMonth;
            ThisMonthReading = thisMonth;
        }

        // Updates the customer name with basic validation.
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            Name = name;
        }

        // Calculated property for water consumption.
        public int Usage => ThisMonthReading - LastMonthReading;

        // Abstract property to be implemented by subclasses to identify their specific type.
        public abstract string CustomerType { get; }

        // Abstract method: Each subclass MUST define its own pricing formula.
        // Returns the Bill Amount + Environment Fee (before VAT).
        public abstract decimal CalculateBill();

        // Standard VAT calculation (10%) applied to all customers.
        public decimal CalculateBillWithVAT()
        {
            return CalculateBill() * 1.1m;
        }
        
        // Generates a formatted bill summary string.
        public virtual string GetBillInfo()
        {
            decimal billAmount = CalculateBill(); 
            decimal billWithVAT = CalculateBillWithVAT();
            DateTime today = DateTime.Now;
            DateTime fiveDaysLater = today.AddDays(5);

            return $"Dear Customer {Name}\n" +
                   $"Customer Type: {CustomerType}\n" +
                   $"Last month's water meter readings: {LastMonthReading}\n" +
                   $"This month's water meter readings: {ThisMonthReading}\n" +
                   $"Water Usage This Month: {Usage} m3\n" +
                   $"Water Bill: {billAmount:N0} VND\n" +
                   $"Water Bill (Including 10% VAT): {billWithVAT:N0} VND\n" +
                   $"Payment Due Date: {fiveDaysLater:dd/MM/yyyy}";
        }
    }

    // Concrete class for Household customers with Tiered Pricing logic.
    public class HouseholdCustomer : Customer
    {
        public int PeopleCount { get; private set; }

        public HouseholdCustomer(string name, int lastMonth, int thisMonth, int peopleCount) 
            : base(name, lastMonth, thisMonth) 
        {
            if (peopleCount <= 0) throw new ArgumentException("Household must have at least 1 person.");
            PeopleCount = peopleCount;
        }

        public void UpdatePeopleCount(int count)
        {
            if (count <= 0) throw new ArgumentException("Household must have at least 1 person.");
            PeopleCount = count;
        }

        public override string CustomerType => "Household";

        // Calculates bill using tiered pricing based on usage PER PERSON.
        public override decimal CalculateBill()
        {
            int wValue = Usage;
            int people = PeopleCount;
            
            // Pricing constants for each tier
            const double PRICE_T1 = 5973,
                         PRICE_T2 = 7052,
                         PRICE_T3 = 8699,
                         PRICE_T4 = 15929;
            
            // Calculate usage thresholds based on number of people
            int tier1 = 10 * people;
            int tier2 = 20 * people;
            int tier3 = 30 * people;
            
            double billAmount = 0;

            // Determine which tier the usage falls into and calculate cumulatively
            if (wValue <= tier1)
            {
                billAmount = wValue * PRICE_T1;
            }
            else if (wValue <= tier2)
            {
                billAmount = (wValue - tier1) * PRICE_T2
                            + tier1 * PRICE_T1;
            }
            else if (wValue <= tier3)
            {
                billAmount = (wValue - tier2) * PRICE_T3
                            + tier1 * PRICE_T1 + (tier2 - tier1) * PRICE_T2;
            }
            else
            {
                billAmount = (wValue - tier3) * PRICE_T4
                            + tier1 * PRICE_T1 + (tier2 - tier1) * PRICE_T2 + (tier3 - tier2) * PRICE_T3;
            }

            // Add 10% Environment Fee
            double envFee = billAmount * 0.10;
            return (decimal)((int)(billAmount + envFee));
        }
        
        public override string GetBillInfo()
        {
            return $"Number of people in household: {PeopleCount}\n" + base.GetBillInfo();
        }
    }

    // Concrete class for Administrative Agencies (Flat Rate).
    public class AdminCustomer : Customer
    {
        public AdminCustomer(string name, int lastMonth, int thisMonth) 
            : base(name, lastMonth, thisMonth) { }

        public override string CustomerType => "Administrative Agency";

        public override decimal CalculateBill()
        {
            double basePrice = Usage * 9955;
            double envFee = basePrice * 0.10;
            return (decimal)((int)(basePrice + envFee));
        }
    }

    // Concrete class for Production Units (Flat Rate).
    public class ProductionCustomer : Customer
    {
        public ProductionCustomer(string name, int lastMonth, int thisMonth) 
            : base(name, lastMonth, thisMonth) { }

        public override string CustomerType => "Production Unit";

        public override decimal CalculateBill()
        {
            double basePrice = Usage * 11615;
            double envFee = basePrice * 0.10;
            return (decimal)((int)(basePrice + envFee));
        }
    }

    // Concrete class for Business Services (Flat Rate).
    public class BusinessCustomer : Customer
    {
        public BusinessCustomer(string name, int lastMonth, int thisMonth) 
            : base(name, lastMonth, thisMonth) { }

        public override string CustomerType => "Business Service";

        public override decimal CalculateBill()
        {
            double basePrice = Usage * 22068;
            double envFee = basePrice * 0.10;
            return (decimal)((int)(basePrice + envFee));
        }
    }
}
