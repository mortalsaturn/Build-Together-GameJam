using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject badScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= 5f)
        {
            badScreen.SetActive(true);
        }
    }
}
