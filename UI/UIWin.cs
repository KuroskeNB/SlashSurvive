using UnityEngine;
using UnityEngine.UI;

public class UIWin : MonoBehaviour
{
    public WaveManager waveManager;
    public Button StartNewButton;
    // Start is called before the first frame update
    void Start()
    {
        StartNewButton.onClick.AddListener(OnStartNewButtonClicked);
    }

    // Update is called once per frame
    void OnStartNewButtonClicked()
    {
     waveManager.StartLevel();
     gameObject.SetActive(false);
    }

}
