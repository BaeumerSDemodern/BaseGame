using System.Collections.Generic;
using UnityEngine;

namespace Demodern {
    public class Debugging : MonoBehaviour {
		public bool enableDebug = false;
        public static bool isDebug {
            get {
                return _isDebug;
            }
            set {
                _isDebug = value;
                for(int i = 0; i < debugObjects.Count; i++) {
                    DebugObject d = debugObjects[i];
                    if(d != null){
                        d.gameObject.SetActive(isDebug);
                    }                    
                }
            }
        }
        private static bool _isDebug = false;

        public static List<DebugObject> debugObjects = new List<DebugObject>();
        public delegate void PrintAction(string s);
        private static PrintAction _callback = null;        
        public static PrintAction callback{
            get {
                return _callback;
            }
            set {
                if(_callback != null) {
                    throw new System.Exception("Multiple Print Callbacks!");
                }
                _callback = value;
            }
        }
        public static void Log(object s){
            Log(s.ToString());
        }
        public static void Log(string s){
            if(isDebug){
                _callback(s);
            }            
        }

		public static void ToggleDebug(){
			isDebug = !isDebug;
			Debugging.Log ("Debug " + (isDebug?"enabled":"disabled"));
		}

		private void Awake() {
			isDebug = enableDebug;
			callback += (s) => {
				print(s);
			};
		}

		private void Update(){
			if (Input.GetKey (KeyCode.LeftControl)) {
				if (Input.GetKeyDown(KeyCode.D)) {
					ToggleDebug ();
				}
			}
		}
    }
}