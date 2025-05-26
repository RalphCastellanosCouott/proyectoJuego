using UnityEngine;

public class PlataformaControl : MonoBehaviour
{
    public Transform plataforma;
    public float altura = 3f;
    public float velocidad = 2f;
    public KeyCode tecla = KeyCode.E;
    private bool estaArriba = false;
    private bool jugadorEncima = false;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;
    private Transform jugadorTransform;
    void Start()
    {
        posicionInicial = plataforma.position;
        posicionFinal = posicionInicial + Vector3.up * altura;
    }

    void Update()
    {
        if (jugadorEncima && Input.GetKeyDown(tecla))
        {
            StopAllCoroutines();
            StartCoroutine(MoverPlataforma());
        }
    }

    System.Collections.IEnumerator MoverPlataforma()
    {
        Vector3 destino = estaArriba ? posicionInicial : posicionFinal;
        if (jugadorTransform != null)
            jugadorTransform.SetParent(plataforma);

        while (Vector3.Distance(plataforma.position, destino) > 0.01f)
        {
            plataforma.position = Vector3.MoveTowards(plataforma.position, destino, velocidad * Time.deltaTime);
            yield return null;
        }
        plataforma.position = destino;
        estaArriba = !estaArriba;

        if (jugadorTransform != null)
            jugadorTransform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = true;
            jugadorTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = false;
            if (jugadorTransform != null)
            {
                jugadorTransform.SetParent(null);
                jugadorTransform = null;
            }
        }
    }
}