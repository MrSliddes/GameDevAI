using UnityEngine;

namespace TAB.VariableTypes
{
    [CreateAssetMenu(fileName = "VariableTransform_", menuName = "TAB/VariableTypes/Transform")]
    public class VariableTransform : BaseScriptableObject
    {
        //Old value, New value
        public System.Action<Transform, Transform> OnValueChanged;
        [SerializeField] private Transform value;

        public Transform Value
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