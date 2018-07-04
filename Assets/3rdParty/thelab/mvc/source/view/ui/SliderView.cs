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
    public class SliderView : ButtonView 
    {
        
        /// <summary>
        /// Reference to the slider.
        /// </summary>
        public Slider slider;

        /// <summary>
        /// Reference to the text field if any.
        /// </summary>
        public Text field;

        /// <summary>
        /// Unit prefix.
        /// </summary>
        public string unit;
        
        /// <summary>
        /// String format.
        /// </summary>
        public string format = "0.00";
        
        /// <summary>
        /// CTOR.
        /// </summary>
        protected void Awake() {
            field  = GetComponentInChildren<Text>();
            slider = GetComponent<Slider>();
            if(slider) {
                slider.onValueChanged.AddListener(OnChange);
                UpdateField();
            }
        }

        /// <summary>
        /// Callback for value change on slider.
        /// </summary>
        /// <param name="v"></param>
        virtual protected void OnChange(float v) {  
            UpdateField();          
            Notify(notification+"@change");
        }

        /// <summary>
        /// Updates the text field.
        /// </summary>
        private void UpdateField() {
            if(field) field.text = slider.value.ToString(format)+unit;
        }


    }

}