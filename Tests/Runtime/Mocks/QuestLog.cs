using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class QuestLog
    {
        [TomlLiteral]
        [TomlMultiline]
        private List<string> _completedQuests = new();

        public IReadOnlyList<string> CompletedQuests => _completedQuests;

        public void AddCompletedQuest(string questName)
        {
            if (string.IsNullOrWhiteSpace(questName))
                throw new ArgumentException("Quest name cannot be null or whitespace.", nameof(questName));

            if (!_completedQuests.Contains(questName))
                _completedQuests.Add(questName);
        }
    }
}
