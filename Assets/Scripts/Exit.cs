using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Application.Quit();
            Debug.Log("Juego cerrado");
#if UNITY_EDITOR            
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}