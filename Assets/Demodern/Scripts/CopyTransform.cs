using UnityEngine;

namespace Demodern
{
	public class CopyTransform : MonoBehaviour
	{
		public bool copyPosition = true;
		public bool copyRotation = true;
		public Transform target;
		private void Update() {
			if (copyPosition) {
				transform.position = target.position;
			}
			if (copyRotation) {
				transform.rotation = target.rotation;
			}
		}
	}
}

