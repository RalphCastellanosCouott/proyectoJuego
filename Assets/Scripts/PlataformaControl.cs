using UnityEngine;

public class PlataformaControl : MonoBehaviour
{
    public Transform plataforma;
    public float altura = 3f;
    public float velocidad = 2f;
    public KeyCode tecla = KeyCode.E;
    public Collider zonaBotonTrigger;
    private bool estaArriba = false;
    private bool jugadorEncima = false;
    public bool jugadorEnZonaBoton = false;
    public bool forzarBajada = false;
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
        if (!moverPlataforma && jugadorEncima && jugadorEnZonaBoton && Input.GetKeyDown(tecla))
        {
            destino = estaArriba ? posicionInicial : posicionFinal;
            moverPlataforma = true;
            estaArriba = !estaArriba;
        }

        if (!moverPlataforma && forzarBajada && estaArriba)
        {
            destino = posicionInicial;
            moverPlataforma = true;
            estaArriba = false;
            forzarBajada = false;
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
            Debug.Log("Jugador entró en la zona de la plataforma");
            jugadorEncima = true;
            jugadorTransform = other.transform;
            jugadorTransform.SetParent(plataforma);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador salió de la zona de la plataforma");
            jugadorEncima = false;
            jugadorEnZonaBoton = false;
            if (jugadorTransform != null)
            {
                jugadorTransform.SetParent(null);
                jugadorTransform = null;
            }
        }
    }
}