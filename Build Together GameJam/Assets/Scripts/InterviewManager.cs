using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class InterviewManager : MonoBehaviour
{
    public enum GameState
    {
        CPUTurn,
        PlayerTurn,
        BetweenTurns,
    }

    public GameState gameState;
    GameState lastGameState;
    int turnCount;

    public GameObject cpuTextBoxPrefab,
        playerTextBoxPrefab;
    GameObject cpuTextBox,
        playerTextBox;
    TextMeshPro cpuText,
        playerText;

    float turnStartTime;
    public float waitSecs;
    public float answerTimerSecs;
    bool isRight,
        isWrong;
    string[] questions =
    {
        "Are you <u>ready</u>?",
        "How good are you at SQL <u>queries</u>?",
        "<b>Explain</b> your <u>work</u> ethic.",
        "<b>Question</b>: When dealing with <u>extremely</u> frustrating bugs, <i>what</i> do you do to fix them?",
    };
    string[] rightAns =
    {
        "I'm ready!",
        "I'm really good with queries!",
        "I never stop until my work is complete!",
        "I excel in fixing bugs by using all of my resources!",
    };
    string[] wrongAns =
    {
        "Are YOU ready?",
        "Queries make me queasy.",
        "I hear that I'm a very lazy and unreliable person.",
        "Bug? Where?! I'm scared of bugs!",
    };
    private bool cpuTextBoxInstantiated;
    private bool turnStartTimeSet;
    private bool playerTextBoxInstantiated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameState.CPUTurn;
        turnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnCount == 4)
        {
            GameEnd();
            return;
        }
        if (gameState == GameState.CPUTurn)
        {
            CPUTurn();
        }
        else if (gameState == GameState.PlayerTurn)
        {
            PlayerTurn();
        }
        else if (gameState == GameState.BetweenTurns)
        {
            BetweenTurns();
        }
    }

    private void CPUTurn()
    {
        if (!cpuTextBoxInstantiated)
        {
            cpuTextBox = Instantiate(cpuTextBoxPrefab, cpuTextBoxPrefab.transform);
            cpuTextBoxInstantiated = true;
        }

        cpuText = cpuTextBox.GetComponentInChildren<TextMeshPro>();
        cpuText.text = questions[turnCount];

        SetTurnStartTime();

        if (Time.time >= turnStartTime + waitSecs)
        {
            answerTimerSecs = 3f;
            lastGameState = GameState.CPUTurn;
            gameState = GameState.BetweenTurns;
        }
    }

    private void PlayerTurn()
    {
        answerTimerSecs -= Time.deltaTime;
        cpuText.text = questions[turnCount] + $"\nAnswer in {Math.Round(answerTimerSecs, 1)}";

        bool answerQ = Keyboard.current.qKey.wasPressedThisFrame;
        bool answerW = Keyboard.current.wKey.wasPressedThisFrame;
        bool answerE = Keyboard.current.eKey.wasPressedThisFrame;
        bool answerR = Keyboard.current.rKey.wasPressedThisFrame;

        switch (turnCount)
        {
            case 0:
                isRight = answerR;
                isWrong = answerQ || answerW || answerE;
                break;
            case 1:
                isRight = answerQ;
                isWrong = answerR || answerW || answerE;
                break;
            case 2:
                isRight = answerW;
                isWrong = answerQ || answerR || answerE;
                break;
            case 3:
                isRight = answerE;
                isWrong = answerQ || answerW || answerR;
                break;
            default:
                break;
        }

        if (isRight && answerTimerSecs > 0)
        {
            Destroy(cpuTextBox);
            cpuTextBoxInstantiated = false;

            if (!playerTextBoxInstantiated)
            {
                playerTextBox = Instantiate(playerTextBoxPrefab, playerTextBoxPrefab.transform);
                playerTextBoxInstantiated = true;
            }

            playerText = playerTextBox.GetComponentInChildren<TextMeshPro>();
            playerText.text = rightAns[turnCount];
        }
        else if (isWrong && answerTimerSecs > 0)
        {
            Destroy(cpuTextBox);
            cpuTextBoxInstantiated = false;

            if (!playerTextBoxInstantiated)
            {
                playerTextBox = Instantiate(playerTextBoxPrefab, playerTextBoxPrefab.transform);
                playerTextBoxInstantiated = true;
            }

            playerText = playerTextBox.GetComponentInChildren<TextMeshPro>();
            playerText.text = wrongAns[turnCount];
        }

        SetTurnStartTime();

        if (Time.time >= turnStartTime + waitSecs)
        {
            lastGameState = GameState.PlayerTurn;
            gameState = GameState.BetweenTurns;
        }
    }

    private void BetweenTurns()
    {
        turnStartTimeSet = false;
        if (lastGameState == GameState.CPUTurn)
        {
            gameState = GameState.PlayerTurn;
        }
        else if (lastGameState == GameState.PlayerTurn)
        {
            Destroy(playerTextBox);
            playerTextBoxInstantiated = false;
            turnCount += 1;
            gameState = GameState.CPUTurn;
        }
    }

    private void SetTurnStartTime()
    {
        if (!turnStartTimeSet)
        {
            turnStartTime = Time.time;
            turnStartTimeSet = true;
        }
    }

    private void GameEnd()
    {
        if (!cpuTextBoxInstantiated)
        {
            cpuTextBox = Instantiate(cpuTextBoxPrefab, cpuTextBoxPrefab.transform);
            cpuTextBoxInstantiated = true;
        }

        cpuText = cpuTextBox.GetComponentInChildren<TextMeshPro>();
        cpuText.text = "We will be contacting you soon.";

        SetTurnStartTime();

        if (Time.time >= turnStartTime + waitSecs)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
