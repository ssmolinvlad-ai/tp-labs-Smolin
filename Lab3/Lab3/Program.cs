using System.Text;
using Lab3;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.WriteLine("=== Копирование файла с переименованием ===");

do
{
    string source = ReadLine("Путь к исходному файлу: ");
    string folder = ReadLine("Папка назначения: ");
    string newName = ReadLine("Новое имя файла (с расширением): ");

    try
    {
        var result = FileCopier.Copy(
            source, folder, newName,
            existingPath => AskYesNo($"Файл «{existingPath}» уже существует. Перезаписать? (д/н): "));

        Console.WriteLine(result switch
        {
            CopyOutcome.Copied => "Файл успешно скопирован.",
            CopyOutcome.Overwritten => "Существующий файл перезаписан.",
            _ => "Операция отменена, файл не изменён."
        });
    }
    // FileNotFoundException и DirectoryNotFoundException наследуют IOException,
    // поэтому их catch-блоки должны стоять ВЫШЕ блока IOException
    catch (FileNotFoundException ex) { Console.WriteLine($"Ошибка: {ex.Message} ({ex.FileName})"); }
    catch (DirectoryNotFoundException ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
    catch (ArgumentException ex) { Console.WriteLine($"Некорректный ввод: {ex.Message}"); }
    catch (NotSupportedException) { Console.WriteLine("Ошибка: путь имеет неподдерживаемый формат."); }
    catch (UnauthorizedAccessException) { Console.WriteLine("Ошибка: нет прав доступа к файлу или папке."); }
    catch (PathTooLongException) { Console.WriteLine("Ошибка: слишком длинный путь."); }
    catch (IOException ex) { Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}"); }

    Console.WriteLine();
}
while (AskYesNo("Выполнить ещё одно копирование? (д/н): "));

// ---------- вспомогательные методы ----------
static string ReadLine(string prompt)
{
    Console.Write(prompt);
    // Trim('"') убирает кавычки, которые добавляет Windows при «Копировать как путь»
    return (Console.ReadLine() ?? "").Trim().Trim('"');
}

static bool AskYesNo(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string answer = (Console.ReadLine() ?? "").Trim().ToLower();
        if (answer is "д" or "да" or "y" or "yes") return true;
        if (answer is "н" or "нет" or "n" or "no") return false;
        Console.WriteLine("Введите «д» или «н».");
    }
}