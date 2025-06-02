using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarraComida : MonoBehaviour
{
    public Image barraComidaImage;
    public Sprite[] estadosBarra;

    public int comidaActual = 8;
    public float tiempoEntreBajadas = 2f;

    private Coroutine corutinaVaciar;
    void Start()
    {
        CambiarComida(comidaActual);
        corutinaVaciar = StartCoroutine(VaciarComidaTiempo());
    }

    public void CambiarComida(int nuevaCantidad)
    {
        comidaActual = Mathf.Clamp(nuevaCantidad, 0, 8);
        barraComidaImage.sprite = estadosBarra[comidaActual];

        if (comidaActual == 0)
        {
            Debug.Log("Sin comida. Cerrando el juego...");
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    IEnumerator VaciarComidaTiempo()
    {
        while (comidaActual > 0)
        {
            yield return new WaitForSeconds(tiempoEntreBajadas);
            CambiarComida(comidaActual - 1);
        }
    }

    public void RecuperarComida(int cantidad)
    {
        CambiarComida(comidaActual + cantidad);
        ReiniciarTemporizador();
    }

    public void ReiniciarTemporizador()
    {
        if (corutinaVaciar != null)
        {
            StopCoroutine(corutinaVaciar);
        }
        corutinaVaciar = StartCoroutine(VaciarComidaTiempo());
    }
}