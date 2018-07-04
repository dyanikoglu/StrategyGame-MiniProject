using UnityEngine;
using System.Collections;
using thelab.mvc;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace thelab.mvc
{
    

    /// <summary>
    /// Base class for all Button features related classes.
    /// </summary>
    public class InputFieldView : ButtonView 
    {  
        /// <summary>
        /// Reference to the input field.
        /// </summary>
        public InputField field;
        
        /// <summary>
        /// CTOR.
        /// </summary>
        protected void Awake() {
            field = GetComponentInChildren<InputField>();
            if(field) {
                field.onValueChanged.AddListener(OnChange);
            }
        }

        /// <summary>
        /// Callback for value change.
        /// </summary>
        /// <param name="v"></param>
        virtual protected void OnChange(string v) {            
            Notify(notification+"@change");
        }


    }
    
}

