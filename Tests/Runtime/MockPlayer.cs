using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class MockPlayer
    {
        private readonly string _name;
        private readonly int _level;
        private readonly int _health;
        private readonly int _mana;
        private readonly int _gold;
        private readonly List<MockItem> _inventory = new();

        public string Name => _name;
        public int Level => _level;
        public int Health => _health;
        public int Mana => _mana;
        public int Gold => _gold;
        public IReadOnlyList<MockItem> Inventory => _inventory;

        public MockPlayer(string name, int level, int health, int mana, int gold, IEnumerable<MockItem> items = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace", nameof(name));

            _name = name;
            _level = level;
            _health = health;
            _mana = mana;
            _gold = gold;

            if (items == null)
                return;

            _inventory.AddRange(items);
        }
    }
}
