using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class MockNestedItem
    {
        private int _id = 1;
        private string _key = "item_key";
        private string _displayName = "Item";
        private bool _identified;
        private float _weight = 1f;
        private int _quantity = 1;
        private int _maxQuantity = 1;

        private readonly Dictionary<string, float> _modifiers = new();
        
        public int Id
        {
            get => _id;
            set => _id = value;
        }
        
        public string Key
        {
            get => _key;
            set => _key = value;
        }
        
        public string DisplayName
        {
            get => _displayName;
            set => _displayName = value;
        }
        
        public bool Identified
        {
            get => _identified;
            set => _identified = value;
        }
        
        public float Weight
        {
            get => _weight;
            set => _weight = value;
        }
        
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }
        
        public int MaxQuantity
        {
            get => _maxQuantity;
            set => _maxQuantity = value;
        }

        public IReadOnlyDictionary<string, float> Modifiers => _modifiers;

        public void AddModifier(string modifier, float value)
        {
            if (_modifiers.TryGetValue(modifier, out var existingValue))
                value = existingValue + value;

            _modifiers[modifier] = value;
        }
    }
}
