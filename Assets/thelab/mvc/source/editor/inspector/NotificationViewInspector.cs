using UnityEngine;
using System.Collections;
using UnityEditor;
using thelab.mvc;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace thelab.mvc {

    [CustomEditor(typeof(NotificationView), true)]
    public class NotificationViewInspector : Editor {
        /// <summary>
        /// Reference to the "N" global notification class, if any.
        /// </summary>
        static Type notification_class {
            get
            {
                if (m_n != null) return m_n;
                if (m_n_searched) return m_n;
                m_n_searched = true;
                Assembly[] al = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly a in al)
                {
                    Type[] tl = a.GetTypes();
                    foreach (Type t in tl) if (t.Name == "N") { return m_n = t; }
                }
                return null;
            }
        }
        static private Type m_n;
        static private bool m_n_searched = false;

        /// <summary>
        /// Internals
        /// </summary>
        static private BindingFlags rf = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

        /// <summary>
        /// View target of this inspector.
        /// </summary>
        new public NotificationView target { get { return (NotificationView)base.target; } }

        /// <summary>
        /// Draws the GUI
        /// </summary>
        public override void OnInspectorGUI() {

            Type t = notification_class;

            if (t == null) {
                base.OnInspectorGUI();
                return;
            }

            Type[] cl = t.GetNestedTypes();

            int oid = target.GetInstanceID();

            bool is_open = EditorPrefs.GetBool("viewinspector-" + oid, false);
            bool next_is_open = false;
            if (next_is_open = EditorGUILayout.Foldout(is_open, "Notifications")) {

                for (int i = 0; i < cl.Length; i++) {
                    Type ct = cl[i];
                    List<string> nl = GetNotifications(ct);
                    int selected_notification = nl.IndexOf(target.notification);
                    int next_selection = EditorGUILayout.Popup(ct.Name, selected_notification, nl.ToArray());
                    if (next_selection != selected_notification) {
                        string n = nl[next_selection];
                        Undo.RecordObject(target, "Notification Change");
                        target.notification = n;
                    }
                }
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Clear", GUILayout.Width(120f))) {
                    Undo.RecordObject(target, "Notification Clear");
                    target.notification = "";
                }
                EditorGUILayout.EndHorizontal();
            }
            if (next_is_open != is_open) EditorPrefs.SetBool("viewinspector-" + oid, next_is_open);
            base.OnInspectorGUI();

        }

        /// <summary>
        /// Searches the properties and return a list of notifications.
        /// </summary>
        /// <param name="p_type"></param>
        /// <returns></returns>
        public List<string> GetNotifications(Type p_type) {

            Type t = p_type;

            FieldInfo[] fl = t.GetFields(rf);
            List<string> res = new List<string>();

            foreach (FieldInfo it in fl) {
                string v = (string)it.GetValue(t);
                if (v.IndexOf("@") >= 0) v = v.Split('@')[0];
                v = v.Trim();
                res.Add(v);
            }

            res.Sort();

            if (res.Count >= 2) {
                for (int i = 0; i < res.Count; i++)
                    for (int j = i + 1; j < res.Count; j++) {
                        if (res[i] == res[j]) {
                            res.RemoveAt(j--);
                        }
                    }
            }
            return res;
        }
    }
}