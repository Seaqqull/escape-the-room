using UnityEngine;


namespace EscapeTheRoom
{
    public class Pivot : MonoBehaviour
    {
        [System.Serializable]
        private struct Axis
        {
            public bool X;
            public bool Y;
            public bool Z;
        }

        [SerializeField] private GameObject _pivot;
        [Space]
        [Header("Pivoting")]
        [SerializeField] private Axis _rotation;

        private Transform _pivotTransform;


        private void Awake()
        {
            _pivotTransform = _pivot?.GetComponent<Transform>();
        }


        private void Update()
        {
            var pivotRotation = _pivotTransform.rotation;
            var originRotation = transform.rotation;

            transform.rotation = new Quaternion(
                _rotation.X? pivotRotation.x : originRotation.x,
                _rotation.Y? pivotRotation.y : originRotation.y,
                _rotation.Z? pivotRotation.z : originRotation.z,
                pivotRotation.w);
        }
    }
}
