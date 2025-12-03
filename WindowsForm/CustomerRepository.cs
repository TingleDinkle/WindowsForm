using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace WindowsForm
{
    public interface ICustomerRepository
    {
        void Save(List<Customer> customers);
        List<CustomerDto> Load();
        void ExportToCsv(List<Customer> customers, string filePath);
    }

    public class CustomerDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int LastMonth { get; set; }
        public int ThisMonth { get; set; }
    }

    public class JsonCustomerRepository : ICustomerRepository
    {
        private const string DATA_FILE = "customers.json";

        public void Save(List<Customer> customers)
        {
            try
            {
                var dtos = customers.Select(c => new CustomerDto
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
                Console.WriteLine("Error saving file: " + ex.Message);
            }
        }

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

        public void ExportToCsv(List<Customer> customers, string filePath)
        {
            var lines = new List<string>();
            lines.Add("Name,Type,LastMonth,ThisMonth,Usage,BillWithVAT");
            
            foreach (var c in customers)
            {
                lines.Add($"{EscapeCsv(c.Name)},{EscapeCsv(c.CustomerType)},{c.LastMonthReading},{c.ThisMonthReading},{c.Usage},{c.CalculateBillWithVAT()}");
            }

            File.WriteAllLines(filePath, lines);
        }

        private string EscapeCsv(string field)
        {
            if (field == null) return "";
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }
    }
}