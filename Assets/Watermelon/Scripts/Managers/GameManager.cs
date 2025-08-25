using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Settings")]
    [SerializeField] private GameState gameState;

    [Header("Actions")]
    public static Action<GameState> onGameStateChanged;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        SetMenu();
    }

    void Update()
    {

    }

    private void SetMenu()
    {
        SetGanmdState(GameState.Menu);


    }
    private void SetGame()
    {
        SetGanmdState(GameState.Game);

    }
    private void SetGameOver()
    {
        SetGanmdState(GameState.GameOver);

    }
    private void SetGanmdState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }
    public GameState GetGameState()
    {
        return gameState;
    }
    public void SetGameState()
    {
        SetGame();
    }
    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
    public void SetGameOverState()
    {
        SetGameOver();
    }
}
