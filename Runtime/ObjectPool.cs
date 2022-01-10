using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constantin.Utils
{
    [System.Serializable]
    public class Pool
    {
        public enum SIZE_SETTING
        {
            STATIC,
            VARIABLE,
        }

        [SerializeField] private GameObject pooledObject;
        [SerializeField] private string seachTag;
        
        [SerializeField] private bool initializeAtStart = false;

        [Header("Size settings")]
        [SerializeField] private SIZE_SETTING type = SIZE_SETTING.VARIABLE;
        [SerializeField] private uint maxSize=1;

        public uint MaxSize { get => maxSize; }
        public SIZE_SETTING Type { get => type; }
        public GameObject PooledObject { get => pooledObject; }
        public bool InitializeAtStart { get => initializeAtStart; }
        public string SeachTag { get => seachTag;}

        public List<GameObject> allPooledObject;
    }

    public class BaseObjectPooler : Singleton<BaseObjectPooler>
    {
        [SerializeField] List<Pool> allPools;
        public Dictionary<Pool, List<GameObject>> poolDic;

        // Start is called before the first frame update
        void Awake()
        {
            base.Awake();
            poolDic = new Dictionary<Pool, List<GameObject>>();
            foreach (var pool in allPools)
            {
                var clist = new List<GameObject>();
                pool.allPooledObject = clist;
                poolDic.Add(pool, clist);
                if (pool.Type == Pool.SIZE_SETTING.STATIC || pool.InitializeAtStart)
                {
                    for (int i = 0; i < pool.MaxSize; i++)
                    {
                        clist.Add(Instantiate(pool.PooledObject));
                    }
                }
            }
        }

        public GameObject GetFromPool(string tag)
        {
            Pool currentPool = null;
            List<GameObject> currentPoolList = null;
            foreach (KeyValuePair<Pool,List<GameObject>> item in poolDic)
            {
                if (item.Key.SeachTag == tag)
                {
                    currentPool = item.Key;
                    currentPoolList = item.Value;
                    break;
                }
            }
            if (currentPoolList == null)
            {
                Debug.Log("[Object Pooler] No pool with tag : " + tag);
                return null;
            }


            for (int i = 0; i < currentPoolList.Count; i++)
            {
                if (!currentPoolList[i].activeInHierarchy)
                {
                    return currentPoolList[i];
                }
            }
            if (currentPool.Type == Pool.SIZE_SETTING.VARIABLE || (currentPoolList.Count < currentPool.MaxSize))
            {
                GameObject inst = Instantiate(currentPool.PooledObject);
                currentPoolList.Add(inst);
                return inst;
            }
            return null;
        }

        public bool TryGetFromPool(string tag, out GameObject returned)
        {
            returned = GetFromPool(tag);
            return returned;
        }

    }
}

