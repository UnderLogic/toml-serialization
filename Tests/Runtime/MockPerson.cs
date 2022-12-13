using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class MockPerson
    {
        private string _name;
        private int _age;
        private DateTime _dateOfBirth;

        public string Name => _name;
        public int Age => _age;
        public DateTime DateOfBirth => _dateOfBirth;

        public MockPerson(string name, int age, DateTime dateOfBirth)
        {
            _name = name;
            _age = age;
            _dateOfBirth = dateOfBirth;
        }
    }
}
