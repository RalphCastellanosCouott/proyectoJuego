using UnityEngine;

public class Balanceo : MonoBehaviour
{
    public Rigidbody playerRb;
    public bool canSwing = false;
    public float swingForce = 5f;

    void Update()
    {
        if (!canSwing) return;

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        Vector3 forceDirection = (transform.forward * Vertical + transform.right * Horizontal).normalized;
        playerRb.AddForce(forceDirection * swingForce, ForceMode.Acceleration);
    }
}