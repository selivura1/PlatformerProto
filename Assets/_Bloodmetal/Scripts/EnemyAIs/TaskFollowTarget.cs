using BehaviorTree;
using Selivura;
using UnityEngine;
namespace Selivura
{
    public class TaskFollowTarget : Node
    {
        Transform _transform;
        Enemy _enemy;
        private float _speed = 2;
        public TaskFollowTarget(Transform transform, float speed)
        {
            _transform = transform;
            _enemy = _transform.GetComponent<Enemy>();
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");
            if (Vector2.Distance(_transform.position, target.position) > 0.01f)
            {
                _enemy.Move(target.position - _transform.position, _speed);
            }
            state = NodeState.Running;
            return state;
        }
    }
}