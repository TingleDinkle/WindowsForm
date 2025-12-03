using System;
using System.Collections.Generic;

namespace WindowsForm
{
    public class CustomerManager
    {
        private List<Customer> _customers = new List<Customer>();

        public bool AddCustomer(string name, string type, int lastMonth, int thisMonth)
        {
            if (thisMonth < lastMonth)
            {
                return false; // Validation failed
            }

            Customer newCustomer = null;

            // Factory logic to create the correct customer type
            switch (type)
            {
                case "HouseHold":
                case "Household":
                    newCustomer = new HouseholdCustomer(name, lastMonth, thisMonth);
                    break;
                case "Public Services":
                case "Public Service":
                    newCustomer = new PublicServiceCustomer(name, lastMonth, thisMonth);
                    break;
                case "Production Units":
                    newCustomer = new ProductionUnitCustomer(name, lastMonth, thisMonth);
                    break;
                case "Business Services":
                    newCustomer = new BusinessServiceCustomer(name, lastMonth, thisMonth);
                    break;
                default:
                    // Default to Household if unknown, or handle as error. 
                    // Original code defaulted to Household behavior in switch but had named cases.
                    newCustomer = new HouseholdCustomer(name, lastMonth, thisMonth);
                    break;
            }

            if (newCustomer != null)
            {
                _customers.Add(newCustomer);
                return true;
            }
            return false;
        }

        public void DeleteCustomer(int index)
        {
            if (index >= 0 && index < _customers.Count)
            {
                _customers.RemoveAt(index);
            }
        }

        public Customer GetCustomer(int index)
        {
            if (index >= 0 && index < _customers.Count)
            {
                return _customers[index];
            }
            return null;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }

        // Helper to get data in the format ListView expects (List of string arrays)
        // This keeps Form1 logic cleaner or allows it to be easily adapted
        public List<string[]> GetAllCustomersForView()
        {
            List<string[]> viewData = new List<string[]>();
            foreach (var customer in _customers)
            {
                viewData.Add(new string[] {
                    customer.Name,
                    customer.CustomerType,
                    customer.LastMonthReading.ToString(),
                    customer.ThisMonthReading.ToString(),
                    customer.Usage.ToString(),
                    customer.CalculateBillWithVAT().ToString("N0")
                });
            }
            return viewData;
        }
        
        public int GetCustomerCount()
        {
            return _customers.Count;
        }
    }
}
