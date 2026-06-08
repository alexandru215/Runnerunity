using UnityEngine;
using TMPro;
using System.IO;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI historyText;

    private void Start()
    {
       
        string filePath = Path.Combine(Application.persistentDataPath, "IstoricScor.txt");
        if (File.Exists(filePath))
        {
            historyText.text = File.ReadAllText(filePath);
        }
        else
        {
            historyText.text = "Nu exista scoruri salvate.";
        }
    }

    public void RetryGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Joc");
    }
}
