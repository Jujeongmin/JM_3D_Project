using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    MoveDirection myDir;

    private Vector3 moveVec;
    private Vector3 dirVec;

    public enum MoveDirection
    {
        Vertical,
        Horizontal,
        UpperLeft_LowerRight,
        UpperRigth_LowerLeft
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        switch (myDir)
        {
            case MoveDirection.Vertical:
                dirVec = Vector3.forward;
                break;
            case MoveDirection.Horizontal:
                dirVec = Vector3.right;
                break;
            case MoveDirection.UpperLeft_LowerRight:
                dirVec = new Vector3(1, 0, -1).normalized;
                break;
            case MoveDirection.UpperRigth_LowerLeft:
                dirVec = new Vector3(1, 0, 1).normalized;
                break;
        }
    }

    private void FixedUpdate()
    {
        moveVec = dirVec * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + moveVec);
    }

    public void OnBumped()
    {
        dirVec *= -1;
    }
}
