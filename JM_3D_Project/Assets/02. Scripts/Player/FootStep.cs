using UnityEngine;

public class FootStep : MonoBehaviour
{
    private Rigidbody rb;
    public float footStepThreshold;
    public float footStepRate;
    private float footStepTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();        
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
                    SoundManager.Instance.PlaySFX("Step");
                }
            }
        }
    }
}
