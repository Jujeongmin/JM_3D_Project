using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField]
    MovingPlatform platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            platform.OnBumped();
        }
    }    
}
