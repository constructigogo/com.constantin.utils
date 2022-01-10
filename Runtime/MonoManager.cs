using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constantin.Utils
{
    public class MonoManager<T> : Singleton<MonoManager<T>> where T : ManagedMonoBehavior<T>
    {
        public List<T> managedItems;

        [SerializeField] bool updateEnabled = true;
        [SerializeField] bool fixedUpdateEnabled = true;
        [SerializeField] bool lateUpdateEnabled = true;

        void Awake()
        {
            base.Awake();
            managedItems = new List<T>();
        }

        public virtual void AddToManager(T toAdd)
        {
            managedItems.Add(toAdd);
        }


        public virtual void Update()
        {
            if (updateEnabled)
            {
                for (int i = 0; i < managedItems.Count; i++)
                {
                    managedItems[i].ManualUpdate();
                }
            }
            
        }

        public virtual void FixedUpdate()
        {
            if (fixedUpdateEnabled)
            {
                for (int i = 0; i < managedItems.Count; i++)
                {
                    managedItems[i].ManualFixedUpdate();
                }
            }
        }

        public virtual void LateUpdate()
        {
            if (lateUpdateEnabled)
            {
                for (int i = 0; i < managedItems.Count; i++)
                {
                    managedItems[i].ManualLateUpdate();
                }
            }
            
        }
    }
}

