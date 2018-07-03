using UnityEngine;

namespace Demodern
{
	public class FlyByWire : MonoBehaviour
	{
		public Thruster[] thrusters;
		public Wing[] wings;
		private void Update(){
			foreach (var t in thrusters) {
				t.targetThrust = Input.GetButton("Fire1")?t.maxThrust:t.minThrust;
			}
			foreach (var w in wings) {
				w.bank = Input.GetAxis ("Bank");
				w.hor = Input.GetAxis ("Horizontal");
				w.ver = Input.GetAxis ("Vertical");
			}
		}
	}
}

