using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class InventoryItem
    {
        private int _id = 1;
        private string _name = "item";
        private string _displayName = "Item";
        private int _quantity = 1;
        private int _maxQuantity = 1;
        private bool _canDrop = true;

        public int Id
        {
            get => _id;
            set => _id = value;
        }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public string DisplayName
        {
            get => _displayName;
            set => _displayName = value;
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
        
        public bool CanDrop
        {
            get => _canDrop;
            set => _canDrop = value;
        }
    }
}
