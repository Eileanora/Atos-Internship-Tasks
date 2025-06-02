// Simple Calculator
using SimpleCalculator.Classes;
using SimpleCalculator.Constants;
using SimpleCalculator.Interfaces;

IValidateAll validateAll = new ValidateAll(new InputValidators());
IOperations operations = new Operations(new CustomMath());

while (true)
{
    var exitCalculator = false;
    
    Console.WriteLine(ConstantMsgs.WelcomeMsg);
    Console.WriteLine(ConstantMsgs.Instructions);
    Console.WriteLine(ConstantMsgs.InputMessage);
    while(true)
    {
        var trimmedInput = (Console.ReadLine() ?? "").Trim().ToLower();
        
        // check for commands in a diff method
        if (trimmedInput == "bye")
        {
            exitCalculator = true;
            break;
        }

        if (trimmedInput == "clear")
        {
            Console.Clear();
            continue;
        }
        
        var parts = trimmedInput.Split(" ").ToList();
        parts.Insert(0, trimmedInput);
        var validationMsg = validateAll.RunAllValidators(parts);
        if (validationMsg != "")
        {
            Console.WriteLine(validationMsg);
            continue;
        }
        
        double number1 = double.Parse(parts[1]);
        double number2 = double.Parse(parts[3]);
        char op = char.Parse(parts[2]);
        try
        {
            double result = operations.Calculate(number1, number2, op);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine(ConstantMsgs.InputMessage);
        }
        catch (OverflowException ex) // configure an ILogger to log exceptions for developer use
        {
            Console.WriteLine(ErrorMsgs.OverflowError);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ErrorMsgs.DivideZero);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ErrorMsgs.GeneralError);
        }
    }
    
    if (exitCalculator)
    {
        Console.WriteLine(ConstantMsgs.GoodByeMessage);
        break;
    }
}
