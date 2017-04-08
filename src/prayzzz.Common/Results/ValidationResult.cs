using System.Collections.Generic;
using System.Linq;

namespace prayzzz.Common.Results
{
    public class ValidationResult
    {
        private readonly List<string> _messages;

        public ValidationResult()
        {
            _messages = new List<string>();
        }

        public bool HasErrors => _messages.Any();

        public void Add(string message)
        {
            _messages.Add(message);
        }

        public ValidationResult Merge(ValidationResult other)
        {
            _messages.AddRange(other._messages);

            return this;
        }

        public override string ToString()
        {
            return string.Join("\r\n", _messages);
        }
    }
}