using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class MockItem
    {
        private string _key;
        private string _displayName;
        private int _quantity;
        private int _maxQuantity;

        public string Key => _key;
        public string DisplayName => _displayName;
        public int Quantity => _quantity;
        public int MaxQuantity => _maxQuantity;

        public MockItem(string key, string displayName, int quantity = 1, int maxQuantity = 1)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace", nameof(key));

            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Display name cannot be null or whitespace", nameof(displayName));

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero");

            if (maxQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxQuantity), "Max quantity must be greater than zero");

            if (quantity > maxQuantity)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be greater than max quantity");

            _key = key;
            _displayName = displayName;
            _quantity = quantity;
            _maxQuantity = maxQuantity;
        }
    }
}
