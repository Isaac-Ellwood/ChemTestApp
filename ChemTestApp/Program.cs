using System;
using System.Collections.Generic;
using System.Linq;

namespace ChemTestApp
{
    class Program
    {
        // global chemical name list
        static List<string> chemNameList = new List<string>();

        // main method
        static void Main(string[] args)
        {
            // define variables
            string chemicalName = "";
            int initialGerms, remainingGerms, time, userChoice = 1, randNumChoice;
            double acidEffect = 0;

            // creates SortedList of int keys, string values 
            SortedList<double, string> chemList = new SortedList<double, string>();


            // welcome message
            Console.WriteLine("=============================================\n" +
                "Welcome to:\n" +
                "\n" +
                " █████  ██   ██ ███████ ███    ███ ████████ ███████  ██████ ████████  █████  ██████  ██████  \n" +
                "██   ██ ██   ██ ██      ████  ████    ██    ██      ██         ██    ██   ██ ██   ██ ██   ██ \n" +
                "██      ███████ █████   ██ ████ ██    ██    █████    █████     ██    ███████ ██████  ██████  \n" +
                "██   ██ ██   ██ ██      ██  ██  ██    ██    ██           ██    ██    ██   ██ ██      ██      \n" +
                " █████  ██   ██ ███████ ██      ██    ██    ███████ ██████     ██    ██   ██ ██      ██      \n" +
                "\n" +
                "This app is used to find and sort the effectiveness of chemicals.\n" +
                "=============================================");

            // while loop allows user to input multiple chemicals
            bool flag = true;
            while (flag)
            {
                if (userChoice == 2)
                {
                    flag = false;
                }
                else
                {
                    // user inputs chemical name
                    chemicalName = StringCheck();

                    // user chooses whether to enter the initial germs themselves, or have it randomly generated for them
                    Console.WriteLine("=============================================\n" +
                        "Would you like to enter your own initial germ values, or have them randomly generated?\n" +
                        "1. Enter them myself\n" +
                        "2. Have them randomly generated\n" +
                        "=============================================");
                    randNumChoice = NumCheck(1, 2);

                    // loop process 5 times to gather more data
                    for (int i = 0; i < 5; i++)
                    {
                        // user inputs number of initial germs, time taken, and remaining germs
                        if (randNumChoice == 1)
                        {
                            Console.WriteLine("Enter the number of intitial germs:");
                            initialGerms = NumCheck(1, 1000);
                        }
                        else
                        {
                            Random rand = new Random();
                            initialGerms = rand.Next(10, 1001);
                            Console.WriteLine($"{initialGerms} germs have been added to {chemicalName}");
                        }
                        Console.WriteLine($"How long were the germs left in {chemicalName}? (in seconds)");
                        time = NumCheck(1, 1000);
                        Console.WriteLine("Enter the number of germs left:");
                        remainingGerms = NumCheck(0, initialGerms);

                        // calculate to find effectiveness of acid * 5
                        acidEffect = +(initialGerms - remainingGerms) / time;
                    }
                    // calculate average acid effectiveness 
                    acidEffect = acidEffect / 5;

                    // while loop repeats until valid
                    bool flag2 = true;
                    while (flag2)
                    {
                        // while loop repeats until acidEffect and chemicalName can be added to the SortedList
                        try
                        {
                            chemList.Add(acidEffect, chemicalName);
                            chemNameList.Add(chemicalName);
                            flag2 = false;
                        }
                        // if the acidEffect and chemicalName cannot be added to the SortedList, an un
                        catch
                        {
                            acidEffect += 0.00000000000001;
                        }
                    }

                    // if userChoice == 1, then the while loop will end
                    Console.WriteLine("=============================================\n" +
                        "would you like to enter another chemical?\n" +
                        "1. Yes\n" +
                        "2. No\n" +
                        "=============================================");
                    userChoice = NumCheck(1, 2);
                }
            }

            // declaring variables used for output display
            int listLength = chemList.Count;
            var displayList = chemList.Take(0);

            // while loop repeats so that user can choose multiple display options
            bool flag3 = true;
            while (flag3)
            {
                Console.WriteLine("=============================================\n" +
                        "1. Display 3 least effective chemicals\n" +
                        "2. Display 3 most effective chemicals\n" +
                        "3. Display entire list\n" +
                        "4. Exit\n" +
                        "=============================================");

                userChoice = NumCheck(1, 4);
                if (userChoice >= 1 && userChoice <= 3)
                {
                    // set displayList to either the entire chemical list, the 3 least effective chemicals, or the three most effective chemicals
                    if (userChoice == 1)
                    {
                        displayList = chemList.Take(3);
                    }
                    else if (userChoice == 2)
                    {
                        displayList = chemList.Skip(listLength - 3).Take(3);
                    }
                    else
                    {
                        displayList = chemList.Take(listLength);
                    }

                    // outputs displayList
                    Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n" +
                        "Chemicals from lowest to highest:\n" +
                        "");
                    foreach (var kvp in displayList)
                    {
                        Console.WriteLine($"Chemical:{kvp.Value} Effectiveness:{Math.Round(kvp.Key, 10)}");
                    }
                }
                else
                {
                    flag3 = false;
                }
            }
        }

        // integer check method
        static int NumCheck(int min, int max)
        {
            // declaring variables
            string ERRORMSG = $"ERROR: Please enter a number between {min} and {max}";
            int numInput = 0;
            bool flag = true;
            while (flag)
            {
                // try catch statement ensures that the input is an integer
                try
                {
                    numInput = Convert.ToInt32(Console.ReadLine());
                    if (numInput >= min && numInput <= max)
                    {
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine(ERRORMSG);
                    }
                }
                catch
                {
                    Console.WriteLine(ERRORMSG);
                }
            }
            // returns correct integer
            return numInput;
        }

        // string check method
        static string StringCheck()
        {
            // declaring variables
            string stringInput = "";
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Enter the chemical name:");
                stringInput = Console.ReadLine();

                // makes sure the chemical name being entered has not already been entered
                if (chemNameList.Contains(stringInput))
                {
                    Console.WriteLine("ERROR: You cannot enter the same chemical twice");
                }
                else
                {
                    flag = false;
                }
            }
            // returns correct string
            return stringInput;
        }
    }
}
