using UnityEngine;


namespace EscapeTheRoom.Utilities.Constant
{
    /// <summary>
    /// Animation hashed ids
    /// </summary>
    public static class Animation
    {
        public static readonly int SHOW = Animator.StringToHash("Show");
        public static readonly int HIDE = Animator.StringToHash("Hide");
        public static readonly int FAILURE = Animator.StringToHash("Failure");
        public static readonly int SUCCESS = Animator.StringToHash("Success");
    }

    public static class Player
    {
        public static readonly string TIME = "BestTime";
    }
}
