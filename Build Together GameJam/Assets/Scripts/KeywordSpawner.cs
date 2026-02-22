using System;
using Unity.Mathematics;
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
    float elapsedSecs = 0f;
    bool spawnCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveDir = direction == Direction.Right ? Vector3.right : Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedSecs += Time.deltaTime;
        float elapsedSecsRounded = Mathf.Round(elapsedSecs);
        if (!spawnCooldown && elapsedSecsRounded % 3 == 0)
        {
            SpawnKeyword();
        }
        if (elapsedSecsRounded % 3 != 0)
        {
            spawnCooldown = false;
        }
    }

    void SpawnKeyword()
    {
        GameObject kwObj = Instantiate(keywordObject, transform.position, Quaternion.identity);
        Destroy(kwObj, 9f);
        keyword = kwObj.GetComponent<Keyword>();
        keyword.scrollSpeed = scrollSpeed;
        keyword.moveDir = moveDir;
        spawnCooldown = true;
    }
}
