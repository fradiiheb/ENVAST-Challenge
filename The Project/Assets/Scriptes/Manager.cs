using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    [Header("Level")]
    public float currentLevel;
    public float maxLevel;
    [SerializeField] private Slider _levelSlider;

    [SerializeField] private Text _levelValueText;

    [Header("Timer")]
    public float timeRemaining;
    public float maxTime;
    public bool gameStoped;
    [SerializeField] private Text _timeValueText;

    [Header("repeat")]
    public int repeatCount;
    public int maxRepeatCount;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Text GameOverText;
    [SerializeField] private Image[] _starsImages;
    [SerializeField] private GameObject WinRepeatButton;
    [SerializeField] private QuizGenerator quizGenerator;
    [SerializeField] private AwnserValidator awnserValidator;
    [SerializeField] private int coins;
    [SerializeField] private int Oldcoins;
    [SerializeField] private Text coinstext;
    // Start is called before the first frame update

    void StartALevel()
    {
        // called when a new level is started
        //reset time & repeatcounter to max
        //reset all start images to white
        timeRemaining = maxTime;
        repeatCount = maxRepeatCount;
        for (int i = 0; i < repeatCount; i++)
        {
            _starsImages[i].color = Color.white;
        }
        //updateLevelBar
        Update_levelBar();
    }
    void Start()
    {
        //call the StartALevel to create a quiz and init coins & oldcoins to 0 
        StartALevel();
        coins=Oldcoins = 0;
    }

    public void Update_levelBar()
    {

        float levelValue = currentLevel / maxLevel;
        _levelSlider.value = levelValue;
        _levelValueText.text = Mathf.FloorToInt(levelValue * 100).ToString() + "%";
    }
    // Update is called once per frame
    void Update()
    {
        //test if we still have time or we are not in a panel(gameStoped)
        if (timeRemaining >= 0&&!gameStoped)
        {
            //determin the timeRemaining in seconds and minutes and display
            float minutes = Mathf.Floor(timeRemaining / 60);
            float seconds = Mathf.RoundToInt(timeRemaining % 60);
            _timeValueText.text = minutes + ":" + seconds;
            //decrement the timeRemaining each second
            timeRemaining -= Time.deltaTime;
        }else if(timeRemaining<=0){
            //when time end call loseLevel fuction then rest arrows
            LoseLevel();
            awnserValidator.Reset();
        }
    }

    public void LoseRepeatClick()
    {
        //when clicked on the repeat button of losepanel decrement repeat count
        //stop game (stop counter)
        //reset timer
        //deactivate losepanel
        repeatCount--;
        gameStoped=false;
        timeRemaining = maxTime;
        LosePanel.SetActive(false);
    }
    public void WinRepeatClick()
    {
        //when clicked on the repeat button of win call startALevel && resume game
        StartALevel();
        gameStoped=false;
        //desactivate winPanel && reset coins to its old coins before wining the round
        winPanel.SetActive(false);
        coins=Oldcoins;
        coinstext.text = coins.ToString();
    }
    public void LoseLevel()
    {
        //called when player lose a round it will stop game and activate the lose panel
        gameStoped=true;
        LosePanel.SetActive(true);
    }
    public void WinLevel()
    {  
        //called when player win a round it will stop game and activate the win panel 
        gameStoped=true;
        winPanel.SetActive(true);
        //activate repeatButton of win panel in case it was desactivated
        WinRepeatButton.SetActive(true);
        //make the starts color yellow based on his repeatCount 
        for (int i = 0; i < repeatCount; i++)
        {
            _starsImages[i].color = Color.yellow;
        }
        //reward base on repeatCount
        if (repeatCount == 3)
        {
            WinRepeatButton.SetActive(false);
            coins += 100;

        }
        else if (repeatCount == 2)
            coins += 60;
        else if (repeatCount == 1)
        {
            coins += 30;
        }
        else
        {
            coins += 10;
        }
        coinstext.text = coins.ToString();
    }
    public void NextBtnClick()
    {
        //when clicked on the next button of win panel call start level and resum game
        StartALevel();
        gameStoped=false;
        //increment currentLevel and change oldcoins to take value of new coins
        currentLevel++;
        Oldcoins=coins;
        coinstext.text = coins.ToString();
        //crate a new quiz
        quizGenerator.InstansiateQuiz();
        //desactivate winPanel
        winPanel.SetActive(false);
    }

    public void WinGame(){
        //called when all rounds are compleated will stop the game ans activate GameOverPanel
       GameOverPanel.SetActive(true);
       gameStoped=true;
       GameOverText.text="Congaguations you win with score : "+coins.ToString();
    }
    
}
