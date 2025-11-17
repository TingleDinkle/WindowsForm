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
         * input customer information
         */
        static void inputCustomerInfo()
        {
            bool isWrongInput = true;

            while (isWrongInput)
            {
                isWrongInput = false;
                Console.WriteLine("--------------------");
                Console.WriteLine("Input information for new customer: ");
                Console.WriteLine("Input customer name: ");
                CustomerNameList[CustomerQuantity] = Console.ReadLine();
                Console.WriteLine("Input customer type: (1 - Household; 2 - Public Service)");
                int typeChoice = 1;
                switch (typeChoice)
                {
                    case 1:
                        CustomerTypeList[CustomerQuantity] = CUSTOMER_TYPE_HOUSEHOLD;
                        break;
                    case 2:
                        CustomerTypeList[CustomerQuantity] = CUSTOMER_TYPE_PUBLIC_SERVICE;
                        break;
                    default:
                        isWrongInput = true;
                        CustomerTypeList[CustomerQuantity] = "Please only choose the values described";
                        break;
                }
                CustomerTypeList[CustomerQuantity] = Console.ReadLine();
                Console.WriteLine("Input electricity usage of last month: ");
                ElectricityLastMonthAmount[CustomerQuantity] = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input electricity usage of this month: ");
                ElectricityThisMonthAmount[CustomerQuantity] = Convert.ToInt32(Console.ReadLine());
                if (ElectricityThisMonthAmount[CustomerQuantity] < ElectricityLastMonthAmount[CustomerQuantity])
                {
                    isWrongInput = true;
                    Console.WriteLine("Invalid electricity usage value, default usage = 0");
                    ElectricityThisMonthAmount[CustomerQuantity] = ElectricityLastMonthAmount[CustomerQuantity];
                }
                else
                {
                    isWrongInput = false;
                }
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
         * print bill info
         */
        static void printBill(int index)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Dear Customer {CustomerNameList[index]}");
            Console.WriteLine($"Customer Type: {CustomerTypeList[index]}");
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
                    Console.WriteLine("Invalid Customer Type");
                    break;
            }
            //int billAmount = calculateHouseHoldBill(ElectricityUsage);
            Console.WriteLine($"Last month's electricity meter readings: {ElectricityLastMonthAmount[index]}");
            Console.WriteLine($"This month's electricity meter readings: {ElectricityThisMonthAmount[index]}");
            Console.WriteLine($"Electricity Usage This Month: {ElectricityUsage} kWh");
            Console.WriteLine("Bill has been calculated");
            Console.WriteLine($"Electricity Bill: {billAmount.ToString("N0")} VND"); // "N0" là Nờ và số 0 - anà
            double billWithVAT = billAmount * 1.1;
            Console.WriteLine($"Electricity Bill (Including 10% VAT): {billWithVAT.ToString("N0")} VND");
            DateTime today = DateTime.Now;
            DateTime fiveDaysLater = today.AddDays(5);
            Console.WriteLine($"Payment Due Date: {fiveDaysLater.ToString("dd/MM/yyyy")}");
        }
    }
}
