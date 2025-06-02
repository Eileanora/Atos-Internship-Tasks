namespace SimpleCalculator.Interfaces;

public interface ICustomMath
{
    double Add(double a, double b);
    double Subtract(double a, double b);
    double Multiply(double a, double b);
    double Divide(double a, double b);
    double Modulus(double a, double b);
    double Power(double a, double b);
}