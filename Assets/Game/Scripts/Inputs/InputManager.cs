using Baruah.HackNSlash.Services;
using UnityEngine;

namespace Baruah.HackNSlash.Input
{
    public class InputManager : IService
    {
        private InputControls controls;
        
        public InputControls.PlayerActions Player => controls.Player;
        
        public InputManager()
        {
            controls = new InputControls();
        }
        
        public void Update()
        {
            
        }

        public void OnDestroy()
        {
            controls = null;
        }
    }
}
