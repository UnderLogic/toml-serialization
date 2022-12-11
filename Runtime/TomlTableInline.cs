using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlTableInline : TomlValue
    {
        private readonly IDictionary<string, TomlValue> _table = new Dictionary<string, TomlValue>();

        public TomlTableInline() { }

        public TomlTableInline(IEnumerable<TomlKeyValuePair> keyValuePairs)
        {
            if (keyValuePairs == null)
                throw new ArgumentNullException(nameof(keyValuePairs));

            foreach (var pair in keyValuePairs)
                _table.Add(pair.Key, pair.Value);
        }

        public override string ToTomlString()
        {
            var keyPairStrings = _table.Select(pair => $"{pair.Key} = {pair.Value.ToTomlString()}");
            return $"{string.Join(", ", keyPairStrings)}";
        }
    }
}
