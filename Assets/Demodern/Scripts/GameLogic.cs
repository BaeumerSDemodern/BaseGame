using UnityEngine;
using System;

namespace Demodern {
    public class GameLogic : MonoBehaviour {
		public static GameLogic instance;
		public Transform headContainer;
		public Transform head;

		[Serializable]
		public class State : StateMachine<GameLogic>.State {
			public GameObject[] stateObjects = {};
			public State(GameLogic r) : base(r){ }
			public override void Enter ()
			{
				base.Enter ();
				Debugging.print ("Enter " + this);
				foreach (var o in stateObjects) {
					o.SetActive (true);
				}
			}

			public override void Exit(){
				base.Exit ();
				foreach (var o in stateObjects) {
					o.SetActive (false);
				}

			}
			public virtual void Interaction(InteractionType i) { 
				switch (i) {
					case InteractionType.Debug:
						{
							Debugging.ToggleDebug ();
							break;
						}
				}
			}
		}
		[Serializable]
        public class Init : State {
			public Init(GameLogic r) : base(r) { }

			public override void Enter ()
			{
				base.Enter ();
			}

			public override void Update ()
			{
				base.Update ();
				Debugging.Log (Time.time);
			}

			public override void Exit(){
				base.Exit ();
			}

			public override void Interaction(InteractionType i) { 
				base.Interaction (i);
				switch (i) {
					case InteractionType.Start:
						{
							representation.stateMachine.SetState(representation.inGame);
							break;
						}
				}
			}
        }
		[Serializable]
		public class InGame : State {
			public InGame(GameLogic r) : base(r) { }

			public override void Enter ()
			{
				base.Enter ();
			}

			public override void Update ()
			{
				base.Update ();
			}

			public override void Exit(){
				base.Exit ();
			}

			public override void Interaction(InteractionType i) { 
				base.Interaction (i);
				switch (i) {
				case InteractionType.Start:
					{
						representation.stateMachine.SetState(representation.init);
						break;
					}
				}
			}
		}
        
        public StateMachine<GameLogic> stateMachine;
		public Init init;
		public InGame inGame;

        private void Awake() {
			instance = this;
            init = new Init(this);
			inGame = new InGame (this);
			stateMachine = new StateMachine<GameLogic>(init);
        }

		private void Update() {
			if (stateMachine != null) {
				stateMachine.Update();
			}
		}

		public enum InteractionType {
			Start,
			End,
			Score,
			Debug
		}

		public void Interaction(InteractionType i){
			State s = (State)stateMachine.state;
			s.Interaction(i);
		}
    }
}