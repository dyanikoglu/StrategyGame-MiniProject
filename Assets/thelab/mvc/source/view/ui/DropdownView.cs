using UnityEngine;
using System.Collections;
using thelab.mvc;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace thelab.mvc
{
    

    /// <summary>
    /// Base class for all dropdown features related classes.
    /// </summary>
    public class DropdownView : ButtonView 
    {
        
        /// <summary>
        /// Reference to the component.
        /// </summary>
        public Dropdown dropdown;
        
        /// <summary>
        /// CTOR.
        /// </summary>
        protected void Awake() {
            dropdown  = GetComponentInChildren<Dropdown>();
            if(dropdown)dropdown.onValueChanged.AddListener(OnChange);
        }

        /// <summary>
        /// Callback for value change on component.
        /// </summary>
        /// <param name="v"></param>
        virtual protected void OnChange(int v) {                    
            Notify(notification+"@change");
        }
        
    }

}