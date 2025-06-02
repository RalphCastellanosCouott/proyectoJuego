using System.Collections;
using UnityEngine;

public class Estalactita : MonoBehaviour
{
    public Transform resetPoint;
    private Rigidbody rb;
    private Quaternion startRot;
    public float cooldownTime = 2f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        if (resetPoint == null) resetPoint = transform.parent;
        startRot = transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
        {            
            StartCoroutine(ResetWithCooldown());
        }
    }

    IEnumerator ResetWithCooldown()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.position = transform.parent.position;
        transform.rotation = startRot;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(cooldownTime);

        rb.isKinematic = false;
        rb.useGravity = true;
    }
}