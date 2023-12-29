namespace Selivura
{
    public class AndLevelCondition : LevelCondition
    {
        public bool[] Conditions;
        public override bool ConditionSatisfied => CheckIfSatisfied();

        public override void ResetCondition()
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                Conditions[i] = false;
            }
        }
        public void SatisfyConditionAt(int index)
        {
            if (ConditionSatisfied)
                return;
            Conditions[index] = true;
            if (ConditionSatisfied)
                OnConditionMatched?.Invoke();
        }
        bool CheckIfSatisfied()
        {
            foreach (var conditon in Conditions)
            {
                if (!conditon)
                    return false;
            }
            return true;
        }
    }
}
