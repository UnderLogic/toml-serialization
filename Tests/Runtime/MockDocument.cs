using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class MockDocumentArray
    {
        public MockRecord[] records;

        public MockDocumentArray(IEnumerable<MockRecord> collection) => records = collection.ToArray();

        public string ToTomlString(string key = "records")
        {
            var tomlRecords = records.Select(record => record.ToTomlString(key));
            return string.Join("\n", tomlRecords);
        }
    }
    
    [Serializable]
    public class MockDocumentEnumerable
    {
        public IList<MockRecord> records;

        public MockDocumentEnumerable(IEnumerable<MockRecord> collection) => records = collection.ToList();

        public string ToTomlString(string key = "records")
        {
            var tomlRecords = records.Select(record => record.ToTomlString(key));
            return string.Join("\n", tomlRecords);
        }
    }

    [Serializable]
    public class MockRecord
    {
        public int id = 1;
        public string name = "Test";
        public float x;
        public float y;
        public float z;
        public bool enabled = true;
        public DateTime createdAt = DateTime.Now;

        public string ToTomlString(string key)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[[{key}]]");
            sb.AppendLine($"id = {id}");
            sb.AppendLine($"name = \"{name}\"");
            sb.AppendLine($"x = {x}");
            sb.AppendLine($"y = {y}");
            sb.AppendLine($"z = {z}");
            sb.AppendLine($"enabled = {enabled.ToString().ToLowerInvariant()}");
            sb.AppendLine($"createdAt = {createdAt:yyyy-MM-ddTHH:mm:ss.fffZ}");
            
            return sb.ToString();
        }
    }
}
