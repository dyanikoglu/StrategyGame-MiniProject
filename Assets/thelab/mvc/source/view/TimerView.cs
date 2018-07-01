using UnityEngine;
using System.Collections;

namespace thelab.mvc
{    
    /// <summary>
    /// Base class for collision related classes.
    /// </summary>
    public class TimerView : NotificationView {        
        /// <summary>
        /// Flag that indicates if the time-scale affects this Timer.
        /// </summary>
        public bool scale = true;

        /// <summary>
        /// Flag that indicates if this View is active.
        /// </summary>        
        public bool active = true;

        /// <summary>
        /// Duration of the timer.
        /// </summary>
        public float duration;

        /// <summary>
        /// Cycles before completion.
        /// </summary>
        public int count;

        /// <summary>
        /// Elapsed time.
        /// </summary>
        public float elapsed;

        /// <summary>
        /// Current step.
        /// </summary>
        public int step;

        /// <summary>
        /// Restarts the timer.
        /// </summary>
        public void Restart() {
            elapsed = 0f;
            step = 0;
        }

        /// <summary>
        /// Activates the Timer.
        /// </summary>
        public void Play() {
            active = true;
        }

        /// <summary>
        /// Stops the Timer and reset its values.
        /// </summary>
        public void Stop() {
            active = false;
            Restart();
        }

        /// <summary>
        /// Updates the timer logic.
        /// </summary>
        void Update() {
            if (!active) return;
            elapsed += scale ? Time.deltaTime : Time.unscaledDeltaTime;
            if(elapsed>=duration) {
                elapsed = 0f;
                Notify(notification + "@timer.step");
                step++;
                if(step>=count) {
                    Notify(notification + "@timer.complete");
                    active = false;
                }
            }
        }

    }
}