using SimpleCalculator.Constants;
using SimpleCalculator.Interfaces;

namespace SimpleCalculator.Classes;

public class InputValidators : IInputValidators
{
    public string ValidateInputNumber(string number)
    {
        var msg="";
        if (!double.TryParse(number, out _))
        {
            msg= ErrorMsgs.InvalidInputNumber;
        }
        return msg;
    }
    public string ValidateOperator(string op)
    {
        var msg="";
        if ( ConstantVars.Operators.IndexOf(op) < 0)
        {
            msg= ErrorMsgs.InvalidOperator;
        }
        return msg;
    }
    public string ValidateInput(string input){
        var parts = input.Split(" ");
        var msg="";
        if (parts.Length != 3)
        {
            msg = ErrorMsgs.InvalidInputEquation;
        }
        return msg;
    }
}