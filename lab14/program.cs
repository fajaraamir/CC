using System;
using System.Collections.Generic;
using System.Linq;

class Operator
{
    public char Symbol;
    public int Location;
    public int Precedence;
}

class Program
{
    static int GetPrecedence(char op)
    {
        return op switch
        {
            '*' or '/' => 2,
            '+' or '-' => 1,
            _ => 0
        };
    }

    static bool IsOperator(char c)
    {
        return "+-*/".Contains(c);
    }

    static void Main()
    {
        Console.Write("Enter an expression: ");
        string expr = Console.ReadLine();

        List<Operator> operators = new List<Operator>();

        // Step 1: Collect Operators
        for (int i = 0; i < expr.Length; i++)
        {
            if (IsOperator(expr[i]))
            {
                operators.Add(new Operator
                {
                    Symbol = expr[i],
                    Location = i,
                    Precedence = GetPrecedence(expr[i])
                });
            }
        }

        // Step 2: Show Operators
        Console.WriteLine("\nOperators:");
        Console.WriteLine("Operator\tLocation");
        foreach (var op in operators)
        {
            Console.WriteLine($"{op.Symbol}\t\t{op.Location}");
        }

        // Step 3: Sort by Precedence
        var sortedOps = operators.OrderByDescending(op => op.Precedence).ToList();

        Console.WriteLine("\nOperators sorted in their precedence:");
        Console.WriteLine("Operator\tLocation");
        foreach (var op in sortedOps)
        {
            Console.WriteLine($"{op.Symbol}\t\t{op.Location}");
        }

        // Step 4: Generate TAC
        Dictionary<int, string> tempVars = new Dictionary<int, string>();
        int tempCount = 1;

        foreach (var op in sortedOps)
        {
            int i = op.Location;
            string left = tempVars.ContainsKey(i - 1) ? tempVars[i - 1] : expr[i - 1].ToString();
            string right = tempVars.ContainsKey(i + 1) ? tempVars[i + 1] : expr[i + 1].ToString();
            string result = $"t{tempCount++}";

            Console.WriteLine($"{result} = {left} {op.Symbol} {right}");

            // Save to both left, op, and right for future reuse
            tempVars[i - 1] = result;
            tempVars[i] = result;
            tempVars[i + 1] = result;
        }
    }
}

