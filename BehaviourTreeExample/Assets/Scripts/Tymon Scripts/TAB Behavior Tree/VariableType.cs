using UnityEngine;

namespace TAB.BehaviorTree
{
    /// <summary>
    /// This class is used to pass trough a value and be able to change that value later in a different script. This stuff is apperently not possible? cannot use Instantiate(VeriableType<T>)
    /// </summary>
    /// <typeparam name="T">The type of the variable</typeparam>
    [CreateAssetMenu(fileName = "VariableType_", menuName = "TAB/BehaviorTree/VariableType")]
    public class VariableType<T> : BaseScriptableObject
    {
        //Old value, New value
        public System.Action<T, T> OnValueChanged;
        [SerializeField] private T value;


        public T Value
        {
            get { return value; }
            set
            {
                OnValueChanged?.Invoke(this.value, value);
                this.value = value;
            }
        }
    }
}