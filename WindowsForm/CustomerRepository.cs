using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace WindowsForm
{
    // Interface for the Repository Pattern.
    // Decouples the business logic from the specific data storage implementation (JSON, Database, etc.).
    public interface ICustomerRepository
    {
        void Save(List<Customer> customers);
        List<CustomerDto> Load();
        void ExportToCsv(List<Customer> customers, string filePath);
    }

    // Data Transfer Object (DTO).
    // Used to serialize/deserialize customer data without exposing the complex logic of the domain classes.
    public class CustomerDto
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public int LastMonth { get; set; }
        public int ThisMonth { get; set; }
        public int PeopleCount { get; set; }
    }

    // Concrete implementation of ICustomerRepository using a JSON file for storage.
    public class JsonCustomerRepository : ICustomerRepository
    {
        private const string DATA_FILE = "customers.json";

        // Maps domain objects to DTOs and writes them to the JSON file.
        public void Save(List<Customer> customers)
        {
            try
            {
                var dtos = customers.Select(c => new CustomerDto
                {
                    Name = c.Name,
                    Type = c.CustomerType,
                    LastMonth = c.LastMonthReading,
                    ThisMonth = c.ThisMonthReading,
                    // Extract specific property if it's a HouseholdCustomer
                    PeopleCount = (c is HouseholdCustomer h) ? h.PeopleCount : 0
                }).ToList();

                string jsonString = JsonSerializer.Serialize(dtos);
                File.WriteAllText(DATA_FILE, jsonString);
            }
            catch (Exception ex)
            {
                // In a real app, use a logger.
                Console.WriteLine("Error saving file: " + ex.Message);
            }
        }

        // Reads DTOs from the JSON file. 
        // The Manager layer is responsible for converting these DTOs back into rich domain objects.
        public List<CustomerDto> Load()
        {
            if (!File.Exists(DATA_FILE)) return new List<CustomerDto>();

            try
            {
                string jsonString = File.ReadAllText(DATA_FILE);
                return JsonSerializer.Deserialize<List<CustomerDto>>(jsonString) ?? new List<CustomerDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
                return new List<CustomerDto>();
            }
        }

        // Exports the current customer list to a CSV file for external use (e.g., Excel).
        public void ExportToCsv(List<Customer> customers, string filePath)
        {
            var lines = new List<string>();
            lines.Add("Name,Type,People,LastMonth,ThisMonth,Usage,BillWithVAT");
            
            foreach (var c in customers)
            {
                int people = (c is HouseholdCustomer h) ? h.PeopleCount : 0;
                decimal finalBill = c.CalculateBillWithVAT();
                
                lines.Add($"{EscapeCsv(c.Name)},{EscapeCsv(c.CustomerType)},{people},{c.LastMonthReading},{c.ThisMonthReading},{c.Usage},{finalBill:F0}");
            }

            File.WriteAllLines(filePath, lines);
        }

        // Helper to handle special characters (commas, quotes) in CSV fields.
        private string EscapeCsv(string? field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }
    }
}