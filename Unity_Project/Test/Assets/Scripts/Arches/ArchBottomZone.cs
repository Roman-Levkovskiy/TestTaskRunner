using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchBottomZone : MonoBehaviour
{
    [SerializeField] Arch.shoes currentShoesType;
    public void setCurrentShoesType(Arch.shoes currentShoesType, Material groundMaterial)
    {
        this.currentShoesType = currentShoesType;
        GetComponent<Renderer>().material = groundMaterial;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HumanController>())
        {
            if (other.gameObject.GetComponent<HumanController>().getShoes() != currentShoesType.ToString())
            Destroy(other.gameObject);
        }
    }
}
