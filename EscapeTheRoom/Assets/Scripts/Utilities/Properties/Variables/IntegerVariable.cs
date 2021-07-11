using UnityEngine;


namespace EscapeTheRoom.Utilities.Variables
{
    [CreateAssetMenu(menuName = "Variable/Integer")]
    public class IntegerVariable : Variable<int>
    {
        public void ApplyChange(int amount)
        {
            Value += amount;
        }

        public void ApplyChange(Variable<int> amount)
        {
            Value += amount.Value;
        }
    }
}
