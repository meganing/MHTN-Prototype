using System;

namespace Project.Data
{
    [Serializable]
    public struct SaveGamePlayerData
    {
        public string name;
        public int level;

        public override readonly string ToString()
        {
            return $"{{name:{name},level:{level}}}";
        }
    }
}