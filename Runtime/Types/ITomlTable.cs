using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Types
{
    internal interface ITomlTable : IEnumerable<TomlKeyValuePair>
    {
        void AddTomlValue(string key, TomlValue value);
    }
}
