using CalculatorCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        private readonly Calculator _calculator = new Calculator();

        // ТЕСТЫ СЛОЖЕНИЯ (ADD)
        [TestMethod]
        [DataRow(5, 3, 8)]
        [DataRow(-2, 3, 1)]
        [DataRow(0, 0, 0)]
        [DataRow(-5, -3, -8)]
        [DataRow(0, 5, 5)]
        public void Add_ShouldReturnCorrectResult(int a, int b, int expected)
        {
            var result = _calculator.Add(a, b);

            Assert.AreEqual(expected, result);
        }

        // ТЕСТЫ ВЫЧИТАНИЯ (SUBTRACT)
        [TestMethod]
        [DataRow(5, 3, 2)]
        [DataRow(3, 5, -2)]
        [DataRow(0, 0, 0)]
        [DataRow(-5, -3, -2)]
        [DataRow(10, 0, 10)]
        public void Subtract_ShouldReturnCorrectResult(int a, int b, int expected)
        {
            var result = _calculator.Subtract(a, b);

            Assert.AreEqual(expected, result);
        }

        // ТЕСТЫ УМНОЖЕНИЯ (MULTIPLY)
        [TestMethod]
        [DataRow(5, 3, 15)]
        [DataRow(-2, 3, -6)]
        [DataRow(0, 5, 0)]
        [DataRow(-3, -4, 12)]
        [DataRow(1, 1, 1)]
        public void Multiply_ShouldReturnCorrectResult(int a, int b, int expected)
        {
            var result = _calculator.Multiply(a, b);

            Assert.AreEqual(expected, result);
        }

        // ТЕСТЫ ДЕЛЕНИЯ (DIVIDE)
        [TestMethod]
        [DataRow(10, 2, 5)]
        [DataRow(9, 3, 3)]
        [DataRow(-12, 4, -3)]
        [DataRow(0, 5, 0)]
        [DataRow(1, 4, 0.25)]
        public void Divide_ShouldReturnCorrectResult(int a, int b, double expected)
        {
            var result = _calculator.Divide(a, b);

            Assert.AreEqual((decimal)expected, result);
        }

        // ТЕСТ ИСКЛЮЧЕНИЯ ПРИ ДЕЛЕНИИ НА НОЛЬ
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_ByZero_ShouldThrowDivideByZeroException()
        {
            decimal a = 10;
            decimal b = 0;

            _calculator.Divide(a, b);
        }

        // ТЕСТЫ МЕТОДА TRYDIVIDE (УСПЕШНЫЕ СЦЕНАРИИ)
        [TestMethod]
        [DataRow(10, 2, true, 5)]
        [DataRow(9, 3, true, 3)]
        [DataRow(0, 5, true, 0)]
        public void TryDivide_ValidDivision_ShouldReturnTrueAndCorrectResult(int a, int b, bool expectedSuccess, int expectedResult)
        {
            var success = _calculator.TryDivide(a, b, out var result);

            Assert.AreEqual(expectedSuccess, success);
            Assert.AreEqual(expectedResult, result);
        }

        // ТЕСТЫ МЕТОДА TRYDIVIDE (ДЕЛЕНИЕ НА НОЛЬ)
        [TestMethod]
        [DataRow(10, 0, false, 0)]
        [DataRow(5, 0, false, 0)]
        [DataRow(-3, 0, false, 0)]
        public void TryDivide_DivisionByZero_ShouldReturnFalse(int a, int b, bool expectedSuccess, int expectedResult)
        {
            var success = _calculator.TryDivide(a, b, out var result);

            Assert.AreEqual(expectedSuccess, success);
            Assert.AreEqual(expectedResult, result);
        }

        // Отдельные тесты для decimal значений
        [TestMethod]
        public void Add_DecimalNumbers_ShouldReturnCorrectResult()
        {
            decimal a = 1.5m;
            decimal b = 2.5m;
            decimal expected = 4.0m;

            var result = _calculator.Add(a, b);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Subtract_DecimalNumbers_ShouldReturnCorrectResult()
        {
            // Тест вычитания decimal чисел
            decimal a = 5.5m;
            decimal b = 2.5m;
            decimal expected = 3.0m;

            var result = _calculator.Subtract(a, b);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiply_DecimalNumbers_ShouldReturnCorrectResult()
        {
            // Тест умножения decimal чисел
            decimal a = 2.5m;
            decimal b = 4.0m;
            decimal expected = 10.0m;

            var result = _calculator.Multiply(a, b);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Divide_DecimalNumbers_ShouldReturnCorrectResult()
        {
            // Тест деления decimal чисел
            decimal a = 7.0m;
            decimal b = 2.0m;
            decimal expected = 3.5m;

            var result = _calculator.Divide(a, b);

            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class CalculatorServiceTests
    {
        // Тестовый калькулятор для сервиса
        private class TestCalculator : ICalculator
        {
            public decimal Add(decimal a, decimal b) => a + b;
            public decimal Subtract(decimal a, decimal b) => a - b;
            public decimal Multiply(decimal a, decimal b) => a * b;
            public decimal Divide(decimal a, decimal b) => a / b;

            public bool TryDivide(decimal a, decimal b, out decimal result)
            {
                if (b == 0)
                {
                    result = 0;
                    return false;
                }
                result = a / b;
                return true;
            }
        }

        private readonly CalculatorService _calculatorService;

        public CalculatorServiceTests()
        {
            _calculatorService = new CalculatorService(new TestCalculator());
        }

        // ТЕСТЫ ВЫПОЛНЕНИЯ ОПЕРАЦИЙ ЧЕРЕЗ СЕРВИС
        [TestMethod]
        [DataRow("add", 5, 3, 8)]
        [DataRow("ADD", 5, 3, 8)]
        [DataRow("+", 5, 3, 8)]
        [DataRow("subtract", 5, 3, 2)]
        [DataRow("SUBTRACT", 5, 3, 2)]
        [DataRow("-", 5, 3, 2)]
        [DataRow("multiply", 5, 3, 15)]
        [DataRow("MULTIPLY", 5, 3, 15)]
        [DataRow("*", 5, 3, 15)]
        [DataRow("divide", 10, 2, 5)]
        [DataRow("DIVIDE", 10, 2, 5)]
        [DataRow("/", 10, 2, 5)]
        public void PerformOperation_ValidOperations_ShouldReturnSuccessResult(string operation, int a, int b, int expected)
        {
            var result = _calculatorService.PerformOperation(operation, a, b);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(expected, result.Value);
            Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));
        }

        // ТЕСТЫ ДЕЛЕНИЯ НА НОЛЬ ЧЕРЕЗ СЕРВИС
        [TestMethod]
        [DataRow("divide", 10, 0)]
        [DataRow("DIVIDE", 10, 0)]
        [DataRow("/", 10, 0)]
        public void PerformOperation_DivideByZero_ShouldReturnFailureResult(string operation, int a, int b)
        {
            var result = _calculatorService.PerformOperation(operation, a, b);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Division by zero", result.ErrorMessage);
            Assert.AreEqual(0, result.Value);
        }

        // ТЕСТЫ НЕИЗВЕСТНЫХ ОПЕРАЦИЙ
        [TestMethod]
        [DataRow("unknown", 5, 3)]
        [DataRow("power", 2, 3)]
        [DataRow("mod", 10, 3)]
        public void PerformOperation_UnknownOperation_ShouldReturnFailureResult(string operation, int a, int b)
        {
            var result = _calculatorService.PerformOperation(operation, a, b);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Unknown operation", result.ErrorMessage);
            Assert.AreEqual(0, result.Value);
        }

        // ТЕСТ DECIMAL ЧИСЕЛ ЧЕРЕЗ СЕРВИС
        [TestMethod]
        public void PerformOperation_DecimalNumbers_ShouldReturnCorrectResult()
        {
            // Тест сложения decimal чисел через сервис
            string operation = "add";
            decimal a = 1.5m;
            decimal b = 2.5m;
            decimal expected = 4.0m;

            var result = _calculatorService.PerformOperation(operation, a, b);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(expected, result.Value);
        }

        // ТЕСТ ПУСТОЙ ОПЕРАЦИИ
        [TestMethod]
        public void PerformOperation_EmptyOperation_ShouldReturnFailureResult()
        {
            var result = _calculatorService.PerformOperation("", 1, 2);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Unknown operation", result.ErrorMessage);
        }
    }

    [TestClass]
    public class CalculatorResultTests
    {
        // ТЕСТЫ ДЛЯ КЛАССА CalculatorResult
        [TestMethod]
        public void CalculatorResult_Ok_ShouldCreateSuccessResult()
        {
            // Тест создания успешного результата
            decimal expectedValue = 42;

            var result = CalculatorResult.Ok(expectedValue);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));
        }

        [TestMethod]
        public void CalculatorResult_Fail_ShouldCreateFailureResult()
        {
            // Тест создания неуспешного результата с сообщением
            string expectedError = "Test error";

            var result = CalculatorResult.Fail(expectedError);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(expectedError, result.ErrorMessage);
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod]
        public void CalculatorResult_Fail_WithEmptyMessage_ShouldWorkCorrectly()
        {
            // Тест создания неуспешного результата с пустым сообщением
            var result = CalculatorResult.Fail("");

            Assert.IsFalse(result.Success);
            Assert.AreEqual("", result.ErrorMessage);
        }

        [TestMethod]
        public void CalculatorResult_Fail_WithNullMessage_ShouldWorkCorrectly()
        {
            // Тест создания неуспешного результата с null сообщением
            var result = CalculatorResult.Fail(null);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.ErrorMessage);
        }
    }
}