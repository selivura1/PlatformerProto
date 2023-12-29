using BehaviorTree;
using UnityEngine;

namespace Selivura
{
    public class TaskPatrol : Node
    {
        Transform _transform;
        Transform[] _waypoints;
        Enemy _enemy;

        private int _currentWaypointIndex = 0;

        private float _speed = 2;
        private float _waitTime = 1;
        private float _waitCounter = 0;
        private bool _waiting = false;
        private float _reachDistance;
        public TaskPatrol(Transform transform, Transform[] waypoints, float speed, float reachDistance)
        {
            _transform = transform;
            _waypoints = waypoints;
            _speed = speed;
            _enemy = _transform.GetComponent<Enemy>();
            _reachDistance = reachDistance;
        }
        public override NodeState Evaluate()
        {
            if (_waiting)
            {
                _waitCounter += Time.fixedDeltaTime;
                if (_waitCounter >= _waitTime)
                    _waiting = false;
            }
            else
            {
                Transform wp = _waypoints[_currentWaypointIndex];
                if (Vector3.Distance(_transform.position, wp.position) < _reachDistance)
                {
                    _transform.position = wp.position;
                    _waitCounter = 0;
                    _waiting = true;
                    _enemy.StopMoving();
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                }
                else
                {
                    _enemy.Move(wp.position - _transform.position, _speed);
                }
            }

            state = NodeState.Running; return state;
        }
    }
}
