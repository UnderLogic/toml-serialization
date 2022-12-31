using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class Spellbook
    {
        private string _name;

        [TomlMultiline]
        private List<string> _learnedSpells = new();
        
        [TomlMultiline]
        private List<string> _memorizedSpells = new();
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public IReadOnlyCollection<string> LearnedSpells => _learnedSpells;
        public IReadOnlyCollection<string> MemorizedSpells => _memorizedSpells;

        public void LearnSpell(string spellName)
        {
            if (string.IsNullOrWhiteSpace(spellName))
                throw new ArgumentException("Spell name cannot be null or whitespace", nameof(spellName));
            
            _learnedSpells.Add(spellName);
        }
        
        public void MemorizeSpell(string spellName)
        {
            if (string.IsNullOrWhiteSpace(spellName))
                throw new ArgumentException("Spell name cannot be null or whitespace", nameof(spellName));
            
            _memorizedSpells.Add(spellName);
        }
    }
}
