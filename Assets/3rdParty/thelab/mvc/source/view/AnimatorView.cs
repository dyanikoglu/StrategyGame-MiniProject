using UnityEngine;
using System.Collections;

namespace thelab.mvc
{   
    /// <summary>
    /// Extension of AnimatorView to handle any kind of application.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AnimatorView<T> : AnimatorView where T : BaseApplication {
        /// <summary>
        /// Reference to the application.
        /// </summary>
        new public T app { get { return (T)(object)base.app; } }
    }

    /// <summary>
    /// Base class for collision related classes.
    /// </summary>
    public class AnimatorView : StateMachineBehaviour {
        /// <summary>
        /// Notification to be sent.
        /// </summary>
        public string notification;

        /// <summary>
        /// Flags of enabled notifications.
        /// </summary>
        public bool enter  = true;
        public bool update = false;
        public bool exit   = true;
        public bool move   = true;
        public bool ik     = false;
        public bool begin  = true;
        public bool end    = true;

        /// <summary>
        /// Reference to the main application running.
        /// </summary>
        public BaseApplication app { get { return m_app == null ? (m_app = GameObject.FindObjectOfType<BaseApplication>()) : m_app; } }
        private BaseApplication m_app;

        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (app == null) return;            
            if(enter)app.Notify(notification + "@animator-enter",animator,stateInfo,layerIndex);
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (app == null) return;
            if (update) app.Notify(notification + "@animator-update", animator, stateInfo, layerIndex);
        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (app == null) return;
            if (exit) app.Notify(notification + "@animator-exit", animator, stateInfo, layerIndex);
        }

        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (app == null) return;
            if (move) app.Notify(notification + "@animator-move", animator, stateInfo, layerIndex);
        }

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (app == null) return;
            if (ik) app.Notify(notification + "@animator-ik", animator, stateInfo, layerIndex);
        }

        // OnStateMachineEnter is called when entering a statemachine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
            if (app == null) return;
            if (begin) app.Notify(notification + "@fsm-enter", animator, stateMachinePathHash);
        }

        // OnStateMachineExit is called when exiting a statemachine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
            if (app == null) return;
            if (end) app.Notify(notification + "@fsm-exit", animator, stateMachinePathHash);
        }

    }
}