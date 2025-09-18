using CalculatorCore;

class Program
{
    static void Main(string[] args)
    {
        var calculator = new Calculator();
        var calculatorService = new CalculatorService(calculator);

        Console.WriteLine("=== Калькулятор ===");
        Console.WriteLine("Доступные операции: +, -, *, /");
        Console.WriteLine("Пример: 5 + 3");
        Console.WriteLine("Введите 'exit' для выхода");
        Console.WriteLine();

        while (true)
        {
            Console.Write("Введите выражение: ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) || input.ToLower() == "exit")
                break;

            try
            {
                var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3)
                {
                    Console.WriteLine("Ошибка: Неверный формат. Используйте: число операция число");
                    continue;
                }

                if (!decimal.TryParse(parts[0], out var a) || !decimal.TryParse(parts[2], out var b))
                {
                    Console.WriteLine("Ошибка: Неверный формат чисел");
                    continue;
                }

                var operation = parts[1];
                var result = calculatorService.PerformOperation(operation, a, b);

                if (result.Success)
                    Console.WriteLine($"Результат: {result.Value}");
                else
                    Console.WriteLine($"Ошибка: {result.ErrorMessage}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Работа завершена. Нажмите любую клавишу...");
        Console.ReadKey();
    }
}