using UnityEngine;

namespace Utilities.Object_Pooler
{
    [System.Serializable]
    public struct ObjectPoolItem {
        public GameObject objectToPool;
        public int amountToPool;
        public PoolItemType poolItemType;
    }
}