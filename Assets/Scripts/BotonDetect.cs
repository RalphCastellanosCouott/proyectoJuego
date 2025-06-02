using UnityEngine;

public class BotonDetect : MonoBehaviour
{

    public PlataformaControl plataformaControl;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador entró a la zona del botón");
            plataformaControl.jugadorEnZonaBoton = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador salió de la zona del botón");
            plataformaControl.jugadorEnZonaBoton = false;
        }
    }
}