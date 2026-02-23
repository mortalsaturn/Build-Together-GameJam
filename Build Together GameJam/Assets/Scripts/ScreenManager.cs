using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject badScreen;
    float elapsedSecs = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        elapsedSecs += Time.deltaTime;
        if (elapsedSecs >= 5f)
        {
            badScreen.SetActive(true);
        }
    }
}
