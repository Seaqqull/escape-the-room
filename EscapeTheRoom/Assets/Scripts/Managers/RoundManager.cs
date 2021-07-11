using EscapeTheRoom.Rooms;
using UnityEngine;


namespace EscapeTheRoom.Managers
{
    public class RoundManager : Base.Singleton<RoundManager>
    {

        [SerializeField] private float _roundTime;
        [SerializeField] private RTSCamera _camera;
        [SerializeField] private RoomController _room;

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
                _inputAllowed = true;
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
        }

        public void Clear()
        {
            _room.Disassemble();
        }

        public void Begin()
        {
            if(_firstLaunch)
            {
                UIManager.Instance.HideStartMenu();
            }
            else
            {
                Clear();
                Player.Instance.ClearInventory();
                UIManager.Instance.HideEndMenu();
            }
            _room.Assemble();

            InputAllowed = true;

            _firstLaunch = false;
            _started = true;
            _roundTime = 0;
        }

    }
}
