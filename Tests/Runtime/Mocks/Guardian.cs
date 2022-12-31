using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class Guardian
    {
        private string _name = "Guard";
        private float _aggroRadius = 1f;
        
        [TomlInline]
        private Dictionary<string, PlayerLocation> _waypoints = new();
        
        [TomlExpand]
        private Dictionary<string, string> _dialogueChoices = new();
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public float AggroRadius
        {
            get => _aggroRadius;
            set => _aggroRadius = value;
        }

        public Dictionary<string, PlayerLocation> Waypoints => _waypoints;

        public Dictionary<string, string> DialogueChoices => _dialogueChoices;
        
        public void AddWaypoint(string name, PlayerLocation location)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Waypoint name cannot be null or whitespace.", nameof(name));
            
            _waypoints.Add(name, location);
        }

        public void AddDialogueChoice(string choice, string dialogue)
        {
            if (string.IsNullOrWhiteSpace(choice))
                throw new ArgumentException("Choice cannot be null or whitespace.", nameof(choice));

            _dialogueChoices.Add(choice, dialogue);
        }
    }
}
