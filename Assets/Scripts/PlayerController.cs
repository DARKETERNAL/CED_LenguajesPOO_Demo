using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //cambio de player controller
    private Rigidbody myRigidbody;

    public float jumpForce;

    private bool canJump = true;

    [SerializeField]
    private float jumpCooldown = 0.3F;

    [SerializeField]
    private ParticleSystem fallParticleSystem;

    [SerializeField]
    private ParticleSystem hitParticleSystem;

    private int jumpCount;

    private bool playFallPS;

    [SerializeField]
    private float moveSpeed = 1F;

    public int JumpCount { get { return jumpCount; } }

    // Start is called before the first frame update
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void DoJump()
    {
        if (canJump)
        {
            jumpCount += 1;
            canJump = false;
            myRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Jump") != 0F)
        {
            DoJump();
        }

        float hVal = Input.GetAxis("Horizontal");

        if (hVal != 0F)
        {
            transform.Translate(
                transform.right * hVal * moveSpeed * Time.deltaTime);
        }
    }

    private void ResetCanJump()
    {
        canJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si el objeto que toca es el suelo, reproduzca efectos de partícula de caída.
        if (collision.gameObject.layer.Equals(
            LayerMask.NameToLayer("Ground")))
        {
            if (playFallPS)
            {
                if (fallParticleSystem != null)
                {
                    fallParticleSystem.Play();
                }

                ResetCanJump();
            }
            else
            {
                playFallPS = true;
            }
        }
        // Si el objeto que toca es el cubo, reproduzca efectos de golpe.
        else if (collision.gameObject.layer.Equals(
            LayerMask.NameToLayer("JumpCounter")))
        {
            ParticleSystem psInstance = Instantiate<ParticleSystem>(
                hitParticleSystem,
                collision.contacts[0].point,
                Quaternion.identity);

            //Destroy(psInstance.gameObject, 0.6F);
        }
    }
}