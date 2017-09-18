/*Terry Carter
 * ITSE1430
 * 8/16/2017
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieProgram.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            do
            {
                char choice = GetInput();
                switch (choice)
                {
                    case 'L': ListMovies(); break;
                    case 'A': AddMovies(); break;
                    case 'R': RemoveMovies(); break;
                    case 'Q': quit = true; break;
                };
            } while (!quit);
        }

        private static void AddMovies()
        {
            Console.Write("Enter the title of the movie: ");
            movieTitle = Console.ReadLine().Trim();

            //Ensure not empty

            Console.Write("Enter the length of the movie(Minutes): ");
            movieLength = ReadLength();

            Console.Write("Enter an optional description: ");
            movieDescription = Console.ReadLine().Trim();

            Console.Write("Do you own this movie? (Y/N): ");
            movieOwned = ReadYesNo();
        }

        private static void ListMovies()
        {
            if(movieTitle != "")
            {
                Console.WriteLine(movieTitle);
                Console.WriteLine(movieDescription);
                string msg = $"Run length = {movieLength} mins";
                Console.WriteLine(msg);
                if(movieOwned)
                    Console.WriteLine("Status = Owned");
                else
                    Console.WriteLine("Status = WishList");
            } else
                Console.WriteLine("No Movies available");
        }

        public static void RemoveMovies()
        {
            if(!String.IsNullOrEmpty(movieTitle))
            {
                movieTitle = "";
                movieLength = 0;
                movieDescription = "";
                movieOwned = false;
                Console.WriteLine("The move has been removed");
            } 
            else
                Console.WriteLine("Error: There is no movies to remove");
        }

        static char GetInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("".PadLeft(10, '-'));
                Console.WriteLine("L)ist Movie");
                Console.WriteLine("A)dd Movie");
                Console.WriteLine("R)emove Movie");
                Console.WriteLine("Q)uit");

                var input = Console.ReadLine().Trim();
                if (input != null && input.Length != 0)
                {
                    //Char comparison
                    char letter = Char.ToUpper(input[0]);
                    if (letter == 'A')
                        return 'A';
                    else if (letter == 'L')
                        return 'L';
                    else if (letter == 'Q')
                        return 'Q';
                    else if (letter == 'R')
                        return 'R';
                };

                //Error
                Console.WriteLine("Please choose a valid option");
            };
        }

        /// <summary>Reads a boolean from Console.</summary>
        /// <returns>The boolean value.</returns>
        static bool ReadYesNo()
        {
            do
            {
                string input = Console.ReadLine();

                if (!String.IsNullOrEmpty(input))
                {
                    switch (Char.ToUpper(input[0]))
                    {
                        case 'Y': return true;
                        case 'N': return false;
                    };
                };

                Console.WriteLine("Enter either Y or N");
            } while (true);
        }

        static int ReadLength()
        {
            int length = 0;
            bool isNumber = false;
            while(!isNumber)
            {
                string input = Console.ReadLine();

                if(!String.IsNullOrEmpty(input))
                {
                    isNumber = Int32.TryParse(input, out length);
                };

                if(!isNumber || length < 0)
                {
                    Console.WriteLine("Enter  a valid length of 0 or higher");
                    isNumber = false;
                }
            }

            return length;
        }

        //Movie variables
        static string movieTitle = "";
        static int movieLength = 0;
        static string movieDescription = "";
        static bool movieOwned = false;
    }
}
