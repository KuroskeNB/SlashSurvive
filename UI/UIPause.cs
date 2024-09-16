using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    public Button ResumeButton;
    public Button RestartButton;
    public Button MenuButton;
    // Start is called before the first frame update
    void Start()
    {
       ResumeButton.onClick.AddListener(OnResumeClicked);
       RestartButton.onClick.AddListener(OnRestartClicked);
       MenuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    void OnResumeClicked()
    {
    if(Time.timeScale!=1f)
    Time.timeScale=1f;
     gameObject.SetActive(false);
    }
    void OnRestartClicked()
    {
     gameObject.SetActive(false);
     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnMenuButtonClicked()
    {
      SceneManager.LoadScene("MenuScene");
    }
}
