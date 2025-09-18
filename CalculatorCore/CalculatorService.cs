namespace CalculatorCore
{
    public class CalculatorService
    {
        private readonly ICalculator _calculator;

        public CalculatorService(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public CalculatorResult PerformOperation(string operation, decimal a, decimal b)
        {
            try
            {
                return operation.ToLower() switch
                {
                    "add" or "+" => CalculatorResult.Ok(_calculator.Add(a, b)),
                    "subtract" or "-" => CalculatorResult.Ok(_calculator.Subtract(a, b)),
                    "multiply" or "*" => CalculatorResult.Ok(_calculator.Multiply(a, b)),
                    "divide" or "/" => _calculator.TryDivide(a, b, out var result)
                        ? CalculatorResult.Ok(result)
                        : CalculatorResult.Fail("Division by zero"),
                    _ => CalculatorResult.Fail("Unknown operation")
                };
            }
            catch (Exception ex)
            {
                return CalculatorResult.Fail($"Error: {ex.Message}");
            }
        }
    }
}