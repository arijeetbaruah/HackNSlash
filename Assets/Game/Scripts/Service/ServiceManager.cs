using System.Collections.Generic;
using UnityEngine;

namespace Baruah.HackNSlash.Services
{
    public class ServiceManager : MonoBehaviour
    {
        public static ServiceManager Instance;

        public Dictionary<System.Type, IService> Services = new();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            foreach (var service in Services.Values)
            {
                service.Update();
            }
        }

        public void AddService<T>(T service) where T : IService
        {
            Services.Add(typeof(T), service);
        }

        public T GetService<T>() where T : IService
        {
            return (T)Services[typeof(T)];
        }

        public void RemoveService<T>() where T : IService
        {
            Services.Remove(typeof(T));
        }
    }
}
