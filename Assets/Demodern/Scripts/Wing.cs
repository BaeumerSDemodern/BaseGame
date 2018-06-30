using UnityEngine;

namespace Demodern
{
	[RequireComponent(typeof(Rigidbody))]
	public class Wing : MonoBehaviour
	{
		[Range(0f,1f)]
		public float factor = 0.5f;
		private Rigidbody rigid;
		private void Start() {
			rigid = GetComponent<Rigidbody> ();
		}

		private void FixedUpdate() {
			Vector3 forwardVelocity = Vector3.Project (rigid.velocity, transform.forward);
			rigid.velocity = Vector3.Lerp(rigid.velocity,forwardVelocity,factor * forwardVelocity.magnitude);

			//add wasd
			rigid.AddRelativeTorque(Input.GetAxis("Bank") * -Vector3.forward * 10f);
			rigid.AddRelativeTorque(Input.GetAxis("Horizontal") * Vector3.up * 10f);
			rigid.AddRelativeTorque(Input.GetAxis("Vertical") * Vector3.right * 10f);

			//tend to point at velocity
			rigid.AddRelativeTorque (.4f * Vector3.Cross(rigid.transform.forward, rigid.velocity));
		}
	}
}

