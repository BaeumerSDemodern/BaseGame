using UnityEngine;

namespace Demodern
{
	public class CopyFlatPosition : MonoBehaviour
	{
		public Transform target;
		private void Update() {
			transform.position = new Vector3(target.position.x,transform.position.y,target.position.z);
		}
	}
}

