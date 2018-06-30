using UnityEngine;

namespace Demodern {
    public class DebugObject : MonoBehaviour {
        private void Awake(){
            Debugging.debugObjects.Add(this);
        }
    }
}