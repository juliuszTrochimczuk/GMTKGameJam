using UnityEngine;

namespace _01_9thWave.Scripts.Singleton
{
    public abstract class SingletonDoNotDestroy<T> : Singleton<T> where T : Component
    {
        protected new void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
