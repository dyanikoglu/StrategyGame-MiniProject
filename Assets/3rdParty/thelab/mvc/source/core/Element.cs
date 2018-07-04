using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace thelab.mvc
{

    /// <summary>
    /// Extension of the element class to handle different BaseApplication types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Element<T> : Element where T : BaseApplication {
        /// <summary>
        /// Returns app as a custom 'T' type.
        /// </summary>
        new public T app { get { return (T)base.app; } }
    }

    /// <summary>
    /// Base class for all MVC related classes.
    /// </summary>
    public class Element : MonoBehaviour {

        /// <summary>
        /// Reference to the root application of the scene.
        /// </summary>
        public BaseApplication app { get { return m_app = Assert<BaseApplication>(m_app, true); } }
        private BaseApplication m_app;

        /// <summary>
        /// Reference to the variable storage.
        /// </summary>
        private Dictionary<string, object> m_store { get { return _store == null ? (_store = new Dictionary<string, object>()) : _store; } }
        private Dictionary<string, object> _store;

        /// <summary>
        /// Finds a instance of 'T' if 'var' is null. Returns 'var' otherwise.
        /// If 'global' is 'true' searches in all scope, otherwise, searches in childrens.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_var"></param>
        /// <param name="p_global"></param>
        /// <returns></returns>
        public T Assert<T>(T p_var, bool p_global=false) where T : Object {
            return p_var == null ? (p_global ? GameObject.FindObjectOfType<T>() : transform.GetComponentInChildren<T>()) : p_var;            
        }

        /// <summary>
        /// Finds a instance of 'T' if the named 'var' is null. Returns the storage['var'] otherwise.
        /// If 'global' is 'true' searches in all scope, otherwise, searches in childrens.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_key"></param>
        /// <param name="p_global"></param>
        /// <returns></returns>
        public T Assert<T>(string p_key, bool p_global = false) where T : Object {
            if (m_store.ContainsKey(p_key)) { return (T)(object)m_store[p_key]; }
            T v = (p_global ? GameObject.FindObjectOfType<T>() : transform.GetComponentInChildren<T>());
            m_store[p_key] = v;
            return v;
        }

        /// <summary>
        /// Finds a instance of 'T' locally if the named 'var' is null. Returns storage['var'] otherwise.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_key"></param>
        /// <returns></returns>
        public T AssertLocal<T>(string p_key) where T : Object {
            if (m_store.ContainsKey(p_key)) { return (T)(object)m_store[p_key]; }
            T v = GetComponent<T>();
            m_store[p_key] = v;
            return v;
        }

        /// <summary>
        /// Finds a instance of 'T' locally if 'var' is null. Returns 'var' otherwise.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_var"></param>
        /// <returns></returns>
        public T AssertLocal<T>(T p_var,string p_store="") where T : Object {
            T v = default(T);
            if (p_store != "") if (m_store.ContainsKey(p_store)) { return (T)(object)m_store[p_store]; }   
            v = p_var == null ? (p_var = GetComponent<T>()) : p_var;
            if (p_store != "") m_store[p_store] = v;
            return v;
        }

        /// <summary>
        /// Finds a instance of 'T' in this element's parent if the named 'var' is null. Returns storage['var'] otherwise.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_key"></param>
        /// <returns></returns>
        public T AssertParent<T>(string p_key) where T : Object {
            if (m_store.ContainsKey(p_key)) { return (T)(object)m_store[p_key]; }
            T v = GetComponentInParent<T>();
            m_store[p_key] = v;
            return v;
        }

        /// <summary>
        /// Finds a instance of 'T' in this element's parent if 'var' is null. Returns 'var' otherwise.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_var"></param>
        /// <returns></returns>
        public T AssertParent<T>(T p_var, string p_store = "") where T : Object {
            T v = default(T);
            if (p_store != "") if (m_store.ContainsKey(p_store)) { return (T)(object)m_store[p_store]; }
            v = p_var == null ? (p_var = GetComponentInParent<T>()) : p_var;
            if (p_store != "") m_store[p_store] = v;
            return v;
        }

        /// <summary>
        /// Retrieves a value from cache or store it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_store"></param>
        /// <param name="p_value"></param>
        /// <returns></returns>
        public T AssertCache<T>(string p_store,T p_value) {
            if (m_store.ContainsKey(p_store)) { return (T)(object)m_store[p_store]; }
            m_store[p_store] = p_value;
            return p_value;
        }

        /// <summary>
        /// Helper method for casting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Cast<T>() { return (T)(object)this; }

        /// <summary>
        /// Searchs for a given element in the dot separated path.
        /// </summary>
        /// <param name="p_path"></param>
        /// <returns></returns>
        public T Find<T>(string p_path) where T : Component {
            List<string> tks = new List<string>(p_path.Split('.'));
            if (tks.Count <= 0) return default(T);
            Transform it = transform;
            while (tks.Count > 0) {
                string p = tks[0];
                tks.RemoveAt(0);
                it = it.Find(p);
                if (it == null) return default(T);
            }
            return it.GetComponent<T>();
        }

        /// <summary>
        /// Navigates the path and finds the first component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_path"></param>
        /// <returns></returns>
        public T AssertFind<T>(string p_path) where T : Component {
            if (m_store.ContainsKey(p_path)) { return (T)(object)m_store[p_path]; }
            T v = Find<T>(p_path);
            m_store[p_path] = v;
            return v;
        }

        /// <summary>
        /// Sends a notification to all controllers passing this instance as 'target' and some 'data'.
        /// </summary>
        /// <param name="p_event"></param>
        /// <param name="p_data"></param>
        public void Notify(string p_event,params object[] p_data) { app.Notify(p_event, this, p_data); }
        
        /// <summary>
        /// Sends a notification to all controllers, after 'delay', passing this instance as 'target' and some 'data'.
        /// </summary>
        /// <param name="p_delay"></param>
        /// <param name="p_event"></param>
        /// <param name="p_data"></param>
        public void Notify(float p_delay,string p_event,params object[] p_data) { app.Notify(p_delay,p_event, this, p_data); }

        /// <summary>
        /// Traverses this element's Transform hierarchy with DFS approach.
        /// </summary>
        /// <param name="p_callback"></param>
        public void Traverse(System.Predicate<Transform> p_callback) {
            OnTraverseStep(transform,p_callback);
        }

        /// <summary>
        /// Traverse one element then its children.
        /// </summary>
        /// <param name="p_target"></param>
        /// <param name="p_callback"></param>
        private void OnTraverseStep(Transform p_target,System.Predicate<Transform> p_callback) {
            if(p_target) if(!p_callback(p_target)) return;
            for(int i=0;i<p_target.childCount;i++) { OnTraverseStep(p_target.GetChild(i),p_callback); }
        }

        /// <summary>
        /// Logs a message using this element information.
        /// </summary>
        /// <param name="p_msg"></param>
        public void Log(object p_msg, int p_verbose = 0) {
            //Only outputs logs equal or bigger than the application 'verbose' level.
            if (p_verbose <= app.verbose) Debug.Log(GetType().Name + "> " + p_msg);
        }

    }

}