using UnityEngine;


namespace EscapeTheRoom
{
    public class RTSCamera : MonoBehaviour
    {
        [SerializeField] private Pivot _pivot;
        [Header("Movement")]
        [SerializeField] private float _movementSpeed;
        [Header("Rotation")]
        [SerializeField] private float _rotationSpeed;

        public bool Active {get; set;}


        private void LateUpdate()
        {
            if(!Active)
                return;


            var translation = GetInputTranslation();
            var rotation = GetInputRotation();


            translation = _pivot.transform.TransformDirection(translation);
            if (translation.magnitude > 1.0f)
                translation.Normalize();


            transform.Rotate(
                Vector3.up * (rotation * _rotationSpeed * Time.deltaTime),
                Space.World
            );
            transform.position +=
                (translation * _movementSpeed * Time.deltaTime);
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
