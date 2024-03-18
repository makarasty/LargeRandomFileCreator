namespace LargeFileCreatorNamespace;

class Program
{
	static async Task Main()
	{
		string filePath = "ShadowOfInsight.txt";

		/*
		 * 1000000000 lines
		 * 1000 min
		 * 100000 max
		 * ~2min ~7Gb
		 */
		int numberOfLines = MyInputV2Int("Кількість рядків: ");
		int minValue = MyInputV2Int("Мінімальне значення: ");
		int maxValue = MyInputV2Int("Максимальне значення: ");

		Console.WriteLine("Створюємо файл...");

		try
		{
			await WriteRandomNumbersToFileAsync(filePath, numberOfLines, minValue, maxValue + 1);
			Console.WriteLine($"Файл ({filePath}) успішно записаний!");

			await DisplayFileContentAsync(filePath);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Помилка при записі файлу: {ex.Message}");
		}
	}

	private static async Task WriteRandomNumbersToFileAsync(string filePath, int numberOfLines, int minValue, int maxValue)
	{
		await using var writer = new StreamWriter(filePath);

		Random random = new();

		for (int i = 0; i < numberOfLines; i++)
		{
			int randomNumber = random.Next(minValue, maxValue);
			await writer.WriteLineAsync(randomNumber.ToString());
		}
	}

	private static async Task DisplayFileContentAsync(string filePath)
	{
		using var reader = new StreamReader(filePath);

		Console.WriteLine("Вміст файлу:");
		while (!reader.EndOfStream)
		{
			string line = await reader.ReadLineAsync() ?? string.Empty;
			Console.WriteLine(line);
		}
	}

	private static int MyInputV2Int(string text)
	{
		while (true)
		{
			Console.Write(text);
			if (int.TryParse(Console.ReadLine(), out int result))
			{
				return result;
			}
			else
			{
				Console.WriteLine("Невірний формат числа. Спробуйте ще раз.");
			}
		}
	}
}