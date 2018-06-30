using System;
using UnityEngine.UI;
using UnityEngine;

namespace Demodern
{
	[RequireComponent(typeof(Button))]
	public class InteractionTrigger : MonoBehaviour
	{
		public GameLogic.InteractionType i;
		public void TriggerInteraction() {
			GameLogic.instance.Interaction (i);
		}
	}
}

