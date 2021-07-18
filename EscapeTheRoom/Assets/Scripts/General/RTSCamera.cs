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

        public bool Active {get; set;}


        public void OnRotate(float direction)
        {
            if(!Active) return;


            transform.Rotate(
                Vector3.up * (direction * _rotationSpeed * Time.deltaTime),
                Space.World
            );
        }

        public void OnTranslate(Vector3 direction)
        {
            if(!Active) return;


            var translation = _pivot.transform.TransformDirection(direction);
            if (translation.magnitude > 1.0f)
                translation.Normalize();

            transform.position +=
                (translation * (_movementSpeed * Time.deltaTime));
        }
    }
}
