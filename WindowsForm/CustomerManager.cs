using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsForm
{
    public class CustomerManager
    {
        private List<Customer> _customers;
        private readonly ICustomerRepository _repository;

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
                    Customer c = CreateCustomerFactory(dto.Name, dto.Type, dto.LastMonth, dto.ThisMonth, dto.PeopleCount);
                    if (c != null) _customers.Add(c);
                 } catch { /* Skip invalid data */ }
             }
        }

        public bool AddCustomer(string name, string type, int lastMonth, int thisMonth, int peopleCount = 0)
        {
            try 
            {
                Customer newCustomer = CreateCustomerFactory(name, type, lastMonth, thisMonth, peopleCount);
                
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

        public bool UpdateCustomer(int index, string name, string type, int lastMonth, int thisMonth, int peopleCount = 0)
        {
             if (index < 0 || index >= _customers.Count) return false;

             try
             {
                 Customer existing = _customers[index];
                 
                 // Check if type changed or if it is household we might need to update people count
                 if (!IsSameType(existing.CustomerType, type))
                 {
                     // Replace with new type
                     Customer newCustomer = CreateCustomerFactory(name, type, lastMonth, thisMonth, peopleCount);
                     _customers[index] = newCustomer;
                 }
                 else
                 {
                     // Update existing object
                     existing.UpdateName(name);
                     existing.UpdateReadings(lastMonth, thisMonth);
                     
                     if (existing is HouseholdCustomer hh)
                     {
                         hh.UpdatePeopleCount(peopleCount);
                     }
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
            // Normalize for robust comparison
            string t1 = NormalizeType(type1);
            string t2 = NormalizeType(type2);
            return t1 == t2;
        }

        private string NormalizeType(string type)
        {
             if (string.IsNullOrEmpty(type)) return "";
             string lower = type.ToLower().Replace(" ", "").Replace("/", "");
             
             if (lower.Contains("household")) return "household";
             if (lower.Contains("admin") || lower.Contains("public")) return "admin";
             if (lower.Contains("production")) return "production";
             if (lower.Contains("business")) return "business";
             
             return lower;
        }

        private Customer CreateCustomerFactory(string name, string type, int lastMonth, int thisMonth, int peopleCount)
        {
            string normalized = NormalizeType(type);

            switch (normalized)
            {
                case "household":
                    return new HouseholdCustomer(name, lastMonth, thisMonth, peopleCount);
                case "admin":
                    return new AdminCustomer(name, lastMonth, thisMonth);
                case "production":
                    return new ProductionCustomer(name, lastMonth, thisMonth);
                case "business":
                    return new BusinessCustomer(name, lastMonth, thisMonth);
                default:
                    throw new ArgumentException("Invalid Customer Type");
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
            return _customers;
        }

        public List<string[]> GetAllCustomersForView()
        {
            List<string[]> viewData = new List<string[]>();
            foreach (var customer in _customers)
            {
                // Calculate final bill with VAT (1.1) based on your logic that the class returns (Bill+Env)
                decimal finalBill = customer.CalculateBill() * 1.1m;
                
                viewData.Add(new string[] {
                    customer.Name,
                    customer.CustomerType,
                    customer.LastMonthReading.ToString(),
                    customer.ThisMonthReading.ToString(),
                    customer.Usage.ToString(),
                    finalBill.ToString("N0")
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
        
        public void SortByName()
        {
            _customers.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase));
            _repository.Save(_customers);
        }
        
        public void SortByUsage()
        {
            _customers.Sort((x, y) => y.Usage.CompareTo(x.Usage)); // Descending
            _repository.Save(_customers);
        }
        
        public List<Customer> SearchByName(string name)
        {
            return _customers.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}