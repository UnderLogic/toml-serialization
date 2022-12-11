using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class MockDictionary
    {
        public IDictionary<string, int> scores;

        public MockDictionary() => scores = new Dictionary<string, int>();
        public MockDictionary(IDictionary<string, int> dictionary) => scores = dictionary;

        public void Add(string key, int value) => scores.Add(key, value);

        public string ToTomlStringInline(string key = nameof(scores))
        {
            var pairStrings = scores.Select(pair => $"{pair.Key} = {pair.Value}");
            var tableString = string.Join(", ", pairStrings);
            
            return $"{key} = {{{tableString}}}";
        }

        public string ToTomlString(string key = nameof(scores))
        {
            var sb = new StringBuilder();

            sb.AppendLine($"[{key}]");

            foreach (var pair in scores)
                sb.AppendLine($"{pair.Key} = {pair.Value}");

            return sb.ToString();
        }
    }
}
