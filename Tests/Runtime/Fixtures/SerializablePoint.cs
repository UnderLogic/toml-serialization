using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal struct SerializablePoint
    {
        private float _x;
        private float _y;
        private float _z;

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

        public SerializablePoint(float x, float y, float z = 0)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public override string ToString() => $"X = {X}, Y = {Y}, Z = {Z}";
    }
}
