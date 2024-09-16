using UnityEngine;
using System;

public class BonusManager : MonoBehaviour
{
    private DateTime endTime;
    private TimeSpan timerDuration; 
    public BonusObject bonusObj;
    public Sprite lockImage;
    private const string LastRewardTimeKey = "LastRewardTime";
    public const float RewardIntervalHours = 0.01f;

    void Start()
    {
        bonusObj=GetComponent<BonusObject>();

        LoadTimer();

        if(Time.timeScale!=1f)
        Time.timeScale=1f;

        if(timerDuration == TimeSpan.Zero)
        bonusObj.timerText.text ="You can take \n the reward";
    }
    void Update()
    {
        UpdateTimer();
    }
    void OnDestroy()
    {
        SaveTimer();
    }

    void GiveReward()
    {
        bonusObj.IsUnlocked=true;
        bonusObj.timerText.text ="You can take \n the reward";
        bonusObj.SetBonus();
        PlayerPrefs.DeleteKey("TimerDuration");
        PlayerPrefs.DeleteKey("EndTime");
    }
    private void LoadTimer()
    {
        Debug.Log("load timer");
        // Загружаем сохраненные данные
        string savedEndTime = PlayerPrefs.GetString("EndTime", "");
        double savedDuration = PlayerPrefs.GetFloat("TimerDuration",0);

        if (!string.IsNullOrEmpty(savedEndTime))
        {
            endTime = DateTime.Parse(savedEndTime);
            timerDuration = TimeSpan.FromSeconds(savedDuration);
            Debug.Log(timerDuration);
            // Рассчитываем оставшееся время
            TimeSpan remainingTime = endTime - DateTime.Now;
            if (remainingTime.TotalSeconds > 0)
            {
                bonusObj.IsUnlocked=false;
                timerDuration = remainingTime;
            }
            else
            {
                Debug.Log("lol");
                timerDuration = TimeSpan.Zero;
                GiveReward();
            }
        }
        else
        {
            // Устанавливаем начальное время отсчета (например, 5 минут)
            timerDuration = TimeSpan.FromMinutes(30);
            Debug.Log(timerDuration);
            endTime = DateTime.Now + timerDuration;
        }
    }
    private void SaveTimer()
    {
        // Сохраняем время окончания и длительность таймера
        PlayerPrefs.SetString("EndTime", endTime.ToString());
        PlayerPrefs.SetFloat("TimerDuration", (float)timerDuration.TotalSeconds);
        PlayerPrefs.Save();
    }
    private void UpdateTimer()
    {
        if(bonusObj.IsRecieved)
        {
        LoadTimer();
        bonusObj.IsRecieved=false;
        }
        if (timerDuration.TotalSeconds > 0)
        {
            if(lockImage)
            bonusObj.bonusImage.sprite=lockImage;
           // Debug.Log(timerDuration);
            timerDuration -= TimeSpan.FromSeconds(Time.deltaTime);
           // Debug.Log(Time.timeScale);
            bonusObj.timerText.text = $"Time remaining: \n {timerDuration.Minutes}:{timerDuration.Seconds}";
            if (timerDuration.TotalSeconds <= 0)
            {
                timerDuration = TimeSpan.Zero;
                GiveReward();
                // Таймер достиг нуля, выполните нужные действия здесь
                Debug.Log("Timer finished!");
            }
        }
        
        // Обновляем отображение таймера (пример)
       // bonusObj.timerText.text = $"Time remaining: {timerDuration.Minutes}:{timerDuration.Seconds}";
    }
}
