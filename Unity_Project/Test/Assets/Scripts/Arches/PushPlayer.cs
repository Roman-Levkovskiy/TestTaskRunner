using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HumanController>())   //its a bit faster then tag comparing and dont use strings
        {
            HumanController thisHuman = other.GetComponent<HumanController>();
            thisHuman.push(transform.position.x);
        }
    }
}
