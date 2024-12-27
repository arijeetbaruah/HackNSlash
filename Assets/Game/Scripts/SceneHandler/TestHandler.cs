using System.Collections;
using System.Collections.Generic;
using Baruah.HackNSlash.Input;
using Baruah.HackNSlash.Services;
using Unity.Cinemachine;
using UnityEngine;

namespace Baruah.HackNSlash
{
    public class TestHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private CinemachineCamera _camera;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            
            var player = Instantiate(_playerPrefab);
            var lookTarget = player.transform.Find("LookTarget");
            
            _camera.Follow = lookTarget;
            
            ServiceManager.Instance.GetService<InputManager>().Player.Enable();
        }
    }
}
