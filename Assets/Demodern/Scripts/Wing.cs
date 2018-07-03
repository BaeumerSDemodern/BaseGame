using UnityEngine;

namespace Demodern
{
	[RequireComponent(typeof(Rigidbody))]
	public class Wing : MonoBehaviour
	{
		[Range(0f,1f)]
		public float factor = 0.5f;
		[Range(0f,10f)]
		public float straightenCourse = .4f;
		private Rigidbody rigid;
		private void Start() {
			rigid = GetComponent<Rigidbody> ();
		}

		public float bank, hor, ver;

		private void FixedUpdate() {
			Vector3 forwardVelocity = Vector3.Project (rigid.velocity, transform.forward);
			float speed = forwardVelocity.magnitude;
			rigid.velocity = Vector3.Lerp(rigid.velocity,forwardVelocity,factor * speed);

			rigid.AddForce(Vector3.Lerp(transform.up/transform.position.y,Vector3.zero,speed/10));

			//add wasd
			rigid.AddRelativeTorque(bank * -Vector3.forward * 5f * speed);
			rigid.AddRelativeTorque(hor * Vector3.up * 5f * speed);
			rigid.AddRelativeTorque(ver * Vector3.right * 5f * speed);

			//tend to point at velocity
			rigid.AddRelativeTorque (straightenCourse * Vector3.Cross(rigid.transform.forward, rigid.velocity) * speed);

			rigid.drag = (rigid.velocity.magnitude*rigid.velocity.magnitude)/100f;
		}
	}
}

