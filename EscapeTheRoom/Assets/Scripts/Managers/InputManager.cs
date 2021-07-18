using EscapeTheRoom.Base;
using UnityEngine.Events;
using UnityEngine;


namespace EscapeTheRoom.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private UnityEvent<Vector3> _onMovement;
        [SerializeField] private UnityEvent<float> _onRotation;
        [SerializeField] private UnityEvent<Vector3> _onMouseMoove;

        
        private void Update()
        {
            var newMousePosition = Input.mousePosition;
            var translation = GetInputTranslation();
            var rotation = GetInputRotation();


            _onMouseMoove.Invoke(newMousePosition);
            _onMovement.Invoke(translation);
            _onRotation.Invoke(rotation);
        }

        
        private float GetInputRotation()
        {
            float rotation = 0;

            // Left-Right
            if (Input.GetKey(KeyCode.Q))
                rotation -= 1;
            if (Input.GetKey(KeyCode.E))
                rotation += 1;

            return rotation;
        }

        private Vector3 GetInputTranslation()
        {
            Vector3 direction = Vector3.zero;

            // Forward-Back
            if (Input.GetKey(KeyCode.W))
                direction += Vector3.forward;
            if (Input.GetKey(KeyCode.S))
                direction += Vector3.back;

            // Left-Right
            if (Input.GetKey(KeyCode.A))
                direction += Vector3.left;
            if (Input.GetKey(KeyCode.D))
                direction += Vector3.right;

            return direction;
        }
    }
}
