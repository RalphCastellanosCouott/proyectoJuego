using UnityEngine;

public class PlayerCollectible : MonoBehaviour
{
    private KiwiManager manager;
    public void Init(KiwiManager manager)
    {
        this.manager = manager;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.Collect(transform);
        }
    }
}