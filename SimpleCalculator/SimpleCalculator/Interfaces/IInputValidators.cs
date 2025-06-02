namespace SimpleCalculator.Interfaces;

public interface IInputValidators
{
    string ValidateInput(string input);
    string ValidateOperator(string op);
    string ValidateInputNumber(string number);
}
