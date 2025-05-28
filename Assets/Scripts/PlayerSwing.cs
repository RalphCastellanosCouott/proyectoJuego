using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cubeVisual;
    public CharacterController controller;
    public float swingForce = 10f;
    public KeyCode detachKey = KeyCode.Space;

    private bool isSwinging = false;
    private Transform swingPoint;
    public bool IsSwinging => isSwinging;
    private bool recentlyDetached = false;
    private float reattachDelay = 0.5f;

    private void Update()
    {
        if (isSwinging)
        {
            float horizontal = Input.GetAxis("Horizontal");
            rb.AddForce(transform.right * horizontal * swingForce);
            if (Input.GetKeyDown(detachKey))
            {
                DetachFromLiana();
            }
        }
    }

    public bool playerIsSwinging()
    {
        return isSwinging;
    }

    public bool RecentlyDetached()
    {
        return recentlyDetached;
    }

    public void AttachToLiana(LianaSwing liana)
    {
        controller.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        swingPoint = liana.hookPoint;
        isSwinging = true;

        transform.position = swingPoint.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        HingeJoint joint = gameObject.AddComponent<HingeJoint>();
        joint.connectedBody = liana.ropeBottom;
        joint.autoConfigureConnectedAnchor = true;
    }

    public void DetachFromLiana()
    {
        isSwinging = false;
        Destroy(GetComponent<HingeJoint>());
        controller.enabled = true;
        rb.isKinematic = true;
        recentlyDetached = true;
        Invoke(nameof(ResetReattach), reattachDelay);
    }

    private void ResetReattach()
    {
        recentlyDetached = false;
    }
}