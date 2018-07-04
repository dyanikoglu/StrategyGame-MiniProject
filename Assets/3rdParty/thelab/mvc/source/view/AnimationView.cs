using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 0109
#pragma warning disable 0108

namespace thelab.mvc
{   
    
    /// <summary>
    /// Base class for collision related classes.
    /// </summary>
    public class AnimationView : NotificationView {

        #region class Argument

        /// <summary>
        /// Class that describes an event argument.
        /// </summary>
        [System.Serializable]
        public class Argument
        {
            #region enum Type

            /// <summary>
            /// Data type of the argument.
            /// </summary>
            public enum Type
            {
                Int=0,
                Float,
                String,
                Curve,
                Vector2,
                Vector3,
                Vector4,
                Rect,
                Color,
                Object
            }

            #endregion
            
            public Argument.Type type;
            public int            aInt;
            public float          aFloat;
            public string         aString;
            public AnimationCurve aCurve;
            public Vector2        aVector2;
            public Vector3        aVector3;
            public Vector4        aVector4;
            public Rect           aRect;
            public Color          aColor;
            public UnityEngine.Object  aObject;
        }

        #endregion

        #region class Event

        /// <summary>
        /// Class that describes an event.
        /// </summary>
        [System.Serializable]
        public class Event
        {
          
            /// <summary>
            /// Reference to the desired clip to track.
            /// </summary>
            public AnimationClip clip;

            /// <summary>
            /// List of callbacks for this event.
            /// </summary>
            public List<Callback> callbacks;

            /// <summary>
            /// Reference to the view which contains this event.
            /// </summary>
            public AnimationView view { get { return m_view; } }
            [System.NonSerialized]
            private AnimationView m_view;

            /// <summary>
            /// Initializes this event.
            /// </summary>
            internal void Init(AnimationView p_view)
            {
                m_view = p_view;
                for (int i = 0; i < callbacks.Count; i++) callbacks[i].Init(this);
            }

            /// <summary>
            /// Updates and check for events.
            /// </summary>
            internal void Update()
            {
                if (clip == null) return;
                if (view == null) return;
                if (view.animation == null) return;
                for (int i = 0; i < callbacks.Count; i++) callbacks[i].Update();
            }
            
        }

        #endregion

        #region struct Interval

        /// <summary>
        /// Describes an interval.
        /// </summary>
        [System.Serializable]
        public struct Interval {
            public float min;
            public float max;
        }

        #endregion

        #region class Callback

        [System.Serializable]
        public class Callback
        {
            /// <summary>
            /// Reference to this event's clip.
            /// </summary>
            [System.NonSerialized]
            private Event parent;

            /// <summary>
            /// Notification to be combined with the parent view notification.
            /// </summary>
            public string notification;

            /// <summary>
            /// Event position in seconds or frame.
            /// </summary>
            public Interval interval;

            /// <summary>
            /// Flag that indicates that the frame of the clip will be sampled for event emission.
            /// </summary>
            public bool useFrame = true;

            /// <summary>
            /// Flag that indicates the current time is inside the target frame.
            /// </summary>
            public bool active;

            /// <summary>
            /// Flag that indicates this callback will be called while the time is in range.
            /// </summary>
            public bool continuous;

            /// <summary>
            /// List of arguments.
            /// </summary>
            public List<Argument> args;

            /// <summary>
            /// Current time.
            /// </summary>
            internal float m_last_time;
            

            /// <summary>
            /// Time inside the clip timeline.
            /// </summary>
            public float time 
            {
                get
                {
                    AnimationClip c = parent.clip;                    
                    Animation a     = parent.view.animation;                    
                    AnimationState s = a[c.name];
                    return (s.normalizedTime - Mathf.Floor(s.normalizedTime)) * s.length;
                }
            }

            /// <summary>
            /// Current frame.
            /// </summary>
            public int frame
            {
                get
                {
                    AnimationClip c = parent.clip;                    
                    return (int)(time * c.frameRate);
                }
            }

            /// <summary>
            /// Progress of execution of the event.
            /// </summary>
            public float progress
            {
                get
                {                    
                    float dt = Mathf.Abs(interval.max - interval.min);
                    float r = 1f;
                    if(useFrame)
                    {
                        float f = (float)frame;
                        r = dt<=0f ? 1f : ((f - interval.min) / dt);
                    }
                    else
                    {
                        r = dt <= 0f ? 1f : ((time - interval.min) / dt);
                    }
                    return Mathf.Clamp01(r);
                }
            }

