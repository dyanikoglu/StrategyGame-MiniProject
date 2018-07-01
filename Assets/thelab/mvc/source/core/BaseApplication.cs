using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;

#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif


namespace thelab.mvc
{
    /// <summary>
    /// Extension of the BaseApplication class to handle different types of Model View Controllers.
    /// </summary>
    /// <typeparam name="M"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="C"></typeparam>
    public class BaseApplication<M, V, C> : BaseApplication
        where M : Element
        where V : Element
        where C : Element
    {
        /// <summary>
        /// Model reference using the new type.
        /// </summary>
        new public M model { get { return (M)(object)base.model; } }

        /// <summary>
        /// View reference using the new type.
        /// </summary>
        new public V view { get { return (V)(object)base.view; } }

        /// <summary>
        /// Controller reference using the new type.
        /// </summary>
        new public C controller { get { return (C)(object)base.controller; } }
    }

    /// <summary>
    /// Root class for the scene's scripts.
    /// </summary>
    public class BaseApplication : Element
    {
        /// <summary>
        /// Arguments to be passed between scenes.
        /// </summary>        
        static List<string> __args;

        /// <summary>
        /// Flag that indicates the first scene was loaded.
        /// </summary>
        static bool m_first_scene;

        /// <summary>
        /// Little static init.
        /// </summary>
        static BaseApplication() { m_first_scene = true; }

        /// <summary>
        /// Arguments passed between scenes.
        /// </summary>
        public List<string> args { get { return __args==null ? (new List<string>()) : __args; } }

        /// <summary>
        /// Verbose Level.
        /// </summary>
        public int verbose;

        /// <summary>
        /// Fetches the root Model instance.
        /// </summary>
        public Model model { get { return m_model = Assert<Model>(m_model); } }
        private Model m_model;

        /// <summary>
        /// Fetches the root View instance.
        /// </summary>
        public View view { get { return m_view = Assert<View>(m_view); } }
        private View m_view;

        /// <summary>
        /// Fetches the root Controller instance.
        /// </summary>
        public Controller controller { get { return m_controller = Assert<Controller>(m_controller); } }
        private Controller m_controller;

        /// <summary>
        /// Wrapper for the current scene's id.
        /// </summary>
        public int levelId {
            get {
                #if UNITY_5_3_OR_NEWER
                return SceneManager.GetActiveScene().buildIndex;
                #else
                return Application.loadedLevel;
                #endif
            }
        }

        /// <summary>
        /// Wrapper for the current scene's name.
        /// </summary>
        public string levelName {
            get {
                #if UNITY_5_3_OR_NEWER
                return SceneManager.GetActiveScene().name;
                #else
                return Application.loadedLevelName;
                #endif
            }
        }

        /// <summary>
        /// Async data structures.
        /// </summary>
        private List<UnityEngine.AsyncOperation> m_async_loads { get { return __async_loads == null ? (__async_loads = new List<UnityEngine.AsyncOperation>()) : __async_loads; } }
        private List<UnityEngine.AsyncOperation> __async_loads;

        private List<string> m_async_args { get { return __async_args == null ? (__async_args = new List<string>()) : __async_args; } }
        private List<string> __async_args;

        /// <summary>
        /// Initialization.
        /// </summary>
        virtual protected void Start() {
            __async_loads = new List<UnityEngine.AsyncOperation>();
            __async_args = new List<string>();
            if (m_first_scene) { m_first_scene = false; OnLevelWasLoaded(levelId); }
            Notify("scene.start", new object[] { levelName, levelId });
        }

        /// <summary>
        /// Capture the level loaded event and notify controllers for 'starting' purposes.
        /// </summary>
        /// <param name="p_level"></param>
        private void OnLevelWasLoaded(int p_level) {
            Notify("scene.load", new object[] { levelName, levelId });
        }

        /// <summary>
        /// Notifies all application's controllers informing who's the 'target' and passing some 'data'.
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        /// <param name="p_data"></param>
        public void Notify(string p_event, Object p_target, params object[] p_data) {                        
            Log(p_event + " [" + p_target + "]", 6);
            Traverse(delegate(Transform it) {
                Controller[] list = it.GetComponents<Controller>();
                for (int i = 0; i < list.Length; i++) list[i].OnNotification(p_event, p_target, p_data);
                return true;
            });
        }

