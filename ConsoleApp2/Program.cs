using System;

class Calculator
{
    private double currentValue = 0;
    
    public void Run()
    {
        Console.WriteLine("Калькулятор");
        Console.WriteLine("Доступные операции: +, -, *, /, %, 1/x, x^2, sqrt, C (очистка), EXIT");
        
        while (true)
        {
            try
            {
                Console.Write($"Текущее значение: {currentValue} > ");
                string input = Console.ReadLine()?.Trim().ToUpper();
                
                if (string.IsNullOrEmpty(input))
                    continue;
                    
                if (input == "EXIT")
                    break;
                    
                if (input == "C")
                {
                    currentValue = 0;
                    continue;
                }
                
                ProcessInput(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
    
    private void ProcessInput(string input)
    {
        switch (input)
        {
            case "1/X":
                if (currentValue == 0)
                    throw new DivideByZeroException("Деление на ноль невозможно");
                currentValue = 1 / currentValue;
                break;
                
            case "X^2":
                currentValue *= currentValue;
                break;
                
            case "SQRT":
                if (currentValue < 0)
                    throw new ArgumentException("Корень из отрицательного числа невозможен");
                currentValue = Math.Sqrt(currentValue);
                break;
                
            default:
                ProcessOperation(input);
                break;
        }
    }
    
    private void ProcessOperation(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length != 2)
            throw new ArgumentException("Неверный формат ввода. Используйте: число операция");
            
        if (!double.TryParse(parts[0], out double number))
            throw new ArgumentException("Неверный числовой формат");
            
        string operation = parts[1];
        
        switch (operation)
        {
            case "+":
                currentValue += number;
                break;
                
            case "-":
                currentValue -= number;
                break;
                
            case "*":
                currentValue *= number;
                break;
                
            case "/":
                if (number == 0)
                    throw new DivideByZeroException("Деление на ноль невозможно");
                currentValue /= number;
                break;
                
            case "%":
                currentValue %= number;
                break;
                
            default:
                throw new ArgumentException($"Неизвестная операция: {operation}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        calculator.Run();
    }
}