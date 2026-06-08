using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    public static GameStateMachine Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ChangeState(GameState.Playing);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.PowerUpActive:
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                ScoreManager.Instance.SaveScoreHistory();
                SceneManager.LoadScene("GameOverScene");
                break;
        }
    }
}
