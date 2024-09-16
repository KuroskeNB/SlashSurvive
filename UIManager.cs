using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public WaveManager waveManager;
    public GameObject notifications;
    public Button StartNewButton;
    public Button GamePauseButton;
    public Canvas LevelPauseUI;
    //public Canvas LevelFinishUI;
    public Canvas LevelFailedUI;
    void Start()
    {
       Time.timeScale=1f;
       
        GamePauseButton.onClick.AddListener(ShowPauseMenu);
        Debug.Log("ui manager srart");
        LevelPauseUI.gameObject.SetActive(false);
        //LevelFinishUI.gameObject.SetActive(false);
        LevelFailedUI.gameObject.SetActive(false);
        waveManager.onLevelEnd+=LevelEnd;
        waveManager.onNewChanceEvent+=StatNewChance;
    }

    void StatNewChance()
    {
      Time.timeScale=1f;
      LevelPauseUI.gameObject.SetActive(false);
      LevelFailedUI.gameObject.SetActive(false);
    }
    void LevelEnd(bool win)
    {
     ShowFailMenu();
    }
    public void ShowPauseMenu()
    {
     LevelPauseUI.gameObject.SetActive(true);

     Time.timeScale=(Time.timeScale<1f)?1f:0f;
    }
    public void ShowFailMenu()
    {
     LevelFailedUI.gameObject.SetActive(true);
             Debug.Log("show fail menu");

    if(Time.timeScale!=0f)
    Time.timeScale=0f;
    }

    public void Notification(GameObject notify)
    {
      Debug.Log("notify");
      Instantiate(notify,notifications.transform.position,notifications.transform.rotation);
    }
}
