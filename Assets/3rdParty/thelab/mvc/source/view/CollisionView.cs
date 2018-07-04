using UnityEngine;
using System.Collections;

namespace thelab.mvc
{
    /// <summary>
    /// Class that implements CollisionView features for any BaseApplication.
    /// </summary>
    public class CollisionView : CollisionView<BaseApplication> { }

    /// <summary>
    /// View class that detects and notifies collision related events.
    /// </summary>
    public class CollisionView<T> : ColliderView<T> where T : BaseApplication {   
        /// <summary>
        /// Callbacks when a Trigger Collider suffers interaction.
        /// </summary>
        /// <param name="p_collider"></param>
        void OnCollisionEnter(Collision p_collider) { if(enter) Notify(notification + "@collision.enter", p_collider); }
        void OnCollisionExit(Collision p_collider)  { if(exit)  Notify(notification + "@collision.exit",  p_collider); }
        void OnCollisionStay(Collision p_collider)  { if(stay)  Notify(notification + "@collision.stay",  p_collider); }
    }

}