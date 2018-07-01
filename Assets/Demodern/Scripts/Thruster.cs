using UnityEngine;

namespace Demodern
{	
	[RequireComponent(typeof(Rigidbody))]
	public class Thruster : MonoBehaviour
	{
		private Rigidbody rigid;
		public float maxThrust = 1f;
		public float minThrust = 0f;
		[Range(0,100)]
		public float targetThrust = 0f;
		public float thrust = 0f;

		private void Start() {
			rigid = GetComponent<Rigidbody>();
		}
		private void FixedUpdate() {
			targetThrust = Input.GetButton("Fire1")?maxThrust:minThrust;
			thrust = Mathf.Lerp (thrust,Mathf.Min(Mathf.Max(targetThrust,minThrust),maxThrust),Time.deltaTime);
			rigid.AddForce (-transform.forward*thrust);			

		}
	}
}

