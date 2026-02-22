using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 10f;

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] GameObject timeUpPanel;
    [SerializeField] GameObject startButton;

    bool timerRunning = false;
    bool timerEnded = false;

    void Start()
    {
        currentTime = startingTime;
        countdownText.text = startingTime.ToString("0");
        timeUpPanel.SetActive(false);
    }

    void Update()
    {
        if (!timerRunning || timerEnded) return;

        currentTime -= Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            timerEnded = true;
            StartCoroutine(TimeUpSequence());
        }
    }
    
    public void StartTimer()
    {
        timerRunning = true;

        if (startButton != null)
            startButton.SetActive(false);
    }

    IEnumerator TimeUpSequence()
    {
        timeUpPanel.SetActive(true);
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}