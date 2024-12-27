using System.Collections;
using Baruah.HackNSlash.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Baruah.HackNSlash
{
    public class SplashHandler : MonoBehaviour
    {
        [SerializeField] private bool loadNextScene;
        
        private IEnumerator Start()
        {
            Services.ServiceManager.Instance.AddService(new InputManager());

            if (loadNextScene)
            {
                yield return SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
