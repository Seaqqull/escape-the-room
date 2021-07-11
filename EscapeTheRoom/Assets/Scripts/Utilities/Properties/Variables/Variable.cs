using UnityEngine;


namespace EscapeTheRoom.Utilities.Variables
{
    public class Variable<T> : ScriptableObject
    {
        [SerializeField] private T _value;
#if UNITY_EDITOR
#pragma warning disable 0414
        [Multiline]
        [SerializeField] private string _description = "";
#pragma warning restore 0414
#endif

        public T Value
        {
            get { return this._value; }
            protected set { this._value = value; }
        }


        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetValue(Variable<T> value)
        {
            Value = value.Value;
        }

    }
}
