using System;
using System.Text;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class MockPlayerData
    {
        private string _displayName;
        private int _level;
        private int _experience;
        private int _gold;
        private DateTime _lastLogin;

        public string DisplayName => _displayName;
        public int Level => _level;
        public int Experience => _experience;
        public int Gold => _gold;
        public DateTime LastLogin => _lastLogin;

        public MockPlayerData(string displayName = "Player", int level = 1, int experience = 0, int gold = 0)
        {
            _displayName = displayName;
            _level = level;
            _experience = experience;
            _gold = gold;
            _lastLogin = DateTime.Now;
        }

        public string ToTomlStringTable(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty or whitespace", nameof(key));

            var sb = new StringBuilder();

            sb.AppendLine($"[{key}]");
            sb.AppendLine($"displayName = \"{DisplayName}\"");
            sb.AppendLine($"level = {Level}");
            sb.AppendLine($"experience = {Experience}");
            sb.AppendLine($"gold = {Gold}");
            sb.AppendLine($"lastLogin = {LastLogin:yyyy-MM-dd HH:mm:ss.fffZ}");

            return sb.ToString();
        }
        
        public string ToTomlStringTableInline(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty or whitespace", nameof(key));

            var sb = new StringBuilder();

            sb.Append($"{key} = {{ ");
            sb.Append($"displayName = \"{DisplayName}\"");
            sb.Append(", ");
            sb.Append($"level = {Level}");
            sb.Append(", ");
            sb.Append($"experience = {Experience}");
            sb.Append(", ");
            sb.Append($"gold = {Gold}");
            sb.Append(", ");
            sb.Append($"lastLogin = {LastLogin:yyyy-MM-dd HH:mm:ss.fffZ}");
            sb.AppendLine(" }");

            return sb.ToString();
        }
    }
}
