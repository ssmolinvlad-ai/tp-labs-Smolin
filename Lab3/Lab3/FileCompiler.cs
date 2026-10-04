namespace Lab3;

public enum CopyOutcome { Copied, Overwritten, Cancelled }

public static class FileCopier
{
    /// <summary>
    /// Копирует файл в папку назначения под новым именем.
    /// confirmOverwrite получает путь существующего файла и возвращает true, если перезапись разрешена.
    /// </summary>
    public static CopyOutcome Copy(
        string sourceFile,
        string targetFolder,
        string newName,
        Func<string, bool> confirmOverwrite)
    {
        // 1. Исходный файл
        if (string.IsNullOrWhiteSpace(sourceFile))
            throw new ArgumentException("Путь к исходному файлу не указан.");
        if (!File.Exists(sourceFile))
            throw new FileNotFoundException("Исходный файл не найден.", sourceFile);

        // 2. Папка назначения (создавать её нельзя!)
        if (string.IsNullOrWhiteSpace(targetFolder))
            throw new ArgumentException("Путь к папке назначения не указан.");
        if (!Directory.Exists(targetFolder))
            throw new DirectoryNotFoundException(
                $"Папка назначения не существует: {targetFolder}. Копирование в несуществующую папку запрещено.");

        // 3. Новое имя
        newName = (newName ?? "").Trim();
        if (newName.Length == 0 || newName is "." or "..")
            throw new ArgumentException("Новое имя файла не указано.");
        if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            throw new ArgumentException("Имя файла содержит недопустимые символы.");

        // 4. Итоговый путь
        string targetPath = Path.Combine(targetFolder, newName);

        // 5. Защита от копирования файла самого в себя
        var comparison = OperatingSystem.IsWindows()
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;
        if (string.Equals(Path.GetFullPath(sourceFile), Path.GetFullPath(targetPath), comparison))
            throw new IOException("Исходный файл и файл назначения совпадают.");

        if (Directory.Exists(targetPath))
            throw new IOException("В папке назначения уже есть каталог с таким именем.");

        // 6. Подтверждение перезаписи
        bool exists = File.Exists(targetPath);
        if (exists && !confirmOverwrite(targetPath))
            return CopyOutcome.Cancelled;

        // 7. Копирование
        File.Copy(sourceFile, targetPath, overwrite: exists);
        return exists ? CopyOutcome.Overwritten : CopyOutcome.Copied;
    }
}