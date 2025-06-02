using SimpleCalculator.Interfaces;

namespace SimpleCalculator.Classes;

public class ValidateAll(IInputValidators inputValidators) : IValidateAll
{
    private delegate string Validator(string input);
    public string RunAllValidators(List<string> parts)
    {
        var errorMsg = "";
    
        var validators = new Validator[]{
            inputValidators.ValidateInput,
            inputValidators.ValidateInputNumber,
            inputValidators.ValidateOperator,
            inputValidators.ValidateInputNumber};
    
        for(var i = 0; i < validators.Length; i++){
            var msg = validators[i](parts[i]);
            if(!string.IsNullOrEmpty(msg))
            {
                errorMsg = msg;
                break;
            }
        }
        return errorMsg;
    }
}
