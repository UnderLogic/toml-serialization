using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public struct MockSimpleStruct
    {
        private int _index;
        private float _x;
        private float _y;
        private float _z;
        
        public int Index
        {
            get => _index;
            set => _index = value;
        }
        
        public float X
        {
            get => _x;
            set => _x = value;
        }
        
        public float Y
        {
            get => _y;
            set => _y = value;
        }
        
        public float Z
        {
            get => _z;
            set => _z = value;
        }
    }
}
