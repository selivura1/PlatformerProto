using UnityEngine;
using BehaviorTree;
using Selivura;

public class TaskShoot : Node
{
    private Transform _transform;
    private Projectile _projectile;
    private ProjectilePool _projectilePool;
    private float _attackCooldown = 1;
    private float _reloadTimer = 0;
    private bool _isReloading => _reloadTimer > 0;
    public TaskShoot(Transform transform, Projectile projectile, float cooldown)
    {
        _projectile = projectile;
        _projectilePool = GameObject.FindAnyObjectByType<ProjectilePool>();
        _transform = transform;
        _attackCooldown = cooldown;
        _reloadTimer = _attackCooldown;
    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (!target.gameObject.activeSelf)
        {
            ClearData("target");
            state = NodeState.Succes;
            return state;
        }
        if (_isReloading)
        {
            _reloadTimer -= Time.fixedDeltaTime;
        }
        else
        {
            _reloadTimer = _attackCooldown;
            Projectile spawned = _projectilePool.GetProjectile(_projectile);
            spawned.transform.position = _transform.position;
            spawned.transform.right = (target.transform.position - _transform.position);
            spawned.Initialize();
        }
        state = NodeState.Running;
        return state;
    }
}
