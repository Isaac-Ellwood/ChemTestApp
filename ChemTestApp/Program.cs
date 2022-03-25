// import modules
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChemTestApp
{
    class Program
    {
        // main method
        static void Main(string[] args)
        {
            // define variables
            string chemicalName = "";
            int initialGerms, remainingGerms, time = 1, displayChoice;
            double acidEffect = 0;
            // creates SortedList of int keys, string values 
            SortedList<double, string> chemList = new SortedList<double, string>();
            // welcome message
            Console.WriteLine("Welcome to Chemical Test app.\n" +
                "-------------------------");
            // while loop allows user to input multiple chemicals
            bool flag = true;
            while (flag)
            {
                // user inputs chemical name
                chemicalName = StringCheck();
                // if chemical name == stop, then the while loop will end
                if (chemicalName == "stop")
                {
                    flag = false;
                }
                else
                {
                    // loop process 5 times to gather more data
                    for (int i = 0; i < 5; i++)
                    {
                        // user inputs number of initial germs
                        Console.WriteLine("Enter the number of intitial germs:");
                        initialGerms = NumCheck(1, 1000);
                        // user inputs number of remaining germs
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
                            flag2 = false;
                        }
                        // if the acidEffect and chemicalName cannot be added to the SortedList, an un
                        catch
                        {
                            acidEffect += 0.000000000001;
                        }
                    }
                }
            }

            int listLength = chemList.Count;
            var threeItemsList = chemList.Take(0);
            bool flag3 = true;
            while (flag3)
            {
                Console.WriteLine("1. Display 3\n" +
                        "2. Display 3\n" +
                        "3. Display entire list\n" +
                        "4. Exit");
                if (listLength <= 3)
                {
                    displayChoice = NumCheck(3, 4);
                }
                else
                {
                    displayChoice = NumCheck(1, 4);
                }

                if (displayChoice >= 1 && displayChoice <= 3)
                {
                    if (displayChoice == 1)
                    {
                        threeItemsList = chemList.Take(3);
                    }
                    else if (displayChoice == 2)
                    {
                        threeItemsList = chemList.Skip(listLength - 3).Take(3);
                    }
                    else
                    {
                        threeItemsList = chemList.Take(listLength);
                    }

                    foreach (var kvp in threeItemsList)
                    {
                        Console.WriteLine($"{kvp.Value}: {Math.Round(kvp.Key, 2)}");
                    }
                }
                else
                {
                    flag3 = false;
                }
            }
        }

        static int NumCheck(int min, int max)
        {
            string ERRORMSG = $"Please enter a number between {min} and {max}";
            int numInput = 0;
            bool flag = true;
            while (flag)
            {
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
            return numInput;
        }
        static string StringCheck()
        {
            string stringInput = "";
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.WriteLine("Enter the chemical name:");
                    stringInput = Console.ReadLine();
                    flag = false;
                }
                catch
                {
                    Console.WriteLine("Please enter a string");
                }
            }
            return stringInput;
        }
    }
}