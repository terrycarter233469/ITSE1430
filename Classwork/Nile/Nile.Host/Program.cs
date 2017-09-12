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
            } while(!quit);
        }

        private static void ListProducts()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine().Trim();

            //ensure not empty

            Console.Write("Enter price (>0): ");
            string price = Console.ReadLine().Trim();

            Console.Write("Enter optional description: ");
            productDescription = Console.ReadLine().Trim();

            Console.Write("Is it discontinued (Y?N: ");
            string discontinued = Console.ReadLine().Trim();
        }

        private static void AddProduct()
        {
            throw new NotImplementedException();
        }

        static char GetInput()
        {
            while(true)
            {
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
                    else if (letter == 'b')
                        return 'B';
                    else if (letter == 'c')
                        return 'C';
                };
                //Error
                Console.WriteLine("Please choose a valid menu option");

            }

        }

        //Product
        static string productName;
        static decimal productPrice;
        static string productDescription;
        static bool productDiscontinued;
    }
}
