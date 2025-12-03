using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsForm
{
    public class CustomerManager
    {
        private List<Customer> _customers;
        private readonly ICustomerRepository _repository;

        // Dependency Injection: The manager depends on an abstraction (interface), not concrete file IO.
        public CustomerManager(ICustomerRepository repository)
        {
            _repository = repository;
            _customers = new List<Customer>();
        }

        public void LoadData()
        {
             var dtos = _repository.Load();
             _customers.Clear();
             foreach (var dto in dtos)
             {
                 try {
                    Customer c = CreateCustomerFactory(dto.Name, dto.Type, dto.LastMonth, dto.ThisMonth);
                    if (c != null) _customers.Add(c);
                 } catch { /* Skip invalid data */ }
             }
        }

        public bool AddCustomer(string name, string type, int lastMonth, int thisMonth)
        {
            try 
            {
                // Domain factory
                Customer newCustomer = CreateCustomerFactory(name, type, lastMonth, thisMonth);
                
                if (newCustomer != null)
                {
                    _customers.Add(newCustomer);
                    _repository.Save(_customers);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCustomer(int index, string name, string type, int lastMonth, int thisMonth)
        {
             if (index < 0 || index >= _customers.Count) return false;

             try
             {
                 // Check if type changed. If so, we need a new object.
                 Customer existing = _customers[index];
                 
                 // Normalize type strings for comparison
                 if (!IsSameType(existing.CustomerType, type))
                 {
                     // Replace with new type
                     Customer newCustomer = CreateCustomerFactory(name, type, lastMonth, thisMonth);
                     _customers[index] = newCustomer;
                 }
                 else
                 {
                     // Update existing object
                     existing.UpdateName(name);
                     existing.UpdateReadings(lastMonth, thisMonth);
                 }

                 _repository.Save(_customers);
                 return true;
             }
             catch (Exception)
             {
                 return false;
             }
        }
        
        private bool IsSameType(string type1, string type2)
        {
            // Simple normalization for comparison
            return type1.Replace(" ", "").Equals(type2.Replace(" ", ""), StringComparison.OrdinalIgnoreCase);
        }

        private Customer CreateCustomerFactory(string name, string type, int lastMonth, int thisMonth)
        {
            // Simple normalization
            string normalizedType = type.Replace(" ", "").ToLower();

            switch (normalizedType)
            {
                case "household":
                    return new HouseholdCustomer(name, lastMonth, thisMonth);
                case "publicservices":
                case "publicservice":
                    return new PublicServiceCustomer(name, lastMonth, thisMonth);
                case "productionunits":
                    return new ProductionUnitCustomer(name, lastMonth, thisMonth);
                case "businessservices":
                    return new BusinessServiceCustomer(name, lastMonth, thisMonth);
                default:
                    // Fallback or throw
                    return new HouseholdCustomer(name, lastMonth, thisMonth);
            }
        }

        public void DeleteCustomer(int index)
        {
            if (index >= 0 && index < _customers.Count)
            {
                _customers.RemoveAt(index);
                _repository.Save(_customers);
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
            return _customers; // Return reference or clone depending on strictness. Reference is fine here.
        }

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

        public void ExportToCsv(string filePath)
        {
            _repository.ExportToCsv(_customers, filePath);
        }
    }
}
