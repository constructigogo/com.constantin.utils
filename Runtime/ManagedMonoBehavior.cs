using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constantin.Utils
{
    public class ManagedMonoBehavior<T> : MonoBehaviour where T : ManagedMonoBehavior<T>
    {
        // Start is called before the first frame update
        public virtual void Start()
        {
            MonoManager<T>.Instance.AddToManager((T)this);
        }

        public virtual void ManualUpdate()
        {

        }

        public virtual void ManualFixedUpdate()
        {

        }
        public virtual void ManualLateUpdate()
        {

        }

    }
}

