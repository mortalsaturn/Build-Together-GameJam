using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KeywordManager : MonoBehaviour
{
    public List<KeywordData> keywords = new List<KeywordData>();
    public TextMeshProUGUI skillsText;
    public TextMeshProUGUI scheduleText;
    public GameObject appPanel;
    public GameObject spawners;
    bool finishedApp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (
                hit.collider != null
                && hit.collider.TryGetComponent(out Keyword keyword)
                && !finishedApp
            )
            {
                keywords.Add(hit.collider.gameObject.GetComponent<Keyword>().ToData());
                switch (keywords.Count)
                {
                    case > 3:
                        appPanel.transform.position = new Vector3(0f, 0.4f, 90f);
                        scheduleText.text += " " + keywords[^1].text + " |";
                        if (keywords.Count == 6)
                        {
                            finishedApp = true;
                            spawners.SetActive(false);
                        }
                        break;
                    case > 0:
                        skillsText.text += " " + keywords[^1].text + " |";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
