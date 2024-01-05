using Pooling;
using Selivura;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    public static VFXPool instance;
    [SerializeField] PoolingSystem<VFX> _poolingSystem;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
        _poolingSystem = new PoolingSystem<VFX>(transform);
    }
    public VFX GetVFX(VFX prefab)
    {
        return _poolingSystem.Get(prefab);
    }
}
