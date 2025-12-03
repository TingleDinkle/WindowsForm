using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace WindowsForm
{
    public class CustomerManager
    {
        private List<Customer> _customers = new List<Customer>();

        // Helper class for JSON serialization since abstract classes can be tricky 
        // without polymorphic configuration in simpler JSON scenarios.
        // We'll use a DTO (Data Transfer Object) approach for simplicity and robustness.
        private class CustomerDto
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int LastMonth { get; set; }
            public int ThisMonth { get; set; }
        }

        public bool AddCustomer(string name, string type, int lastMonth, int thisMonth)
        {
            if (thisMonth < lastMonth)
            {
                return false;
            }

            Customer newCustomer = CreateCustomerFactory(name, type, lastMonth, thisMonth);

            if (newCustomer != null)
            {
                _customers.Add(newCustomer);
                SaveToFile(); // Auto-save
                return true;
            }
            return false;
        }

        public bool UpdateCustomer(int index, string name, string type, int lastMonth, int thisMonth)
        {
             if (index < 0 || index >= _customers.Count) return false;
             if (thisMonth < lastMonth) return false;

             Customer updatedCustomer = CreateCustomerFactory(name, type, lastMonth, thisMonth);
             if (updatedCustomer != null)
             {
                 _customers[index] = updatedCustomer;
                 SaveToFile(); // Auto-save
                 return true;
             }
             return false;
        }

        private Customer CreateCustomerFactory(string name, string type, int lastMonth, int thisMonth)
        {
            switch (type)
            {
                case "HouseHold":
                case "Household":
                    return new HouseholdCustomer(name, lastMonth, thisMonth);
                case "Public Services":
                case "Public Service":
                    return new PublicServiceCustomer(name, lastMonth, thisMonth);
                case "Production Units":
                    return new ProductionUnitCustomer(name, lastMonth, thisMonth);
                case "Business Services":
                    return new BusinessServiceCustomer(name, lastMonth, thisMonth);
                default:
                    return new HouseholdCustomer(name, lastMonth, thisMonth);
            }
        }

        public void DeleteCustomer(int index)
        {
            if (index >= 0 && index < _customers.Count)
            {
                _customers.RemoveAt(index);
                SaveToFile(); // Auto-save
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

        // Persistence Logic
        private const string DATA_FILE = "customers.json";

        public void SaveToFile()
        {
            try
            {
                var dtos = _customers.Select(c => new CustomerDto
                {
                    Name = c.Name,
                    Type = c.CustomerType,
                    LastMonth = c.LastMonthReading,
                    ThisMonth = c.ThisMonthReading
                }).ToList();

                string jsonString = JsonSerializer.Serialize(dtos);
                File.WriteAllText(DATA_FILE, jsonString);
            }
            catch (Exception ex)
            {
                // In a real app, log this. For now, we swallow or let UI handle explicit save errors.
                Console.WriteLine("Error saving file: " + ex.Message);
            }
        }

        public void LoadFromFile()
        {
            if (!File.Exists(DATA_FILE)) return;

            try
            {
                string jsonString = File.ReadAllText(DATA_FILE);
                var dtos = JsonSerializer.Deserialize<List<CustomerDto>>(jsonString);
                
                _customers.Clear();
                if (dtos != null)
                {
                    foreach (var dto in dtos)
                    {
                        AddCustomer(dto.Name, dto.Type, dto.LastMonth, dto.ThisMonth);
                    }
                }
            }
            catch (Exception ex)
            {
                 Console.WriteLine("Error loading file: " + ex.Message);
            }
        }

        // Export Logic
        public void ExportToCsv(string filePath)
        {
            var lines = new List<string>();
            lines.Add("Name,Type,LastMonth,ThisMonth,Usage,BillWithVAT");
            
            foreach (var c in _customers)
            {
                lines.Add($"{EscapeCsv(c.Name)},{EscapeCsv(c.CustomerType)},{c.LastMonthReading},{c.ThisMonthReading},{c.Usage},{c.CalculateBillWithVAT()}");
            }

            File.WriteAllLines(filePath, lines);
        }

        private string EscapeCsv(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }
    }
}