using UnityEngine;

public class LianaSwing : MonoBehaviour
{
    public Transform hookPoint;
    public Rigidbody ropeBottom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            PlayerSwing swing = other.GetComponentInParent<PlayerSwing>();
            if (swing != null && !swing.playerIsSwinging() && !swing.RecentlyDetached())
            {
                swing.AttachToLiana(this);
            }
        }
    }
}