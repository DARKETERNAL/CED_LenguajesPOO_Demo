using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private Text text;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        text.text = player.JumpCount.ToString();
    }
}