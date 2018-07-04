using UnityEngine;
using System.Collections;

namespace thelab.mvc
{

    /// <summary>
    /// Base class for all Model related classes.
    /// </summary>
    public class Model : Element
    {

    }


    /// <summary>
    /// Base class for all Model related classes.
    /// </summary>
    public class Model<T> : Model where T : BaseApplication
    {
        /// <summary>
        /// Returns app as a custom 'T' type.
        /// </summary>
        new public T app { get { return (T)base.app; } }
    }
}