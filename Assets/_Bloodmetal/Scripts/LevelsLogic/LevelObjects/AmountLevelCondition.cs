using UnityEngine;

namespace Selivura
{
    public class AmountLevelCondition : LevelCondition
    {
        [SerializeField] protected int amount;
        [SerializeField] protected int requiredAmount;
        public override bool ConditionSatisfied { get => requiredAmount <= amount; }

        public void AddToAmount(int toAdd = 1)
        {
            if (ConditionSatisfied)
                return;
            amount += toAdd;
            if (amount >= requiredAmount)
            {
                OnConditionMatched?.Invoke();
            }
        }
        public override void ResetCondition()
        {
            amount = 0;
        }
    }
}
