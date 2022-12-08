using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuizGenerator : MonoBehaviour
{
    [SerializeField] List<Animal> AllListanimals = new List<Animal>();
    [SerializeField] List<Animal> selectedAnimals = new List<Animal>();

    [SerializeField] List<ArrowDragDrop>AnimalArrowsDragDrops = new List<ArrowDragDrop>();
    [SerializeField] List<ArrowSlot> AnimalArrowsSlots = new List<ArrowSlot>();
    // Start is called before the first frame update
    void Start()
    {
        InstansiateQuiz();
        
    }

    public void InstansiateQuiz(){
        int animalindex = 0;
        shuffleDragDrop();
        shuffleSlots();
        for (int i = 0; i < 3; i++)
        {
            int timerOut=0;
            do
            {
                animalindex = Random.Range(0, AllListanimals.Count);
                //To Exit loop in case of infinite loop
                timerOut++;
                if(timerOut>150)
                return;
            }
            while (selectedAnimals.Contains(AllListanimals[animalindex]));

            selectedAnimals.Add(AllListanimals[animalindex]);
        }
        int counter=0;
        foreach(ArrowSlot animalSlot in AnimalArrowsSlots){
            animalSlot.AnimalImage.sprite=selectedAnimals[counter].AnimalImage;
            animalSlot.answer=selectedAnimals[counter].numero;
            AnimalArrowsDragDrops[counter].AnimalNameTex.text=selectedAnimals[counter].AnimalName;
            AnimalArrowsDragDrops[counter].CorrectIndex=selectedAnimals[counter].numero;
            counter++;
        }
        foreach(Animal selectedAnimal in selectedAnimals){
        AllListanimals.Remove(selectedAnimal);
        }
        selectedAnimals.Clear();
    }


   void shuffleDragDrop(){
    for (int i = 0; i < AnimalArrowsDragDrops.Count; i++) {
         ArrowDragDrop temp = AnimalArrowsDragDrops[i];
         int randomIndex = Random.Range(i, AnimalArrowsDragDrops.Count);
         AnimalArrowsDragDrops[i] = AnimalArrowsDragDrops[randomIndex];
         AnimalArrowsDragDrops[randomIndex] = temp;
     }
   }
    void shuffleSlots(){
    for (int i = 0; i < AnimalArrowsSlots.Count; i++) {
         ArrowSlot temp = AnimalArrowsSlots[i];
         int randomIndex = Random.Range(i, AnimalArrowsSlots.Count);
         AnimalArrowsSlots[i] = AnimalArrowsSlots[randomIndex];
         AnimalArrowsSlots[randomIndex] = temp;
     }
   }
    
}
