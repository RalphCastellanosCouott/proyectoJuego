using UnityEngine;

public class PlayerLianaSwing : MonoBehaviour
{
    public KeyCode detachKey = KeyCode.Space;
    public CharacterMove characterMoveScript;
    public Balanceo balanceoScript;
    public Animator animator;
    private float swingTimer = 0f;

    private Rigidbody rb;
    private bool isSwinging = false;
    private HingeJoint hJoint;
    private float lastDetachTime = -10f;
    public float coolDown = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastDetachTime < coolDown) return;
        if (other.gameObject.name == "RopeBottom" && !isSwinging)
        {
            Rigidbody ropeRb = other.transform.parent.GetComponent<Rigidbody>();
            if (ropeRb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                hJoint = gameObject.AddComponent<HingeJoint>();
                hJoint.useLimits = false;
                hJoint.connectedBody = ropeRb;
                hJoint.autoConfigureConnectedAnchor = true;

                isSwinging = true;
                animator?.SetBool("IsSwinging", true);
                animator?.SetFloat("SwingPhase", 0f);
                characterMoveScript.enabled = false;

                if (balanceoScript != null)
                {
                    balanceoScript.canSwing = true;
                    balanceoScript.playerRb = rb;
                }
            }
        }
    }

    void Update()
    {
        if (isSwinging)
        {
            swingTimer += Time.deltaTime;
            float phase = Mathf.PingPong(swingTimer, 1f);
            animator?.SetFloat("SwingPhase", phase);
        }
        if (isSwinging && Input.GetKeyDown(detachKey))
        {
            DetachFromLiana();
        }
    }

    void DetachFromLiana()
    {
        isSwinging = false;
        animator?.SetBool("IsSwinging", false);
        animator?.SetFloat("SwingPhase", 1f);
        swingTimer = 0f;
        Destroy(hJoint);
        rb.isKinematic = true;
        characterMoveScript.enabled = true;

        if (balanceoScript != null)
        {
            balanceoScript.canSwing = false;
            balanceoScript.playerRb = null;
        }

        lastDetachTime = Time.time;
    }
}