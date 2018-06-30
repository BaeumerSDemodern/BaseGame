using UnityEngine;

namespace Demodern
{
	[RequireComponent(typeof(Rigidbody))]
	public class PartJoint : MonoBehaviour
	{
		private Rigidbody rigid;
		private Rigidbody parent;
		public float breakForce = 10f;
		private void Start() {
			rigid = GetComponent<Rigidbody> ();
			parent = transform.parent.GetComponent<Rigidbody> ();
			if (parent) {
				transform.SetParent (transform.parent.parent);
				FixedJoint j = gameObject.AddComponent<FixedJoint> ();
				j.breakForce = breakForce;
				j.connectedBody = parent;
			}
		}
	}
}

