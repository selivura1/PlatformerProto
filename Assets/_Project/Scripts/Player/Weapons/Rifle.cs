using UnityEngine;

namespace Selivura
{
    public class Rifle : Weapon
    {
        //private PlayerMovement _playerMovement;
        //private float _dashTime;
        //[SerializeField] private float _attackDuringDashEvery = 0.05f;
        //bool _dashMode = false;
        //protected List<Projectile> _dashProjectiles = new List<Projectile>();
        //private void Start()
        //{
        //    _playerMovement = player.GetComponent<PlayerMovement>();
        //}
        //protected override void OnFixedUpdate()
        //{
        //    if (_dashMode)
        //    {
        //        _dashTime += Time.fixedDeltaTime;
        //        if (_dashTime > _attackDuringDashEvery)
        //        {
        //            DashShoot(player.transform.up);
        //            DashShoot(-player.transform.up);
        //            _dashTime = 0;
        //        }
        //        if (!_playerMovement.IsDashing)
        //        {
        //            _dashMode = false;
        //            _dashTime = 0;
        //            WeaponState = WeaponState.Cooldown;
        //            //foreach (var projectile in _dashProjectiles)
        //            //{
        //            //    projectile.Initialize();
        //            //}
        //            //_dashProjectiles.Clear();
        //        }
        //    }
        //}
        //public override void DoAttackLogic(Vector2 direction)
        //{
        //    if (!_playerMovement.IsDashing)
        //        base.DoAttackLogic(direction);
        //    else
        //        _dashMode = true;
        //}
        //private void DashShoot(Vector2 direction)
        //{
        //    var spawned = projectilePool.GetProjectile(data.BulletPrefab);
        //    spawned.transform.position = transform.position;
        //    spawned.transform.right = direction;
        //    spawned.Initialize();
        //    // _dashProjectiles.Add(spawned);
        //}
        public override void AfterAttackLogic()
        {

        }
    }
}
