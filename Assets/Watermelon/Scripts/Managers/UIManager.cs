using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject ganePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }
    void Start()
    {
        // SetMenu();
    }

    void Update()
    {

    }
    private void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                SetMenu();
                break;
            case GameState.Game:
                SetGame();
                break;
            case GameState.GameOver:
                SetGameOver();
                break;
        }
    }
    private void SetMenu()
    {
        menuPanel.SetActive(true);
        ganePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    private void SetGame()
    {
        ganePanel.SetActive(true);
        menuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    private void SetGameOver()
    {
        gameOverPanel.SetActive(true);
        menuPanel.SetActive(false);
        ganePanel.SetActive(false);

    }
    public void PlayButtonCallBack()
    {
        GameManager.instance.SetGameState();
        SetGame();
    }
    public void NextButtonCallBack()
    {
        Debug.Log("Next");
        SceneManager.LoadScene(0);

    }

}