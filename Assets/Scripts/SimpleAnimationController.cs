using UnityEngine;

public class SimpleAnimationController : MonoBehaviour
{
    [SerializeField]
    private float maxHeight = 10F;

    [SerializeField]
    private float moveSpeed = 1F;

    [SerializeField]
    private Animator animation;

    [SerializeField]
    private bool useClip = true;

    private Vector3 originalPosition;
    private Vector3 currentPosition;

    private float distanceToOriginalPosition;

    private float moveFactor = 1;

    // Start is called before the first frame update
    private void Start()
    {
        originalPosition = transform.position;

        if (useClip && animation != null)
        {
            animation.SetBool("PlayAnim", true);
            enabled = false;            
        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.up * moveFactor * moveSpeed * Time.deltaTime);
        currentPosition = transform.position;

        distanceToOriginalPosition = Vector3.Distance(currentPosition, originalPosition);

        if (distanceToOriginalPosition >= maxHeight)
        {
            moveFactor *= -1;
        }
    }
}