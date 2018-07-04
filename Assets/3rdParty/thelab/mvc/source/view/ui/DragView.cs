using UnityEngine;
using System.Collections;
using thelab.mvc;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace thelab.mvc
{

    /// <summary>
    /// Extension to support generic applications.
    /// </summary>
    public class DragView<T> : DragView where T : BaseApplication {
        /// <summary>
        /// Returns app as a custom 'T' type.
        /// </summary>
        new public T app { get { return (T)base.app; } }
    }

    /// <summary>
    /// Base class for all Mouse Drag features related classes.
    /// </summary>
    public class DragView : NotificationView, IBeginDragHandler, IDragHandler, IEndDragHandler {
        /// <summary>
        /// Flag that indicates there is a dragging occuring.
        /// </summary>
        public bool drag;

        /// <summary>
        /// Drag position.
        /// </summary>
        public Vector2 position;
        
        /// <summary>
        /// Dragged target.
        /// </summary>
        public GameObject target;

        /// <summary>
        /// Flag that indicates that a preview will appear upon dragging.
        /// </summary>
        public bool usePreview = true;

        /// <summary>
        /// Flag that indicates the original should be hidden.
        /// </summary>
        public bool hide = false;

        /// <summary>
        /// Alpha of the preview.
        /// </summary>
        public float alpha = 0.5f;

        /// <summary>
        /// Reference to this dragdrop preview.
        /// </summary>
        public GameObject preview;

        /// <summary>
        /// Internals;
        /// </summary>
        private RectTransform m_canvas;
        private CanvasGroup m_hide_group;
        private float m_initial_alpha;
        private Vector2 m_delta;

        /// <summary>
        /// Init.
        /// </summary>
        void Start() {
            drag = false;
            position = new Vector2();
            m_delta = new Vector2();
            Transform t = transform.parent;
            Canvas c=null;
            while(t) {
                c = t.GetComponent<Canvas>();
                if (c) break;
                t = t.parent;
            }
            if (c) m_canvas = c.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Handles the drag callback.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData e) {
            if (!drag) return;
            Notify(notification + "@drag",e);
            position = e.position;
            if(preview)
            {
                RectTransform t = preview.GetComponent<RectTransform>();
                t.position = position+m_delta;                
            }
        }

        /// <summary>
        /// Handles the drag start.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData e) {

            Notify(notification + "@drag-starts",e);
            target = e.pointerDrag;
            
            drag = true;

            if (target) {

                RectTransform r = target.GetComponent<RectTransform>();
                
                Vector2 size = r.rect.size;

                preview      = Instantiate(target);
                preview.name = "drag-preview";
                r = preview.GetComponent<RectTransform>();

                DragView dv = preview.GetComponent<DragView>();
                if (dv) dv.enabled = false;

                //Log(size.ToString());
                                
                r.SetParent(m_canvas,true);


                //r.anchorMax = new Vector2(0.5f, 0.5f);
                //r.anchorMin = new Vector2(0.5f, 0.5f);
                                
                r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(size.x));
                r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(size.y));

                CanvasGroup cg = preview.GetComponent<CanvasGroup>();
                if (!cg) cg = preview.AddComponent<CanvasGroup>();
                cg.alpha = alpha;
                cg.blocksRaycasts = false;
                
                if(hide) {
                    cg = target.GetComponent<CanvasGroup>();
                    if (!cg)
                    {
                        cg = target.AddComponent<CanvasGroup>();                        
                    }
                    m_hide_group = cg;
                    m_initial_alpha = m_hide_group.alpha;
                    m_hide_group.alpha = 0f;
                }

            }
        }

        /// <summary>
        /// Handles the drag end.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData e) {            
            EndDrag();
        }

        /// <summary>
        /// Finishes the drag.
        /// </summary>
        public void EndDrag() {   

            if (preview){ 
                Destroy(preview);
                preview = null;
            }

            ShowTarget();
            //Invoke("ShowTarget", 3f/60f);
            
            if (!drag) return;
            drag = false;
            Notify(notification + "@drag-end");

        }

        /// <summary>
        /// Undo the the hide operation if any.
        /// </summary>
        void ShowTarget() {

            if (hide) {
                
                CanvasGroup cg = target.GetComponent<CanvasGroup>();

                if (!cg) {
                    if (m_hide_group) {
                        m_hide_group.alpha = 1f;
                        Destroy(m_hide_group);
                    }
                }
                else {
                    cg.alpha = m_initial_alpha;
                }                
            }
        }

        /// <summary>
        /// Updates this view's drag state.
        /// </summary>
        void Update() {
            if(drag) {
                if(Input.GetKeyDown(KeyCode.Escape)) EndDrag();                
            }
        }


    }

}