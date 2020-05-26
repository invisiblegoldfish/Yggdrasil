namespace Yggdrasil
{
    public class Story
    {
        public int ID { get; set; }
        public string scenery { get; set; }
        public string situation { get; set; }
        public int area { get; set; }
        public Decision[] decisions { get; set; }      

        public class Decision
        {
            public string text { get; set; }
            public Followup[] followups { get; set; }
        }

        public class Followup
        {
            public int ID { get; set; }
            public float probability { get; set; }
            public int[] conditions { get; set; }
        }
    }
}
