namespace CalculatorCore
{
    public interface ICalculator
    {
        decimal Add(decimal a, decimal b);
        decimal Subtract(decimal a, decimal b);
        decimal Multiply(decimal a, decimal b);
        decimal Divide(decimal a, decimal b);
        bool TryDivide(decimal a, decimal b, out decimal result);
    }
}