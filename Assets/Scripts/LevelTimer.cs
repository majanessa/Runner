using UnityEngine;
using UnityEngine.UI;

public class LevelTimer: MonoBehaviour
{
    [SerializeField] private float timeStart = 30;
    [SerializeField] private Text timerText;

    private void Start()
    {
        timerText.text = timeStart.ToString();
    }

    private void Update()
    {
        timeStart -= Time.deltaTime;
        timerText.text = Mathf.Round(timeStart).ToString();
        if (timeStart <= 0)
        {
            GameService.Instance.Win();
        }
    }
}
