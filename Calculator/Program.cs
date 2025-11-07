using CalculatorLibrary;
using System;
using System.Collections.Generic;
namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            bool skipNum1 = false;
            int counter = 0;
            double num1Placeholder = 0;

            List<Calculation> history = new List<Calculation>();

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine($"------------------------\n");

            Calculator calculator = new Calculator();

            while (!endApp)
            {
                // Declare variables and set to empty.
                string? numInput1 = "";
                double cleanNum1 = 0;
                string? numInput2 = "";
                double result = 0;

                if (!skipNum1)
                {
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                }
                else
                {
                    cleanNum1 = num1Placeholder;
                    Console.WriteLine($"First number is {num1Placeholder}");
                    skipNum1 = false;
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\te - Exponent");
                Console.WriteLine("\tt - 10x\n");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("\nYour result: {0:0.##}\n", result);
                        Calculation calculation = new Calculation(cleanNum1, cleanNum2, result, op, counter);
                        history.Add(calculation);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                counter++;
                if (counter == 1) Console.WriteLine($"{counter} calculation completed this session\n");
                else Console.WriteLine($"{counter} calculations completed this session\n");

                // Wait for the user to respond before closing.
                Console.WriteLine("Press 'n' and Enter to close the app\n");
                Console.WriteLine("Press 'h' to view the last 5 calculations\n");
                Console.Write("or press any other key and Enter to continue: ");
                string? userInput = Console.ReadLine();
                if (userInput == "n") endApp = true;
                if (userInput == "h")
                {
                    double? selectedResult = null;
                    selectedResult = calculator.ViewHistory(history);
                    if (selectedResult != null)
                    {
                        num1Placeholder = (double)selectedResult;
                        skipNum1 = true;
                    }
                }

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}
