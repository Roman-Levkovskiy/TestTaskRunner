using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    [SerializeField] Transform firstHuman;//Target to follow
    public Vector3 offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        offset = new Vector3(0, 10, -10);
    }
    private void FixedUpdate()
    {
        if(firstHuman)
        {
            Vector3 targetPosition = firstHuman.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        }
    }
    public void setFirstHuman(Transform firstHuman)
    {
        this.firstHuman = firstHuman;
    }
}
