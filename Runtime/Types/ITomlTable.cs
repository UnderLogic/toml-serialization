using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Types
{
    internal interface ITomlTable : IEnumerable<TomlKeyValuePair>
    {
        void Add(string key, TomlValue value);
    }
}
