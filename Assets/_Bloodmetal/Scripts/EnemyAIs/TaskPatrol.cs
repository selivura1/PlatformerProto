using BehaviorTree;
using UnityEngine;

namespace Selivura
{
    public class TaskPatrol : Node
    {
        Transform _transform;
        Transform[] _waypoints;

        private int _currentWaypointIndex = 0;

        private float _waitTime = 1;
        private float _waitCounter = 0;
        private bool _waiting = false;
        public TaskPatrol(Transform transform, Transform[] waypoints)
        {
            _transform = transform;
            _waypoints = waypoints;
        }
        public override NodeState Evaluate()
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                    _waiting = false;
            }
            else
            {
                Transform wp = _waypoints[_currentWaypointIndex];
                if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
                {
                    _transform.position = wp.position;
                    _waitCounter = 0;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                }
                else
                {
                    _transform.position = Vector2.MoveTowards(_transform.position, wp.position, KnightBT.Speed * Time.deltaTime);
                    //_transform.LookAt(wp.position);
                }
            }

            state = NodeState.Running; return state;
        }
    }
}