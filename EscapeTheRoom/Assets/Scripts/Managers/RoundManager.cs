using EscapeTheRoom.General;
using UnityEngine.Events;
using UnityEngine;


namespace EscapeTheRoom.Managers
{
    public class RoundManager : Base.Singleton<RoundManager>
    {

        [SerializeField] private float _roundTime;
        [SerializeField] private RTSCamera _camera;
        [SerializeField] private Transform _center;
        [SerializeField] private UnityEvent _onBegin;
        [SerializeField] private UnityEvent _onClear;

        private bool _inputAllowed;
        private bool _firstLaunch = true;
        private bool _started;

        public bool InputAllowed
        {
            get {return _inputAllowed;}
            set
            {
                 if(_camera != null)
                     _camera.Active = value;
                _inputAllowed = value;
            }
        }
        public float RoundTime
        {
            get {return _roundTime;}
        }
        public bool Started
        {
            get {return _started;}
        }


        private void Update()
        {
            if(!_started)
                return;

            _roundTime += Time.deltaTime;

            UIManager.Instance.SetCountdownTime(_roundTime);
        }


        public void Stop()
        {
            InputAllowed = false;
            _started = false;

            UIManager.Instance.ShowEndMenu();
            AudioManager.Instance.StopBackground();
            AudioManager.Instance.PlanWin();
        }

        public void Begin()
        {
            if(_firstLaunch)
            {
                UIManager.Instance.HideStartMenu();
            }
            else
            {
                _onClear.Invoke();
                UIManager.Instance.HideEndMenu();
            }

            // Reset room parameters
            _camera.transform.position = _center.position;
            
            // Events
            AudioManager.Instance.PlayBackground();
            _onBegin.Invoke();

            // Reset round parameters
            InputAllowed = true;

            _firstLaunch = false;
            _started = true;
            _roundTime = 0;
        }

    }
}