            /// <summary>
            /// Reference to the current animation state.
            /// </summary>
            public AnimationState state;

            /// <summary>
            /// Init.
            /// </summary>
            /// <param name="p_event"></param>
            internal void Init(Event p_event)
            {
                parent = p_event;
                AnimationClip c = parent.clip;
                if (c == null) return;
                Animation a = parent.view.animation;
                if (a == null) return;                
                m_last_time = time;
            }

            /// <summary>
            /// Updates and check for the event frame.
            /// </summary>
            internal void Update()
            {
                AnimationClip c = parent.clip;
                Animation a = parent.view.animation;
                
                if (!a.IsPlaying(c.name)) return;
                
                state = a[c.name];

                //bool is_wrap = false;
                float t0 = m_last_time;
                float t1 = time;
                //if (t1 < t0) is_wrap = true;
                m_last_time = t1;

                if (useFrame)
                {
                    t0 = Mathf.Floor(t0 * c.frameRate);
                    t1 = Mathf.Floor(t1 * c.frameRate);
                }

                float i0 = interval.min;
                float i1 = interval.max;

                bool in_range = false;

                in_range = ((t1 >= i0) && (t1 < i1)) || ((t0 >= i0) && (t0 < i1));
                if(!in_range)
                {
                    in_range = (t1 >= i1) && (t0 <= i0);
                }
                                
                //if (is_wrap) in_range = ((i0 >= t0) && (i0 > t1)) || ((i1 <= t0) && (i1 < t1));
                
                if (in_range)
                {
                    bool is_continuous = continuous && (Mathf.Abs(interval.max - interval.min) > 0f);
                    bool emmit = is_continuous || (!active);

                    if (emmit)
                    {                        
                        active = true;
                        parent.view.callback = this;
                        if (string.IsNullOrEmpty(notification))
                        {                            
                            parent.view.OnAnimationEvent(args);
                        }
                        else
                        {
                            string pn = parent.view.notification;
                            if (!string.IsNullOrEmpty(pn)) parent.view.OnAnimationEvent(pn + "." + notification, args);
                        }
                    }
                }
                else
                {
                    active = false;
                }
               
            }

        }

        #endregion

        /// <summary>
        /// List of events.
        /// </summary>
        public List<Event> events;

        /// <summary>
        /// Callback currently being called.
        /// </summary>
        public Callback callback;

        /// <summary>
        /// Reference to this element's Animation component.
        /// </summary>
        internal Animation animation { get { return AssertLocal<Animation>("animation"); } }

        /// <summary>
        /// Init.
        /// </summary>
        void Awake() {
            for (int i = 0; i < events.Count; i++) { events[i].Init(this); }
        }

        /// <summary>
        /// Callback called from within animation to send events.
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_args"></param>
        public void OnAnimationEvent(string p_event,List<Argument> p_args) {
            object[] ev_args = new object[p_args.Count];
            for (int i = 0; i < p_args.Count;i++) {
                Argument a = p_args[i];
                switch(a.type) {
                    case Argument.Type.Int:     ev_args[i] = a.aInt;     break;
                    case Argument.Type.Float:   ev_args[i] = a.aFloat;   break;
                    case Argument.Type.String:  ev_args[i] = a.aString;  break;
                    case Argument.Type.Curve:   ev_args[i] = a.aCurve;   break;
                    case Argument.Type.Vector2: ev_args[i] = a.aVector2; break;
                    case Argument.Type.Vector3: ev_args[i] = a.aVector3; break;
                    case Argument.Type.Vector4: ev_args[i] = a.aVector4; break;
                    case Argument.Type.Rect:    ev_args[i] = a.aRect;    break;
                    case Argument.Type.Color:   ev_args[i] = a.aColor;   break;
                    case Argument.Type.Object:  ev_args[i] = a.aObject;  break;                    
                    default: ev_args[i] = null; break;
                }
            }
            Notify(p_event,ev_args);
        }

        /// <summary>
        /// Callback called with default notification and arguments.
        /// </summary>
        /// <param name="p_args"></param>
        public void OnAnimationEvent(List<Argument> p_args) { OnAnimationEvent(notification, p_args); }

        /// <summary>
        /// Update used to check for events.
        /// </summary>
        void Update() {
            for (int i = 0; i < events.Count; i++) events[i].Update();
        }
    }
}