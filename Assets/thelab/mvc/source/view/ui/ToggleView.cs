using UnityEngine;
using System.Collections;
using thelab.mvc;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace thelab.mvc
{
    

    /// <summary>
    /// Base class for all Toggle features related classes.
    /// </summary>
    public class ToggleView : ButtonView 
    {
        
        /// <summary>
        /// Reference to the component.
        /// </summary>
        public Toggle toggle;
        
        /// <summary>
        /// CTOR.
        /// </summary>
        protected void Awake() {
            toggle  = GetComponentInChildren<Toggle>();
            if(toggle)toggle.onValueChanged.AddListener(OnChange);
        }

        /// <summary>
        /// Callback for value change on component.
        /// </summary>
        /// <param name="v"></param>
        virtual protected void OnChange(bool v) {                    
            Notify(notification+"@change");
        }
        
    }

}