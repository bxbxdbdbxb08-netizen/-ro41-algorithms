using System;
using System.Text;

public static class TextTools
{
    // 1. Подсчет количества слов (без Regex и LINQ)
    public static int CountWords(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        int count = 0;
        bool inWord = false;

        foreach (char c in s)
        {
            if (!char.IsWhiteSpace(c))
            {
                if (!inWord)
                {
                    inWord = true;
                    count++;
                }
            }
            else
            {
                inWord = false;
            }
        }

        return count;
    }

    // 2. Проверка на палиндром без учета регистра и пробелов
    public static bool IsPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s)) return true;

        StringBuilder sb = new StringBuilder();
        foreach (char c in s)
        {
            if (!char.IsWhiteSpace(c))
            {
                sb.Append(char.ToLower(c));
            }
        }

        string cleaned = sb.ToString();
        int left = 0;
        int right = cleaned.Length - 1;

        while (left < right)
        {
            if (cleaned[left] != cleaned[right])
            {
                return false;
            }
            left++;
            right--;
        }

        return true;
    }

    // 3. Получение инициалов (например: "Ахметов Ерлан Серикұлы" -> "А. Е. С.")
    public static string Initials(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;

        string[] parts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].Length > 0)
            {
                char firstChar = char.ToUpper(parts[i][0]);
                sb.Append(firstChar).Append(". ");
            }
        }

        if (sb.Length > 0)
        {
            sb.Length--; // Удаляем последний лишний пробел
        }

        return sb.ToString();
    }

    // 4. Повторение строки n раз через StringBuilder
    public static string Repeat(string s, int n)
    {
        if (string.IsNullOrEmpty(s) || n <= 0) return string.Empty;

        StringBuilder sb = new StringBuilder(s.Length * n);
        for (int i = 0; i < n; i++)
        {
            sb.Append(s);
        }

        return sb.ToString();
    }
}

class Program
{
    static void Main()
    {
        string[] testStrings = {
            "",
            "    ",
            " a  b ",
            "А роза упала на лапу Азора",
            "Ахметов Ерлан"
        };

        Console.WriteLine("=== Тестирование методов TextTools ===");

        foreach (var s in testStrings)
        {
            Console.WriteLine($"\nВходная строка: \"{s}\"");

            // CountWords
            int expectedWords = GetExpectedWords(s);
            int actualWords = TextTools.CountWords(s);
            PrintTestResult("CountWords", expectedWords, actualWords);

            // IsPalindrome
            bool expectedPal = GetExpectedPalindrome(s);
            bool actualPal = TextTools.IsPalindrome(s);
            PrintTestResult("IsPalindrome", expectedPal, actualPal);

            // Initials
            string expectedInit = GetExpectedInitials(s);
            string actualInit = TextTools.Initials(s);
            PrintTestResult("Initials", $"\"{expectedInit}\"", $"\"{actualInit}\"");
        }

        // Проверка метода Repeat
        Console.WriteLine("\n--- Проверка Repeat ---");
        string repInput = "ab";
        int repN = 3;
        string expectedRep = "ababab";
        string actualRep = TextTools.Repeat(repInput, repN);
        Console.WriteLine($"Repeat(\"{repInput}\", {repN}) -> Ожидалось: \"{expectedRep}\", Фактически: \"{actualRep}\" -> {(expectedRep == actualRep ? "OK" : "FAIL")}");
    }

    static int GetExpectedWords(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;
        string[] parts = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return parts.Length;
    }
