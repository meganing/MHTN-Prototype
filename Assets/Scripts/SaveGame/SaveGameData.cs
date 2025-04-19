using System;
using System.Collections.Generic;

namespace Project.Data
{
    [Serializable]
    public struct SaveGameData
    {
        public int version;
        public List<SaveGameCharacter> characters;

        public override readonly string ToString()
        {
            return $"{{version:{version}}}";
        }
    }

    [Serializable]
    public struct SaveGameCharacter
    {
        public string id;
        public List<SaveGameCharacterEpisode> episodes;
    }

    [Serializable]
    public struct SaveGameCharacterEpisode
    {
        public string id;
        public bool isUnlocked;
    }
}
