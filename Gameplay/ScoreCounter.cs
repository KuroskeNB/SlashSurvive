using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text scoreText;
    public CurrencyData currencyData;
    public ComboCounter comboCounter;
    public Text overallRecieved;
    public Text scoreRecieved;
    public Text comboRecieved;
    [SerializeField]
    private int score=0;
    private int releasedScore=0;
    // Start is called before the first frame update
    void Start()
    {
        comboCounter=GetComponent<ComboCounter>();
    }
    public int GetScore(){return score;}

    // Update is called once per frame
    void Update()
    {
        scoreText.text=score.ToString();
    }
    public void AddScore(int addScore)
    {
        score+=addScore;
    }
    public void GameEnd(float multiplier=1)
    {
        int RecievedForScore=(int)(score*multiplier);
        int RecievedForCombos=(int)((comboCounter.OneHitKillsCount+comboCounter.NonStopSlashesCount+comboCounter.MultiplyAttacksCount)*multiplier);
        if(currencyData)
        {
            currencyData.silverCoins.coinCount+=(RecievedForScore+RecievedForCombos)-releasedScore;
            releasedScore+=(RecievedForScore+RecievedForCombos)-releasedScore;
            overallRecieved.text=(RecievedForScore+RecievedForCombos).ToString();
            scoreRecieved.text=RecievedForScore.ToString();
            comboRecieved.text=RecievedForCombos.ToString();

            if(PlayerPrefs.GetInt("MaximalScore")<(RecievedForScore+RecievedForCombos))
        {
            Debug.Log("save score");
       PlayerPrefs.SetInt("MaximalScore",(score));
        }
        }
    }
}
