using EscapeTheRoom.Base;
using UnityEngine.Events;
using UnityEngine;


namespace EscapeTheRoom.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        private enum PressState {Press, Hold, Release, Inactive}

        [Header("Keyboard")]
        [SerializeField] private UnityEvent<Vector3> _onMovement;
        [SerializeField] private UnityEvent<float> _onRotation;
        [Header("Mouse")]
        [SerializeField] private UnityEvent<Vector2> _onMouseMoove;
        [SerializeField] private UnityEvent _onMouseClick;

        private InputMaster _input;

        private Vector3 _direction;
        private float _rotation;

        private PressState _directionState;
        private PressState _rotationState;


        protected override void Awake()
        {
            base.Awake();


            _input = new InputMaster();

            // Keyboard
            // Movement
            _input.Camera.Movement.performed += (context) => OnMovement(context.ReadValue<Vector2>());
            _input.Camera.Movement.canceled += (context) => OnMovementCancel();

            // Rotation
            _input.Camera.Rotation.performed += (context) => OnRotation(context.ReadValue<float>());
            _input.Camera.Rotation.canceled += (context) => OnRotationCancel();


            // Mouse
            // Move
            _input.Player.Point.performed += context => OnMouseMove(context.ReadValue<Vector2>());

            // Click
            _input.Player.Click.performed += _ => OnMouseClick();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            if (_directionState != PressState.Inactive)
            {
                if (_directionState == PressState.Press)
                    _directionState = PressState.Hold;
                if (_directionState == PressState.Release)
                    _directionState = PressState.Inactive;

                _onMovement.Invoke(_direction);
            }

            if (_rotationState != PressState.Inactive)
            {
                if (_rotationState == PressState.Press)
                    _rotationState = PressState.Hold;
                if (_rotationState == PressState.Release)
                    _rotationState = PressState.Inactive;

                _onRotation.Invoke(_rotation);
            }
        }


        private void OnRotationCancel()
        {
            _rotationState = PressState.Release;
            _rotation = 0.0f;
        }

        private void OnMovementCancel()
        {
            _directionState = PressState.Release;
            _direction = Vector3.zero;
        }


        private void OnRotation(float inputValue)
        {
            _rotationState = PressState.Press;
            _rotation = inputValue;
        }

        private void OnMovement(Vector2 inputValue)
        {
            _directionState = PressState.Press;
            _direction = new Vector3(inputValue.x, 0.0f, inputValue.y);
        }


        private void OnMouseClick()
        {
            _onMouseClick.Invoke();
        }

        private void OnMouseMove(Vector2 mousePosition)
        {
            _onMouseMoove.Invoke(mousePosition);
        }
    }
}
