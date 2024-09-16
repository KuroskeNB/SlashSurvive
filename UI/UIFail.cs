using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIFail : MonoBehaviour
{
    public Button RestartButton;
    public Button MenuButton;
    // Start is called before the first frame update
    void Start()
    {
       RestartButton.onClick.AddListener(OnRestartClicked);
       MenuButton.onClick.AddListener(OnMenuButtonClicked);
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
