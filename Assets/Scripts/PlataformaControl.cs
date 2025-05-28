using UnityEngine;

public class PlataformaControl : MonoBehaviour
{
    public Transform plataforma;
    public float altura = 3f;
    public float velocidad = 2f;
    public KeyCode tecla = KeyCode.E;
    private bool estaArriba = false;
    private bool jugadorEncima = false;
    private bool moverPlataforma = false;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;
    private Vector3 destino;
    private Transform jugadorTransform;
    void Start()
    {
        posicionInicial = plataforma.position;
        posicionFinal = posicionInicial + Vector3.up * altura;
        destino = posicionInicial;
    }

    void Update()
    {
        if (jugadorEncima && Input.GetKeyDown(tecla))
        {
            destino = estaArriba ? posicionInicial : posicionFinal;
            moverPlataforma = true;
            estaArriba = !estaArriba;
        }
    }

    void FixedUpdate()
    {
        if (moverPlataforma)
        {
            plataforma.position = Vector3.MoveTowards(plataforma.position, destino, velocidad * Time.deltaTime);
            if (Vector3.Distance(plataforma.position, destino) < 0.01f)
            {
                plataforma.position = destino;
                moverPlataforma = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = true;
            jugadorTransform = other.transform;
            jugadorTransform.SetParent(plataforma);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = false;
            jugadorTransform.SetParent(null);
            jugadorTransform = null;
        }
    }
}