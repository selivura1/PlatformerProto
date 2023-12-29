using BehaviorTree;
using UnityEngine;

public class TaskCheckEnemyInRange : Node
{
    private LayerMask _enemyLayerMask = 8;
    private Transform _transform;
    private float _visionRange;
    public TaskCheckEnemyInRange(Transform transform, float fov, LayerMask mask)
    { 
        _transform = transform;
        _visionRange = fov;
        _enemyLayerMask = mask;
    }
    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, _visionRange, _enemyLayerMask);
            if (colliders.Length > 0)
            {
                Parent.Parent.SetData("target", colliders[0].transform);
                state = NodeState.Succes;
                return state;
            }
            state = NodeState.Failure;
            return state;
        }
        else
        {
            Transform target = (Transform)t;
            if (Vector2.Distance(target.position, _transform.position) > _visionRange)
            {
                ClearData("target");
                state = NodeState.Failure;
                return state;
            }
        }
        state = NodeState.Running;
        return state;
    }
}
