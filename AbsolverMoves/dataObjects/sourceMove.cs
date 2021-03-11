using System.Collections.Generic;

namespace absolverMoves.dataObjects
{
    public class sourceMoveList
    {
        public List<sourceMove> moveList;
    }

    public class sourceMove
    {
        public string name { get; set; }
        public string style { get; set; }
        public sourceStance stance { get; set; }
        public string hits { get; set; }
        public string height { get; set; }
        public string type { get; set; }
        public sourceFrames frames { get; set; }
        public List<string> modifiers { get; set; }
        // public float range { get; set; }
    }

    public class sourceQuadrants
    {
        public string FRONT_RIGHT { get; set; }
        public string FRONT_LEFT { get; set; }
        public string BACK_RIGHT { get; set; }
        public string BACK_LEFT { get; set; }
    }

    public class sourceStance
    {
        public sourceQuadrants barehands { get; set; }
        public sourceQuadrants sword { get; set; }
    }

    public class sourceFrames
    {
        public int startup { get; set; }
        public sourceFramesAdvantage advantage { get; set; }
    }

    public class sourceFramesAdvantage
    {
        public int hit { get; set; }
        public int guard { get; set; }
    }
}
