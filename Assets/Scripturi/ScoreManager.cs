using UnityEngine;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    
    private float currentScore = 0f;
    private string filePath;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        filePath = Path.Combine(Application.persistentDataPath, "IstoricScor.txt");
    }

    private void Update()
    {
        if (GameStateMachine.Instance.CurrentState == GameState.Playing || 
            GameStateMachine.Instance.CurrentState == GameState.PowerUpActive)
        {
            currentScore += Time.deltaTime * 5f;
            if (scoreText != null)
            {
                scoreText.text = "Scor: " + Mathf.FloorToInt(currentScore).ToString();
            }
        }
    }

    public void AddBonusScore(int amount)
    {
        currentScore += amount;
    }

    public void SaveScoreHistory()
    {
        int finalScore = Mathf.FloorToInt(currentScore);
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " -> " + finalScore);
        }
    }

    public string LoadScoreHistory()
    {
        if (!File.Exists(filePath)) return "Nu exista scoruri salvate.";
        return File.ReadAllText(filePath);
    }
}
