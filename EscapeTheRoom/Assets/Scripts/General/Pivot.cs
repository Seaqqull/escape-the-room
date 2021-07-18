using UnityEngine;
using System;


namespace EscapeTheRoom.General
{
    public class Pivot : Base.BaseMono
    {
        [Serializable]
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
        [SerializeField] private Axis _position;

        private Transform _pivotTransform;


        protected override void Awake()
        {
            base.Awake();
            
            if (_pivot == null)
                throw new Exception("There is no pivot attached to entity");
            _pivotTransform = _pivot.GetComponent<Transform>();
        }
        
        private void Update()
        {
            if(_rotation.X || _rotation.Y || _rotation.Z)
                AlignRotation();
            if(_position.X || _position.Y || _position.Z)
                AlignPosition();
        }


        private void AlignRotation()
        {
            var pivotRotation = _pivotTransform.rotation;
            var originRotation = Transform.rotation;

            Transform.rotation = new Quaternion(
                _rotation.X? pivotRotation.x : originRotation.x,
                _rotation.Y? pivotRotation.y : originRotation.y,
                _rotation.Z? pivotRotation.z : originRotation.z,
                pivotRotation.w);
        }

        private void AlignPosition()
        {
            var pivotPosition = _pivotTransform.transform.position;
            var originPosition = Transform.position;

            Transform.position = new Vector3(
                _position.X? pivotPosition.x : originPosition.x,
                _position.Y? pivotPosition.y : originPosition.y,
                _position.Z? pivotPosition.z : originPosition.z);
        }
    }
}
