using System.Collections;
using UnityEngine;


namespace EscapeTheRoom.Utilities
{
    /// <summary>
    /// Identifies that entity can perform actions with some delay
    /// </summary>
    public interface IRunLater
    {
        void RunLater(System.Action method, float waitSeconds);
        Coroutine RunLaterValued(System.Action method, float waitSeconds);
        IEnumerator RunLaterCoroutine(System.Action method, float waitSeconds);
    }

}
