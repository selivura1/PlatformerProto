using UnityEngine;

namespace Selivura
{
    public class AllEnemiesKilledCondition : LevelCondition
    {
        [SerializeField] EnemySpawnPoint[] _spawnPoints;
        int _killed;
        public override bool ConditionSatisfied => _killed >= _spawnPoints.Length;
        protected override void OnAwake()
        {
            foreach (var item in _spawnPoints)
            {
                item.OnEnemyCleared.AddListener(AddKilledEnemy);
            }
        }
        private void AddKilledEnemy()
        {
            if (ConditionSatisfied)
                return;
            _killed++;
            if (ConditionSatisfied)
            {
                OnConditionMatched?.Invoke();
            }
        }
        public override void ResetCondition()
        {
            _killed = 0;
        }
    }
}