using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm
{
    internal class Customer
    {
        static string[] CustomerNameList = new string[100];
        static string[] CustomerTypeList = new string[100];
        static int[] ElectricityLastMonthAmount = new int[100];
        static int[] ElectricityThisMonthAmount = new int[100];
        static int CustomerQuantity = 0;

        const string CUSTOMER_TYPE_HOUSEHOLD = "Household";
        const string CUSTOMER_TYPE_PUBLIC_SERVICE = "Public Service";
        /*
         * Add customer information
         */
        static public bool AddCustomer(string name, string type, int lastMonth, int thisMonth)
        {
            CustomerNameList[CustomerQuantity] = name;
            switch (type)
            {
                case "HouseHold":
                    CustomerTypeList[CustomerQuantity] = CUSTOMER_TYPE_HOUSEHOLD;
                    break;
                case "Public Services":
                    CustomerTypeList[CustomerQuantity] = CUSTOMER_TYPE_PUBLIC_SERVICE;
                    break;
                case "Production Units":
                    CustomerTypeList[CustomerQuantity] = "Production Units"; // For now, perhaps fix later
                    break;
                case "Business Services":
                    CustomerTypeList[CustomerQuantity] = "Business Services"; // For now
                    break;
                default:
                    CustomerTypeList[CustomerQuantity] = CUSTOMER_TYPE_HOUSEHOLD;
                    break;
            }
            ElectricityLastMonthAmount[CustomerQuantity] = lastMonth;
            ElectricityThisMonthAmount[CustomerQuantity] = thisMonth;
            if (ElectricityThisMonthAmount[CustomerQuantity] < ElectricityLastMonthAmount[CustomerQuantity])
            {
                ElectricityThisMonthAmount[CustomerQuantity] = ElectricityLastMonthAmount[CustomerQuantity];
                CustomerQuantity++;
                return false; // invalid
            }
            else
            {
                CustomerQuantity++;
                return true;
            }
        }
        /*
         * Calculate electricity bill by level and eValue paramter
         */
        static int calculateBill(int eValue)
        {
            const int PRICE_L1 = 1984,
                      PRICE_L2 = 2050,
                      PRICE_L3 = 2380,
                      PRICE_L4 = 2998,
                      PRICE_L5 = 3350,
                      PRICE_L6 = 3460,
                      MAX_VAL_L1 = 50,
                      MAX_VAL_L2 = 100,
                      MAX_VAL_L3 = 200,
                      MAX_VAL_L4 = 300,
                      MAX_VAL_L5 = 400;
            int billAmount = 0;
            if (eValue < 0)
            {
                Console.WriteLine("Invalid Electricity Usage Value");
            }
            else if (eValue <= MAX_VAL_L1) // level 1
            {
                billAmount = eValue * PRICE_L1;
            }
            else if (eValue <= MAX_VAL_L2) // level 2
            {
                billAmount = (eValue - MAX_VAL_L1) * PRICE_L2
                            + (MAX_VAL_L1 - 0) * PRICE_L1;
            }
            else if (eValue <= MAX_VAL_L3) // level 3
            {
                billAmount = (eValue - MAX_VAL_L2) * PRICE_L3
                            + (MAX_VAL_L1 - 0) * PRICE_L1 + (MAX_VAL_L2 - MAX_VAL_L1) * PRICE_L2;
            }
            else if (eValue <= MAX_VAL_L4) // level 4
            {
                billAmount = (eValue - MAX_VAL_L3) * PRICE_L4
                            + (MAX_VAL_L1 - 0) * PRICE_L1 + (MAX_VAL_L2 - MAX_VAL_L1) * PRICE_L2 + (MAX_VAL_L3 - MAX_VAL_L2) * PRICE_L3;
            }
            else if (eValue <= MAX_VAL_L5) // level 5
            {
                billAmount = (eValue - MAX_VAL_L4) * PRICE_L5
                            + (MAX_VAL_L1 - 0) * PRICE_L1 + (MAX_VAL_L2 - MAX_VAL_L1) * PRICE_L2 + (MAX_VAL_L3 - MAX_VAL_L2) * PRICE_L3 + (MAX_VAL_L4 - MAX_VAL_L3) * PRICE_L4;
            }
            else // level 6
            {
                billAmount = (eValue - MAX_VAL_L5) * PRICE_L6
                            + (MAX_VAL_L1 - 0) * PRICE_L1 + (MAX_VAL_L2 - MAX_VAL_L1) * PRICE_L2 + (MAX_VAL_L3 - MAX_VAL_L2) * PRICE_L3 + (MAX_VAL_L4 - MAX_VAL_L3) * PRICE_L4 + (MAX_VAL_L5 - MAX_VAL_L4) * PRICE_L5;
            }
            return billAmount;
        }
        static int calculateHouseHoldBill(int eValue)
        {
            return calculateBill(eValue);
        }
        static int calculatePublicServiceBill(int eValue)
        {
            return eValue * 2887;
        }
        /*
         * Get bill info as string
         */
        static public string GetBillInfo(int index)
        {
            int ElectricityUsage = ElectricityThisMonthAmount[index] - ElectricityLastMonthAmount[index];
            int billAmount = 0;
            switch (CustomerTypeList[index])
            {
                case CUSTOMER_TYPE_HOUSEHOLD:
                    billAmount = calculateHouseHoldBill(ElectricityUsage);
                    break;
                case CUSTOMER_TYPE_PUBLIC_SERVICE:
                    billAmount = calculatePublicServiceBill(ElectricityUsage);
                    break;
                default:
                    billAmount = 0;
                    break;
            }
            double billWithVAT = billAmount * 1.1;
            DateTime today = DateTime.Now;
            DateTime fiveDaysLater = today.AddDays(5);
            return $"Dear Customer {CustomerNameList[index]}\n" +
                   $"Customer Type: {CustomerTypeList[index]}\n" +
                   $"Last month's electricity meter readings: {ElectricityLastMonthAmount[index]}\n" +
                   $"This month's electricity meter readings: {ElectricityThisMonthAmount[index]}\n" +
                   $"Electricity Usage This Month: {ElectricityUsage} kWh\n" +
                   $"Electricity Bill: {billAmount.ToString("N0")} VND\n" +
                   $"Electricity Bill (Including 10% VAT): {billWithVAT.ToString("N0")} VND\n" +
                   $"Payment Due Date: {fiveDaysLater.ToString("dd/MM/yyyy")}";
        }

        /*
         * Get all customers data
         */
        static public List<string[]> GetAllCustomers()
        {
            List<string[]> customers = new List<string[]>();
            for (int i = 0; i < CustomerQuantity; i++)
            {
                int usage = ElectricityThisMonthAmount[i] - ElectricityLastMonthAmount[i];
                int billAmount = 0;
                switch (CustomerTypeList[i])
                {
                    case CUSTOMER_TYPE_HOUSEHOLD:
                        billAmount = calculateHouseHoldBill(usage);
                        break;
                    case CUSTOMER_TYPE_PUBLIC_SERVICE:
                        billAmount = calculatePublicServiceBill(usage);
                        break;
                    default:
                        billAmount = 0;
                        break;
                }
                double billWithVAT = billAmount * 1.1;
                customers.Add(new string[] {
                    CustomerNameList[i],
                    CustomerTypeList[i],
                    ElectricityLastMonthAmount[i].ToString(),
                    ElectricityThisMonthAmount[i].ToString(),
                    usage.ToString(),
                    billWithVAT.ToString("N0")
                });
            }
            return customers;
        }

        /*
         * Delete customer at index
         */
        static public void DeleteCustomer(int index)
        {
            for (int i = index; i < CustomerQuantity - 1; i++)
            {
                CustomerNameList[i] = CustomerNameList[i + 1];
                CustomerTypeList[i] = CustomerTypeList[i + 1];
                ElectricityLastMonthAmount[i] = ElectricityLastMonthAmount[i + 1];
                ElectricityThisMonthAmount[i] = ElectricityThisMonthAmount[i + 1];
            }
            CustomerQuantity--;
        }

        /*
         * Get count
         */
        static public int GetCustomerCount()
        {
            return CustomerQuantity;
        }
    }
}
