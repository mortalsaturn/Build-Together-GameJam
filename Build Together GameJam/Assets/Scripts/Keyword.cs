using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class Keyword : MonoBehaviour
{
    public KeywordManager keywordManager;
    TextMeshPro keywordText;
    BoxCollider2D col;

    enum Type
    {
        Skill,
        Schedule,
    }

    Type keywordType;
    bool isRight;
    string[] rightSkills =
    {
        "Programming",
        "Version Control",
        "QA Testing",
        "Teamwork",
        "Problem Solving",
    };
    string[] wrongSkills =
    {
        "Flipping Burgers",
        "Customer Service",
        "QA Questing",
        "World Destruction",
        "Couch Potato",
    };
    string[] rightSchedule =
    {
        "9 AM - 5 PM",
        "9 AM - 2 PM",
        "10 AM - 3 PM",
        "11 AM - 4 PM",
        "12 AM - 5 PM",
    };
    string[] wrongSchedule =
    {
        "9 PM - 5 AM",
        "Night Shift",
        "I'm not available",
        "If y'all need me",
        "At some point",
    };
    public float scrollSpeed;
    public Vector3 moveDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keywordText = GetComponentInChildren<TextMeshPro>();
        col = GetComponent<BoxCollider2D>();

        keywordType = Random.Range(0, 2) == 0 ? Type.Skill : Type.Schedule;
        isRight = Random.Range(0, 2) == 0;
        int randIndex = Random.Range(0, 5);

        switch (keywordType)
        {
            case Type.Skill:
                if (isRight)
                    keywordText.text = rightSkills[randIndex];
                else
                    keywordText.text = wrongSkills[randIndex];
                break;
            case Type.Schedule:
                if (isRight)
                    keywordText.text = rightSchedule[randIndex];
                else
                    keywordText.text = wrongSchedule[randIndex];
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out Keyword keyword))
            {
                keywordManager.keywords.Add(hit.collider.GetComponent<Keyword>());
            }
        }
    }

    void Move()
    {
        transform.Translate(moveDir * Time.deltaTime * scrollSpeed);
    }
}
