using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        public string Name;
        public string Description;
        public int SceneID;
    }
}
