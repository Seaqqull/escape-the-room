using UnityEngine;


namespace EscapeTheRoom.Managers
{
    public class RoundManager : Base.Singleton<RoundManager>
    {

        [SerializeField] private float _roundTime;

        private bool _started;


        private void Update()
        {
            if(!_started)
                return;

            _roundTime += Time.deltaTime;

            UIManager.Instance.SetCountdownTime(_roundTime);
        }


        public void Begin()
        {
            _started = true;

            _roundTime = 0;
        }

        public void Stop()
        {
            _started = false;
        }
    }
}
