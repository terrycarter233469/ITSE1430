/*
 * Your Name
 * ITSE 1430
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nile.Host
{
    class Program
    {
        // A single line comment
        static void Main( string[] args )
        {
            bool quit = false;
            do
            {
                char choice = GetInput();
                switch (choice)
                {
                    case 'a':
                    case 'A':
                        AddProduct();
                        break;

                    case 'b':
                    case 'B':
                        ListProducts();
                        break;

                    case 'c':
                    case 'C':
                        quit = true;
                        break;
                };
            } while (!quit);
        }

        private static void ListProducts()
        {

           //string msg = String.Format("{0}\t\t\t{1}\t\t[{3}]", productName, productPrice, productDiscontinued ? "[Discontomied]" : "");
            string msg = $"{productName}\t\t\t${productPrice}\t\t{(productDiscontinued ? "[Discontinued]" : "")}";      //string interpulation mode

            Console.WriteLine(msg);
            Console.WriteLine(productDescription);
        }

        private static void AddProduct()
        {
            Console.Write("Enter product name: ");
            productName = Console.ReadLine().Trim();

            //ensure not empty

            Console.Write("Enter price (>0): ");
            productPrice = ReadDecimal();

            Console.Write("Enter optional description: ");
            productDescription = Console.ReadLine().Trim();

            Console.Write("Is it discontinued (Y/N: ");
            productDiscontinued = ReadYesNo();

            //Name $price[Discontinued]
            //Description
        }

        static char GetInput()
        {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Main Menu");
                    Console.WriteLine("".PadLeft(10, '-'));
                    Console.WriteLine("A) Add product");
                    Console.WriteLine("B) List product");
                    Console.WriteLine("C) Quit product");

                    string input = Console.ReadLine().Trim();

                    if (input != String.Empty)  //input != "" and input.Length == 0 both work, but .Length needs to check for null
                    {
                        //String Comparion
                        //if (String.Compare(input, "A", true) == 0) //case insensitive comparison, returns an int (0 is equal, 1 is not equal)
                        //    return 'A';

                        //Char comparison
                        char letter = Char.ToUpper(input[0]);
                        if (letter == 'A')
                            return 'A';
                        else if (letter == 'B')
                            return 'B';
                        else if (letter == 'C')
                            return 'C';
                    };
                    //Error
                    Console.WriteLine("Please choose a valid menu option");
                }

            }

        /// <summary>Reads a decimal from Console.</summary>
        /// <returns>The decimal value.</returns>
        static decimal ReadDecimal()
        {
            do
            {
                string input = Console.ReadLine();


                //Output Parameter( out datatype parameter )
                //    function must be able to write
                //    function can read after write
                //    function call passes no value
                //inline variable declaration only works with out
                if (Decimal.TryParse(input, out decimal result))
                    return result;

                Console.WriteLine("Enter a valid decimal");
            } while (true);
        }

        static bool ReadYesNo()
        {
            do
            {
                string input = Console.ReadLine();
                
                if(!String.IsNullOrEmpty(input))
                {
                    switch (Char.ToUpper(input[0]))
                    {
                        case 'Y': return true;
                        case 'N': return false;
                    }
                }

                Console.WriteLine("Enter either Y or N");
            } while (true);
        }


        //Product
        static string productName;
            static decimal productPrice;
            static string productDescription;
            static bool productDiscontinued;
        }
    }

