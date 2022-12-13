using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal struct MockCoord
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;

        public float X => _x;
        public float Y => _y;
        public float Z => _z;

        public MockCoord(float x, float y, float z = 0)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }
}
