using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class Player
    {
        private string _name = "NoName";
        private string _title = string.Empty;
        private PlayerClass _class;
        private int _level = 1;
        private long _experience;
        private long _gold;

        private PlayerLocation _location;
        private Direction _facing = Direction.Down;

        private PlayerStats _stats = new ();
        private int _statPoints;

        private StatusEffects _status;

        private List<InventoryItem> _inventory = new();

        private bool _isBanned;
        private DateTime _createdAt;
        private DateTime _lastLoginAt;
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public string Title
        {
            get => _title;
            set => _title = value;
        }
        
        public PlayerClass Class
        {
            get => _class;
            set => _class = value;
        }
        
        public int Level
        {
            get => _level;
            set => _level = value;
        }
        
        public long Experience
        {
            get => _experience;
            set => _experience = value;
        }
        
        public long Gold
        {
            get => _gold;
            set => _gold = value;
        }
        
        public PlayerLocation Location
        {
            get => _location;
            set => _location = value;
        }
        
        public Direction Facing
        {
            get => _facing;
            set => _facing = value;
        }
        
        public PlayerStats Stats
        {
            get => _stats;
            set => _stats = value;
        }
        
        public int StatPoints
        {
            get => _statPoints;
            set => _statPoints = value;
        }
        
        public StatusEffects Status
        {
            get => _status;
            set => _status = value;
        }

        public IReadOnlyList<InventoryItem> Inventory => _inventory;

        public bool IsBanned
        {
            get => _isBanned;
            set => _isBanned = value;
        }
        
        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }
        
        public DateTime LastLoginAt
        {
            get => _lastLoginAt;
            set => _lastLoginAt = value;
        }

        public void AddInventoryItem(InventoryItem item)
        {
            if (!_inventory.Contains(item))
                _inventory.Add(item);
        }
    }
}
