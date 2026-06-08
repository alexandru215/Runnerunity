using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColision : MonoBehaviour
{
    private PlayerController playerController;
    private bool aMuritDeja = false;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        aMuritDeja = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        string nume = other.gameObject.name.ToLower();

        if (nume.Contains("coin") || nume.Contains("ban") || nume.Contains("galben"))
        {
            Collider coinCollider = other.GetComponent<Collider>();
            if (coinCollider != null && coinCollider.enabled == false) return; 
            if (coinCollider != null) coinCollider.enabled = false;

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddBonusScore(200); 
            }

            Debug.Log("Banut colectat! +200 puncte.");
            Destroy(other.gameObject);
        }

        if (nume.Contains("powerup") || nume.Contains("verde"))
        {
            Collider powerUpCollider = other.GetComponent<Collider>();
            if (powerUpCollider != null && powerUpCollider.enabled == false) return; 
            if (powerUpCollider != null) powerUpCollider.enabled = false;

            if (playerController != null)
            {
                playerController.ActivateInvincibility();
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string nume = collision.gameObject.name.ToLower();

        if (nume.Contains("obstac") || nume.Contains("barier"))
        {
            if (playerController != null && playerController.IsInvincible == false)
            {
                if (aMuritDeja == true) return; 
                aMuritDeja = true; 

                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.SaveScoreHistory();
                }

                Debug.Log("GAME OVER! Scor salvat în istoric.");
                SceneManager.LoadScene("GameOverScene");
            }
            else if (playerController != null && playerController.IsInvincible == true)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}