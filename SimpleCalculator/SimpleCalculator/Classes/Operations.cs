using SimpleCalculator.Interfaces;
using SimpleCalculator.Constants;

namespace SimpleCalculator.Classes;

public class Operations(ICustomMath customMath) : IOperations
{
    public double Calculate(double x, double y, char op)
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
}