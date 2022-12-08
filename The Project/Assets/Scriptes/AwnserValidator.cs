using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AwnserValidator : MonoBehaviour
{
    List<ArrowDragDrop> _draggedObjects = new List<ArrowDragDrop>();
    public ArrowDragDrop[] gameObjects = null;
    [SerializeField] Manager manager;
    [SerializeField] Button btn;
    private void Start()
    {
        // make a list of type ArrowDragDrop
        gameObjects = FindObjectsOfType<ArrowDragDrop>();

        foreach (ArrowDragDrop g in gameObjects)
        {
            if (g != null)
            {
                _draggedObjects.Add(g);

            }
        }
    }
    public void OnClick()
    {
        //when clicked browse through the the list draged obbject 
        foreach (ArrowDragDrop dragged in _draggedObjects)
        {
            //check if the dragged object is empty
            if (dragged.arrowSlot == null)
            {
                //call lose function of the manager and call the reset function then exit function 
                Debug.Log("Lose");
                manager.LoseLevel();
                Reset();
                return;
            }
            //compare the dragged object Correct Index variable with awnserInt variable that it is dragged into
            if (dragged.CorrectIndex != dragged.arrowSlot.answer)
            {
                //if they are different then call lose function of the manager and call the reset function then exit function 
                Debug.Log("Lose");
                manager.LoseLevel();
                Reset();
                return;
            }

        }
        //if we are in this level that means that all dragged objects are in thier correct slot
        //desactivate the validation button so that by any chance the user keep clicking it won't function so the score won't multiply
        btn.interactable = false;
        Debug.Log("win");
        //check if we reatched the final level call function WinGame in manager taht will open gameover window
        if (manager.currentLevel == manager.maxLevel)
            manager.WinGame();
        else//if not then call function WinLevel in manager taht will open WinLevel window
            manager.WinLevel();
        //call reset function
        Reset();
    }
    public void Reset()
    {
        //browse through the the list draged obbject 
        foreach (ArrowDragDrop dragged in _draggedObjects)
        {
            //call initArrow function on the dragged object so it will take it's initial position and reset line 
            dragged.InitArrow();
            //if the dragged object is in a slot make haveItem to false and remove that dragged object from the slot
            if (dragged.arrowSlot != null)
            {
                dragged.arrowSlot.haveItem = false;
                dragged.arrowSlot = null;
            }
        }
        //remake the button interactable
        btn.interactable = true;
    }
}
