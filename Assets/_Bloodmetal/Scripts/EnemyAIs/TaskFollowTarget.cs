using BehaviorTree;
using Selivura;
using UnityEngine;
namespace Selivura
{
    public class TaskFollowTarget : Node
    {
        Transform _transform;
        EnemyMovement _enemyMovement;
        private float _speed = 2;
        public TaskFollowTarget(Transform transform, float speed)
        {
            _transform = transform;
            _enemyMovement = _transform.GetComponent<EnemyMovement>();
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");
            if (Vector2.Distance(_transform.position, target.position) > 0.01f)
            {
                _enemyMovement.Move(target.position - _transform.position, _speed);
            }
            state = NodeState.Running;
            return state;
        }
    }
}