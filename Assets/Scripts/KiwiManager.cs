using UnityEngine;
using System.Collections.Generic;

public class KiwiManager : MonoBehaviour
{
    [Header("Lista kiwis visibles")]
    public List<GameObject> kiwiList;
    [Header("Referencia a la barra de comida")]
    public BarraComida foodBar;

    public void CollectKiwi(GameObject kiwi)
    {
        if (kiwiList.Contains(kiwi))
        {
            kiwiList.Remove(kiwi);
            kiwi.SetActive(false);
            if (foodBar != null)
            {
                foodBar.RecuperarComida(2);
            }
        }
    }
}