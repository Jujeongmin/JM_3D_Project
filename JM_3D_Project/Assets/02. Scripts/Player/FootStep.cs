using UnityEngine;

public class FootStep : MonoBehaviour
{
    public AudioClip[] footStepClips;
    private AudioSource audioSource;
    private Rigidbody rb;
    public float footStepThreshold;
    public float footStepRate;
    private float footStepTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            if(rb.velocity.magnitude > footStepThreshold)
            {
                if(Time.time - footStepTime > footStepRate)
                {
                    footStepTime = Time.time;
                    audioSource.PlayOneShot(footStepClips[Random.Range(0,footStepClips.Length)]);
                }
            }
        }
    }
}
