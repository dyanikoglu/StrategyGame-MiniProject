using UnityEngine;
using System.Collections;

namespace thelab.mvc
{

    /// <summary>
    /// Delegate that describes a noticiation handler.
    /// </summary>
    /// <param name="p_event"></param>
    /// <param name="p_target"></param>
    /// <param name="p_data"></param>
    public delegate void NotificationCallback(string p_event, Object p_target, params object[] p_data);

    /// <summary>
    /// Base class for all Controllers in the application.
    /// </summary>
    public class Controller : Element {

        /// <summary>
        /// Handles notifications sent from any Element in the currently running scene.
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        /// <param name="p_data"></param>
        virtual public void OnNotification(string p_event, Object p_target, params object[] p_data) { }
        
    }

    /// <summary>
    /// Base class for all Controller related classes.
    /// </summary>
    public class Controller<T> : Controller where T : BaseApplication {
        /// <summary>
        /// Returns app as a custom 'T' type.
        /// </summary>
        new public T app { get { return (T)base.app; } }
    }

}