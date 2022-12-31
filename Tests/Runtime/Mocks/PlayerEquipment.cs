using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class PlayerEquipment
    {
        [TomlLiteral]
        [TomlExpand]
        private Dictionary<string, string> _equipment = new();

        public IReadOnlyDictionary<string, string> Equipment => _equipment;

        public void EquipItem(string slot, string item)
        {
            if (string.IsNullOrWhiteSpace(slot))
                throw new ArgumentException("Slot cannot be null or whitespace.", nameof(slot));

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException("Item cannot be null or whitespace.", nameof(item));

            _equipment[slot] = item;
        }
    }
}
