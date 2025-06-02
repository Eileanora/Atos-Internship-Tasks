// Simple Calculator
using SimpleCalculator.Classes;
using SimpleCalculator.Constants;
using SimpleCalculator.Interfaces;


IInputValidators inputValidators = new InputValidators();
ICustomMath customMath = new CustomMath();

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
        var validationMsg = ValidateAll(parts);
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
            double result = Calculate(number1, number2, op);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine(ConstantMsgs.InputMessage);
        }
        catch (OverflowException ex)
        {
            Console.WriteLine(ErrorMsgs.OverflowError);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ErrorMsgs.DivideZero);
        }
    }
    
    if (exitCalculator)
    {
        Console.WriteLine(ConstantMsgs.GoodByeMessage);
        break;
    }
}
string ValidateAll(List<string> parts)
{
    string errorMsg = "";
    
    var validators = new Validator[]{
        inputValidators.ValidateInput,
        inputValidators.ValidateInputNumber,
        inputValidators.ValidateOperator,
        inputValidators.ValidateInputNumber};
    
    var msg = "";
    for(var i = 0; i < validators.Length; i++){
        msg = validators[i](parts[i]);
        if(!string.IsNullOrEmpty(msg))
        {
            errorMsg = msg;
            break;
        }
    }
    return errorMsg;
}

double Calculate(double x, double y, char op)
{
    return op switch
    {
        '+' => customMath.Add(x, y),
        '-' => customMath.Subtract(x, y),
        '*' => customMath.Multiply(x, y),
        '/' => customMath.Divide(x, y),
        '%' => customMath.Modulus(x, y),
        '^' => customMath.Power(x, y),
        _ => throw new InvalidOperationException(ErrorMsgs.InvalidOperator)
    };
}

delegate string Validator(string input);