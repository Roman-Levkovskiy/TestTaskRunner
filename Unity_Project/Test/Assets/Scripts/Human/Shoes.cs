using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : MonoBehaviour
{
    [SerializeField] Material materialOfDanger;
    public Material getThisMaterial()
    {
        return materialOfDanger;
    }
}
