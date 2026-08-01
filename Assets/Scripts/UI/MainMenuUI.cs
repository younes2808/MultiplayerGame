using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            //Click code
            Loader.Load(Loader.Scene.LobbyScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            //Click code
            Application.Quit();
        });
        //resetting animations
        Time.timeScale = 1f;
    }


}
