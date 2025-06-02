using UnityEngine;
using System.Collections.Generic;

public class KiwiManager : MonoBehaviour
{
    [Header("Movimiento de los kiwis")]
    public float amplitud = 0.25f;
    public float speed = 2f;
    public float rotationSpeed = 45f;

    [Header("Lista de kiwis en escena")]
    public List<Transform> kiwis = new List<Transform>();

    private Dictionary<Transform, Vector3> posicionesIniciales = new Dictionary<Transform, Vector3>();

    private BarraComida barraComida;
    void Start()
    {
        barraComida = Object.FindAnyObjectByType<BarraComida>();
        foreach (var kiwi in kiwis)
        {
            if (kiwi != null)
            {
                posicionesIniciales[kiwi] = kiwi.position;
                Collider col = kiwi.GetComponent<Collider>();
                if (col == null) col = kiwi.gameObject.AddComponent<BoxCollider>();
                col.isTrigger = true;
                if (kiwi.GetComponent<PlayerCollectible>() == null)
                {
                    kiwi.gameObject.AddComponent<PlayerCollectible>().Init(this);
                }
            }
        }
    }

    void Update()
    {
        foreach (var kiwi in kiwis)
        {
            if (kiwi == null) continue;
            Vector3 startPos = posicionesIniciales[kiwi];
            float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitud;
            kiwi.position = new Vector3(startPos.x, newY, startPos.z);
            kiwi.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void Collect(Transform kiwi)
    {
        if (!kiwis.Contains(kiwi)) return;
        kiwis.Remove(kiwi);
        if (barraComida != null)
        {
            barraComida.RecuperarComida(2);
            barraComida.ReiniciarTemporizador();
        }
        Destroy(kiwi.gameObject);
    }
}