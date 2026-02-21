using UnityEngine;
using UnityEngine.InputSystem;

public class KeywordSpawner : MonoBehaviour
{
    public enum Direction
    {
        Right,
        Left,
    }

    public Direction direction;
    public float scrollSpeed;
    Vector3 moveDir;
    public GameObject keywordObject;
    Keyword keyword;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveDir = direction == Direction.Right ? Vector3.right : Vector3.left;
        var kwObj = Instantiate(keywordObject, transform.position, Quaternion.identity);
        keyword = kwObj.GetComponent<Keyword>();
        keyword.scrollSpeed = scrollSpeed;
        keyword.moveDir = moveDir;
    }

    // Update is called once per frame
    void Update() { }
}
