using UnityEngine;


namespace EscapeTheRoom.Utilities.Variables
{
    public class Reference<TClass, TVariable> where TClass : Variable<TVariable>
    {
        [SerializeField] private bool UseConstant = true;
        [SerializeField] private TVariable ConstantValue;
#pragma warning disable 0649
        [SerializeReference] private TClass Variable;
#pragma warning restore 0649

        public TVariable Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }


        public Reference() { }

        public Reference(TVariable value)
        {
            UseConstant = true;
            ConstantValue = value;
        }


        public static implicit operator TVariable(Reference<TClass, TVariable> reference)
        {
            return reference.Value;
        }
    }
}