        /// <summary>
        /// Notifies all application's controllers informing who's the 'target'.
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        public void Notify(string p_event, Object p_target) { Notify(p_event, p_target,new object[]{}); }

        /// <summary>
        /// Notifies all application's controllers informing who's the 'target' after 'delay' in seconds and passing some 'data'.
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        /// <param name="p_data"></param>
        public void Notify(float p_delay,string p_event, Object p_target,params object[] p_data) {            
            StartCoroutine(TimedNotify(p_delay,p_event,p_target,p_data));
        }

        /// <summary>
        /// Internal Notify to help timed notifications.
        /// </summary>
        /// <param name="p_delay"></param>
        /// <param name="p_event"></param>
        /// <param name="p_target"></param>
        /// <param name="p_data"></param>
        /// <returns></returns>
        private IEnumerator TimedNotify(float p_delay, string p_event, Object p_target,params object[] p_data) {
            yield return new WaitForSeconds(p_delay);
            Notify(p_event, p_target, p_data);
        }
        
        /// <summary>
        /// Adds a new scene by name. An async flag can control the load type.
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_async"></param>
        /// <param name="p_args"></param>
        public void SceneAdd(string p_name, bool p_async, params string[] p_args) {
            if (p_async) { StartCoroutine(SceneLoadAsync(p_name, true, p_args)); }
            else {
                __args = new List<string>(p_args);
                #if UNITY_5_3_OR_NEWER
                SceneManager.LoadScene(p_name, LoadSceneMode.Additive);
                #else
                Application.LoadLevelAdditive(p_name);
                #endif
            }
        }

        /// <summary>
        /// Adds a new scene.
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_args"></param>
        public void SceneAdd(string p_name,params string[] p_args) { SceneAdd(p_name, false, p_args); }

        /// <summary>
        /// Loads a new scene by name. A flag indicating if the load must be async can be informed.
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_async"></param>
        /// <param name="p_args"></param>
        public void SceneLoad(string p_name,bool p_async,params string[] p_args) {
            if (p_async) { StartCoroutine(SceneLoadAsync(p_name,false,p_args)); }
            else {
                __args = new List<string>(p_args);
                #if UNITY_5_3_OR_NEWER
                SceneManager.LoadScene(p_name, LoadSceneMode.Single);
                #else
                Application.LoadLevel(p_name);
                #endif
            }
        }

        /// <summary>
        /// Loads a new scene by name.
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_args"></param>
        public void SceneLoad(string p_name,params string[] p_args) { SceneLoad(p_name, false, p_args); }

        /// <summary>
        /// Internal method for async load level.
        /// </summary>
        /// <param name="p_name"></param>
        /// <param name="p_args"></param>
        /// <returns></returns>
        private IEnumerator SceneLoadAsync(string p_name,bool p_additive,params string[] p_args) {
            //float p = 0f;
            UnityEngine.AsyncOperation async = null;
            string ev = "";
            
            if(p_additive) {
                ev = "scene.add.progress";
                #if UNITY_5_3_OR_NEWER
                async = SceneManager.LoadSceneAsync(p_name,LoadSceneMode.Additive);
                #else
                async = Application.LoadLevelAdditiveAsync(p_name);
                #endif
            }
            else {
                ev = "scene.load.progress";
                #if UNITY_5_3_OR_NEWER
                async = SceneManager.LoadSceneAsync(p_name,LoadSceneMode.Single);
                #else
                async = Application.LoadLevelAsync(p_name);
                #endif                
            }

            m_async_loads.Add(async);
            m_async_args.Add(p_name + "~" + ev);

            yield return async;
            __args = new List<string>(p_args);
        }

        /// <summary>
        /// Update some internal states.
        /// </summary>
        void Update() {
            for(int i=0;i<m_async_loads.Count;i++) {
                
                UnityEngine.AsyncOperation async = m_async_loads[i];

                if (async != null) {
                    string args = m_async_args[i];
                    string s_name = args.Split('~')[0];
                    string s_ev = args.Split('~')[1];
                    if (s_ev != "") Notify(s_ev, new object[] { s_name, async.progress });
                    if (async.progress >= 1.0) m_async_loads[i] = null;                    
                }
                else {
                    m_async_loads.RemoveAt(i--);
                    m_async_args.RemoveAt(i--);
                }
            }
        }
    }
}

#pragma warning restore 0618