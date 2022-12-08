using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
public class ArrowDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

   private CanvasGroup canvasGroup;
    public Canvas canvas;
    private RectTransform rectTransform;
    public UILineRenderer arrowLine;
   Vector2 startPosition, mouseWorld;
   public ArrowSlot arrowSlot = null;
   public List<Vector2> pointList = new List<Vector2>();
    public bool doneDraging = false;
    public int CorrectIndex;
    public Text AnimalNameTex;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
       

    }

     private void Start()
    {
        //save position of dragable object
        mouseWorld = new Vector3();
        startPosition = rectTransform.anchoredPosition;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }


      public void OnBeginDrag(PointerEventData eventData)
    {
        //test if the dragable object is already dragged in slot
        if (arrowSlot != null)
        {
            //remove the the slot from dragable object
            arrowSlot.haveItem = false;
            arrowSlot = null;
        }
        //make it a bit invisible and blocksRaycasts from the canvasGroup to false
        //so that when the dragable object is dragged over other object it wont reset
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        //clear the arrowLine
         pointList.Clear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //the position of the dragged object will take mouseposition and taking into account tha canvas scaleFactor
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //enable the ArrowLine and 
        arrowLine.enabled = true;
        //call the drawArrow function to draw the arrow
        DrawArrow();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Remake the object visible and blocksRaycasts from the canvasGroup to true
        //so that when the dragable object is interactable
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //if we drop the dragable object outside of a slot or occupied slot it will just call initArrow function
         if (!doneDraging)
        {
            InitArrow();
            return;

        }
        //reset doneDraging to false and make the second vector of arrowLine take the value if the dragged object
        doneDraging = false;
        pointList[1] = rectTransform.anchoredPosition;
        arrowLine.Points = pointList.ToArray();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
    public void DrawArrow()
    {
        //vector2 mouseWorld take position of the dragged object
        mouseWorld = rectTransform.anchoredPosition;

        //int arrowLine point for the first time
        if (pointList.Count < 1)
        {
            pointList.Add(startPosition);
            arrowLine.Points = pointList.ToArray();
            pointList.Add(mouseWorld);
            return;
        }
        //for each mouvement the second list of points take position of the dragged object
        pointList[1] = mouseWorld;
        arrowLine.Points = pointList.ToArray();


       
    }
     public void InitArrow()
    {
        //reset dragged object position to its original position
        //clear the arrowline list of points abd disable it
        rectTransform.anchoredPosition = startPosition;
        pointList.Clear();
        arrowLine.enabled = false;
    }
}
