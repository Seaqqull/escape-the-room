using UnityEngine;


namespace EscapeTheRoom.Base
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance {get; private set;}

        protected virtual void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = (this as T);
        }
    }
}
