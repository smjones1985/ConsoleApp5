using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Write an application that will let you keep a tally of how well six salesmen are 
            //performing selling three different products.You should use a two - dimensional array 
            //    to solve the problem.Allow the user to input the any number of sales amounts.
            //    Do a compile - time initialization of the salesperson's names and product list.
            //    Produce a report by salesman,showing the total sales per product. 
            //    3. Revise your solution for problem 2 so that you display the total 
            //    sales per salesman. Increase the number of salesmen to six.Place the 
            //    ﬁrst and last names for the six salesmen in an array.When you display 
            //        your ﬁnal output,print the salesman's last name only. After you 
            //        display the tables of sales,display the largest sales ﬁgure indicating 
            //        which salesman sold it and which product was sold

            int salesManCount = 2;
            int[,] salesCount = new int[salesManCount, 3];
            SalesMan[] salesMen = new SalesMan[salesManCount];
            Product[] products = new Product[] 
            {
                new Product() { Name = "God of War", Price = 59.99m},
                new Product() { Name = "Planescape: Torment", Price = 19.99m},
                new Product() { Name = "Doom", Price = 29.99m},
            };

            int counter = 0;
            while(counter < salesManCount)
            {
                Console.Clear();

                Console.WriteLine("Enter Salesman's first name:" );
                string name = Console.ReadLine();

                Console.WriteLine("Enter Salesman's last name:");
                string lastName = Console.ReadLine(); 

                salesMen[counter] = new SalesMan() { FirstName = name, LastName = lastName};

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter products sold:");
                    int count = 1;
                    foreach (Product product in products)
                    {
                        Console.WriteLine("{0}. {1}", count++, product.Name);
                    }
                    Console.WriteLine("{0}. Finished", count);

                    string input = Console.ReadLine();
                    int selection;
                    if (int.TryParse(input, out selection))
                    {
                        if (selection == products.Length + 1)
                        {
                            break;
                        }
                        selection--;
                        Console.Clear();
                        Console.WriteLine("How many did {0} sell?", name);
                        input = Console.ReadLine();
                        int quantity;
                        if (int.TryParse(input, out quantity))
                        {
                            salesCount[counter, selection] += quantity;
                        }
                    }
                }
                counter++;
            }
            DisplayTotalSalesGrid(products, salesMen, salesCount);
            CalculateAndDisplayLargestSale(products, salesMen, salesCount);

        }

        private static void CalculateAndDisplayLargestSale(Product[] products, SalesMan[] salesMen, int[,] salesCount)
        {
            Console.Clear();

            string salesManWithLargestSaleLastName = "";
            decimal largestSalesAmount = 0m;
            string largestSellingProductName = "";
            for (int i = 0; i < salesCount.GetLength(0); i++)
            {
                for (int j = 0; j < salesCount.GetLength(1); j++)
                {
                    decimal currentPrice = salesCount[i, j] * products[j].Price;
                    if (currentPrice > largestSalesAmount)
                    {
                        largestSalesAmount = currentPrice;
                        salesManWithLargestSaleLastName = salesMen[i].LastName;
                        largestSellingProductName = products[j].Name;
                    }
                }
            }
            Console.WriteLine("Salesman with best selling product is: {0}", salesManWithLargestSaleLastName);
            Console.WriteLine("Best selling product is: {0}", largestSellingProductName);
            Console.WriteLine("Total amount earned for selling {0} is {1:C}", largestSellingProductName, largestSalesAmount);
        }

        private static void DisplayTotalSalesGrid(Product[] products, SalesMan[] salesMen, int[,] salesCount)
        {
            Console.Clear();

            Console.Write("         ");

            for (int i = 0; i < products.Length; i++)
            {
                Console.Write(products[i].Name + "    ");
            }
            Console.WriteLine();
            for (int i = 0; i < salesMen.Length; i++)
            {
                Console.Write("{0}       ", salesMen[i].LastName);
                for (int j = 0; j < products.Length; j++)
                {
                    Console.Write("{0:C}    ", salesCount[i, j] * products[j].Price);
                }
            }
        }
    }
}