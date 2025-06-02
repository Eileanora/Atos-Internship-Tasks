using SimpleCalculator.Constants;
using SimpleCalculator.Interfaces;

namespace SimpleCalculator.Classes;

public class CustomMath : ICustomMath
{
    public double Add(double x, double y) => x + y;
    public double Subtract(double x, double y) => x - y;
    public double Multiply(double x, double y)
    {
        if (x != 0 && y != 0)
        {
            if (Math.Abs(x) > double.MaxValue / Math.Abs(y))
                throw new OverflowException(ErrorMsgs.OverflowError);
        }
        return x * y;
    }
    public double Power(double x, double y)
    {
        double result = Math.Pow(x, y);
        if (double.IsInfinity(result))
        {
            throw new OverflowException(ErrorMsgs.OverflowError);
        }
        return result;
    }
    public double Divide(double x, double y)
    {
        if (y == 0)
            throw new DivideByZeroException(ErrorMsgs.DivideZero);
        return x / y;
    }
    public double Modulus(double x, double y)
    {
        if (y == 0)
            throw new DivideByZeroException(ErrorMsgs.DivideZero);
        return x % y;
    }
}
