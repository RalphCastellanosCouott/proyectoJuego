using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LifeSystem : MonoBehaviour
{
    [Header("Vidas")]
    public int maxLives = 3;
    private int currentLives;
    public Transform respawnPoint;
    public TextMeshProUGUI livesText;
    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        if (respawnPoint == null)
        {
            respawnPoint = this.transform;
        }
    }

    void Update()
    {
        UpdateLivesUI();
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = $"{currentLives}/{maxLives}";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            PerderVida();
            if (currentLives > 0)
            {
                transform.position = respawnPoint.position;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Estalactita"))
        {
            PerderVida();
        }
    }

    public void PerderVida()
    {
        currentLives--;
        UpdateLivesUI();
        Debug.Log("Vidas restantes: " + currentLives);

        if (currentLives == 0)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}