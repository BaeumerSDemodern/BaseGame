using UnityEngine;

namespace Demodern
{
	public class LazyCopyTransform : MonoBehaviour
	{
		public bool copyPosition = true;
		public bool copyRotation = true;
		public Transform target;
		public float laziness = .1f;
		private void Update() {
			if (copyPosition) {
				transform.position = Vector3.Lerp(transform.position,target.position,Time.deltaTime*(1f/laziness));
			}
			if (copyRotation) {
				transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,Time.deltaTime*(1f/laziness));
			}
		}
	}
}

