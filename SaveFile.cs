using System;

namespace Yggdrasil
{
    [Serializable]
    class SaveFile
    {
        public int[] myChars { get; set; }
        public int selectedChar { get; set; }
        public int inStory { get; set; }
        public int[] stories { get; set; }
        public int[] achievements { get; set; }
        public string verification { get; set; }
    }
}