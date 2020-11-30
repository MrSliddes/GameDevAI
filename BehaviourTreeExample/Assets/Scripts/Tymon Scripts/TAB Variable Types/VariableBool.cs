using UnityEngine;

namespace TAB.VariableTypes
{
    [CreateAssetMenu(fileName = "VariableBool_", menuName = "TAB/VariableTypes/Bool")]
    public class VariableBool : BaseScriptableObject
    {
        //Old value, New value
        public System.Action<bool, bool> OnValueChanged;
        [SerializeField] private bool value;

        public bool Value
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