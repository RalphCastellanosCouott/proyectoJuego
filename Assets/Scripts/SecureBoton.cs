using UnityEngine;

public class SecureBoton : MonoBehaviour
{
    public PlataformaControl plataformaControl;
    public KeyCode tecla = KeyCode.E;
    private bool jugadorEnZona = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
        }
    }
    
    void Update()
    {
        if (jugadorEnZona && Input.GetKeyDown(tecla))
        {
            plataformaControl.forzarBajada = true;
            Debug.Log("Llamando a la plataforma desde SecureBoton");
        }
    }
}