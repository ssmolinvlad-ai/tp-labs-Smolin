using System;

namespace Lab2
{
    public class Magazine : Publication
    {
        private int _issueNumber;
        private string _publisher;
        public int IssueNumber;
        public string Publisher;

        public Magazine(string title, string uniqueId, int year, string author,
                        int issueNumber, string publisher) : base(title, uniqueId, year, author)
        {
            if (issueNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(issueNumber), "Номер выпуска должен быть положительным.");
            if (string.IsNullOrWhiteSpace(publisher))
                throw new ArgumentException("Издатель не может быть пустым.", nameof(publisher));

            _issueNumber = issueNumber;
            _publisher = publisher;
        }

        public override string GetPublicationType() => "Журнал";

        public override string ToString() =>
            base.ToString() + $", Выпуск: {IssueNumber}, Издатель: {Publisher}";
    }
}