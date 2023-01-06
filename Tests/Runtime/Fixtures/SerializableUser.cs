using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableUser
    {
        private string _firstName = "John";
        private string _lastName = "Doe";
        private int _age = 42;
        private float _weight = 180.25f;
        private DateTime _createdDate = DateTime.Now;

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public float Weight
        {
            get => _weight;
            set => _weight = value;
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set => _createdDate = value;
        }

        public override string ToString() =>
            $"FirstName = {FirstName}, LastName = {LastName}, Age = {Age}, Weight = {Weight}, CreatedDate = {CreatedDate}";
    }
}
