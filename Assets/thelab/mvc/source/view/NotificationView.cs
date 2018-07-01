using UnityEngine;
using System.Collections;

namespace thelab.mvc
{

    /// <summary>
    /// Base class for all View with notifications.
    /// </summary>
    public class NotificationView : NotificationView<BaseApplication> { }

    /// <summary>
    /// Base class for all View with notification features.
    /// </summary>
    public class NotificationView<T> : View<T> where T : BaseApplication {

        /// <summary>
        /// Fixed notification. Can be empty.
        /// </summary>
        public string notification;

    }

}