using UnityEngine;


namespace EscapeTheRoom.General
{
    public class RTSCamera : MonoBehaviour
    {
        [SerializeField] private Pivot _pivot;
        [Header("Movement")]
        [SerializeField] private float _movementSpeed;
        [Header("Rotation")]
        [SerializeField] private float _rotationSpeed;

        private Vector3 _translation;
        private float _rotation;
        
        public bool Active {get; set;}
        

        private void LateUpdate()
        {
            if(!Active)
                return;
            
            
            // Rotation
            if (_rotation != 0.0f)
            {
                transform.Rotate(
                    Vector3.up * (_rotation * _rotationSpeed * Time.deltaTime),
                    Space.World
                );    
            }
            
            // Translation
            if (_translation != Vector3.zero)
            {
                transform.position +=
                    (_translation * (_movementSpeed * Time.deltaTime));   
            }
        }
        
        
        public void UpdateDirection(float direction)
        {
            _rotation = direction;
        }

        public void UpdateTranslation(Vector3 direction)
        {
            _translation = direction;
            
            _translation = _pivot.transform.TransformDirection(_translation);
            if (_translation.magnitude > 1.0f)
                _translation.Normalize();
        }
    }
}
