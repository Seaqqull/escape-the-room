using UnityEngine;


namespace EscapeTheRoom.Base
{
    public class BaseMono : MonoBehaviour
    {
        public Transform Transform
        {
            get;
            private set;
        }
        public Quaternion Rotation
        {
            get => Transform.rotation;
            set => Transform.rotation = value;
        }
        public GameObject GameObj
        {
            get;
            private set;
        }
        public Vector3 Position
        {
            get => Transform.position;
        }
        public Vector3 Forward
        {
            get => Transform.forward;
        }


        protected virtual void Awake()
        {
            Transform = transform;
            GameObj = gameObject;
        }
    }
}
