namespace absolverMoves.dataObjects
{

    public class moveQuadrants
    {
        public bool front;
        public bool back;
        public bool left;
        public bool right;

        private bool isFront(string aCheck)
        {
            return aCheck == "FRONT_LEFT" || aCheck == "FRONT_RIGHT";
        }

        private bool isLeft(string aCheck)
        {
            return aCheck == "FRONT_LEFT" || aCheck == "BACK_LEFT";
        }

        public moveQuadrants(sourceQuadrants aQuadrants, bool aEndCheck = false)
        {

            if (aEndCheck)
            {
                if (string.IsNullOrEmpty(aQuadrants.FRONT_LEFT) && !isFront(aQuadrants.FRONT_LEFT))
                    back = true;

                if (string.IsNullOrEmpty(aQuadrants.FRONT_RIGHT) && !isFront(aQuadrants.FRONT_RIGHT))
                    back = true;

                if (string.IsNullOrEmpty(aQuadrants.BACK_LEFT) && isFront(aQuadrants.BACK_LEFT))
                    front = true;

                if (string.IsNullOrEmpty(aQuadrants.BACK_RIGHT) && isFront(aQuadrants.BACK_RIGHT))
                    front = true;

                if (string.IsNullOrEmpty(aQuadrants.FRONT_LEFT) && !isLeft(aQuadrants.FRONT_LEFT))
                    right = true;

                if (string.IsNullOrEmpty(aQuadrants.BACK_LEFT) && !isLeft(aQuadrants.BACK_LEFT))
                    right = true;

                if (string.IsNullOrEmpty(aQuadrants.FRONT_RIGHT) && isLeft(aQuadrants.FRONT_RIGHT))
                    left = true;

                if (string.IsNullOrEmpty(aQuadrants.BACK_RIGHT) && isLeft(aQuadrants.BACK_RIGHT))
                    left = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(aQuadrants.FRONT_LEFT) || !string.IsNullOrEmpty(aQuadrants.FRONT_RIGHT))
                    front = true;

                if (!string.IsNullOrEmpty(aQuadrants.BACK_LEFT) || !string.IsNullOrEmpty(aQuadrants.BACK_RIGHT))
                    back = true;

                if (!string.IsNullOrEmpty(aQuadrants.FRONT_LEFT) || !string.IsNullOrEmpty(aQuadrants.BACK_LEFT))
                    left = true;

                if (!string.IsNullOrEmpty(aQuadrants.FRONT_RIGHT) || !string.IsNullOrEmpty(aQuadrants.BACK_RIGHT))
                    right = true;
            }


        }
    }


    public class moveData
    {
        public enum moveStyle : int
        {
            invalid,
            kahlt,
            forsaken,
            windfall,
            stagger,
            faejin
        }

        public enum hitType : int
        {
            invalid,
            horizontal,
            vertical,
            thrust
        }

        public enum hitHeight : int
        {
            invalid,
            low,
            mid,
            high
        }

        public string name;//
        public moveStyle style;//

        // Stance
        public bool switchFront;
        public bool switchSide;

        public hitHeight moveHeight;
        public hitType moveType;

        public moveQuadrants startSword;
        public moveQuadrants startBarehands;

        public bool hitDiffSide;
        public int frameStartup;
        public int frameAdvantageHit;
        public int frameAdvantageGuard;
        public bool isStrafe;
        public bool isStop;
        public bool isJump;
        public bool isDuck;
        public bool isCharge;
        public bool isBreak;
        public bool isParry;
        public bool isDoubleHit;

        bool getChangeFront(sourceQuadrants aQuadrants) // Front to back or vice versa
        {
            return (
                !string.IsNullOrEmpty(aQuadrants.FRONT_LEFT) && (aQuadrants.FRONT_LEFT == "BACK_LEFT" || aQuadrants.FRONT_LEFT == "BACK_RIGHT") ||
                !string.IsNullOrEmpty(aQuadrants.FRONT_RIGHT) && (aQuadrants.FRONT_RIGHT == "BACK_LEFT" || aQuadrants.FRONT_RIGHT == "BACK_RIGHT") ||
                !string.IsNullOrEmpty(aQuadrants.BACK_LEFT) && (aQuadrants.BACK_LEFT == "FRONT_LEFT" || aQuadrants.BACK_LEFT == "FRONT_RIGHT") ||
                !string.IsNullOrEmpty(aQuadrants.BACK_RIGHT) && (aQuadrants.BACK_RIGHT == "FRONT_LEFT" || aQuadrants.BACK_RIGHT == "FRONT_RIGHT")
            );
        }

        public bool getChangeSide(moveQuadrants aStart, moveQuadrants aEnd)
        {

            if (aStart.left != aStart.right)
            {
                return (aStart.left != aEnd.left || aStart.right != aEnd.right);
            }

            return false;
        }

        public moveData(sourceMove aMoveData)
        {
            name = aMoveData.name;


            switch(aMoveData.height)
            {
                case "low":
                    moveHeight = hitHeight.low;
                    break;
                case "mid":
                    moveHeight = hitHeight.mid;
                    break;
                case "high":
                    moveHeight = hitHeight.high;
                    break;
                default:
                    moveHeight = hitHeight.invalid;
                    break;
            }

            switch (aMoveData.type)
            {
                case "thrust":
                    moveType = hitType.thrust;
                    break;
                case "vertical":
                    moveType = hitType.vertical;
                    break;
                case "horizontal":
                    moveType = hitType.horizontal;
                    break;
                default:
                    moveType = hitType.invalid;
                    break;
            }

            switch (aMoveData.style)
            {
                case "forsaken":
                    style = moveStyle.forsaken;
                    break;
                case "kahlt":
                    style = moveStyle.kahlt;
                    break;
                case "windfall":
                    style = moveStyle.windfall;
                    break;
                case "stagger":
                    style = moveStyle.stagger;
                    break;
                case "faejin":
                    style = moveStyle.faejin;
                    break;
                default:
                    style = moveStyle.invalid;
                    break;
            }

            startBarehands = new moveQuadrants(aMoveData.stance.barehands);
            startSword = new moveQuadrants(aMoveData.stance.sword);

            moveQuadrants endBarehands = new moveQuadrants(aMoveData.stance.barehands, true);
            moveQuadrants endSword = new moveQuadrants(aMoveData.stance.sword, true);

            switchSide = getChangeSide(startBarehands, endBarehands) || getChangeSide(startSword, endSword);

            hitDiffSide = aMoveData.hits == "diff";
            frameStartup = aMoveData.frames.startup;
            switchFront = getChangeFront(aMoveData.stance.barehands) || getChangeFront(aMoveData.stance.sword);
            frameAdvantageGuard = aMoveData.frames.advantage.guard;
            frameAdvantageHit = aMoveData.frames.advantage.hit;

            isStop = aMoveData.modifiers.Contains("stop");
            isBreak = aMoveData.modifiers.Contains("break");
            isDuck = aMoveData.modifiers.Contains("duck");
            isJump = aMoveData.modifiers.Contains("jump");
            isStrafe = aMoveData.modifiers.Contains("strafe");
            isParry = aMoveData.modifiers.Contains("parry");
            isCharge = aMoveData.modifiers.Contains("charge");
            isDoubleHit = aMoveData.modifiers.Contains("double");
        }
    }
}
