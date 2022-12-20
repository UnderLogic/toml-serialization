using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class MockSimpleClass
    {
        private int _id;
        private string _name;
        private float _weight = 1f;
        private bool _hidden;
        private DateTime _createdAt = DateTime.Now;

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

        public float Weight
        {
            get => _weight;
            set => _weight = value;
        }
        
        public bool Hidden
        {
            get => _hidden;
            set => _hidden = value;
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }
    }
}
