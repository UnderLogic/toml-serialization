using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal struct PlayerLocation
    {
        private int _map;
        private int _x;
        private int _y;
        private int _zIndex;

        public int Map
        {
            get => _map;
            set => _map = value;
        }

        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public int ZIndex
        {
            get => _zIndex;
            set => _zIndex = value;
        }

        public PlayerLocation(int map, int x, int y, int zIndex = 1) : this()
        {
            Map = map;
            X = x;
            Y = y;
            ZIndex = zIndex;
        }
    }
}
