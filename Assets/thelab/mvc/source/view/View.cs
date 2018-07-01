using UnityEngine;
using System.Collections;

namespace thelab.mvc {

    /// <summary>
    /// Base class for all View related classes.
    /// </summary>
    public class View : Element { }

    /// <summary>
    /// Base class for all View related classes.
    /// </summary>
    public class View<T> : View where T : BaseApplication {

        /// <summary>
        /// Returns app as a custom 'T' type.
        /// </summary>
        new public T app { get { return (T)base.app; } }

    }

}