using System;

namespace Lab2
{
    public class ElectronicEdition : Publication
    {
        private string _format;
        private double _fileSizeMb;

        public string Format => _format;
        public double FileSizeMb => _fileSizeMb;

        public ElectronicEdition(string title, string uniqueId, int year, string author,
                                 string format, double fileSizeMb) : base(title, uniqueId, year, author)
        {
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentException("Формат не может быть пустым.", nameof(format));
            if (fileSizeMb <= 0)
                throw new ArgumentOutOfRangeException(nameof(fileSizeMb), "Размер файла должен быть положительным.");

            _format = format;
            _fileSizeMb = fileSizeMb;
        }

        public override string GetPublicationType() => "Электронное издание";

        public override string ToString() =>
            base.ToString() + $", Формат: {Format}, {FileSizeMb:F1} МБ";
    }
}