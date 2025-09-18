namespace CalculatorCore
{
    public class CalculatorResult
    {
        public bool Success { get; set; }
        public decimal Value { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public static CalculatorResult Ok(decimal value) =>
            new CalculatorResult { Success = true, Value = value };

        public static CalculatorResult Fail(string error) =>
            new CalculatorResult { Success = false, ErrorMessage = error };
    }
}