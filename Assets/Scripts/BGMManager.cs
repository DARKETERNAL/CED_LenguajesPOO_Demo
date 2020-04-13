using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] bgmSources;

    private AudioSource currentBGMSource;

    private int currentBGMIndex = 0;

    // Start is called before the first frame update
    private void Start()
    {
        if (bgmSources.Length > 0)
        {
            currentBGMSource = bgmSources[0];
            currentBGMSource.Play();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentBGMSource != null)
            {
                if (currentBGMSource.isPlaying)
                {
                    currentBGMSource.Stop();
                }
                else
                {
                    currentBGMSource.Play();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentBGMSource != null)
            {
                currentBGMSource.Stop();
            }

            currentBGMIndex += 1;

            if (currentBGMIndex >= bgmSources.Length)
            {
                currentBGMIndex = 0;
            }

            currentBGMSource = bgmSources[currentBGMIndex];
            currentBGMSource.Play();
        }
    }
}