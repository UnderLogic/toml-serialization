using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class InventoryItem
    {
        private int _slot = 1;
        private string _key = "item";
        private string _displayName = "Item";
        private float _weight = 1f;
        private int _amount = 1;
        private int _maxAmount = 1;
        private bool _canUse;
        private bool _canDrop = true;

        public int Slot
        {
            get => _slot;
            set => _slot = value;
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

        public float Weight
        {
            get => _weight;
            set => _weight = value;
        }

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public int MaxAmount
        {
            get => _maxAmount;
            set => _maxAmount = value;
        }

        public bool CanUse
        {
            get => _canUse;
            set => _canUse = value;
        }

        public bool CanDrop
        {
            get => _canDrop;
            set => _canDrop = value;
        }
    }
}
