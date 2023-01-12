using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameService : MonoBehaviour
{
    public static GameService Instance;
    
    [SerializeField] private GameObject gamePlay;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private Text timerText;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        gamePlay.SetActive(true);
        startButton.SetActive(false);
        timerText.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gamePlay.SetActive(false);
        restartButton.SetActive(true);
        timerText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Win()
    {
        RestartGame();
    }
}
