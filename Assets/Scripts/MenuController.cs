using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject bacteria;
    private float y;

    
    void Update()
    {
        y += Time.deltaTime * 50;
        bacteria.transform.rotation = Quaternion.Euler(0, y, 90);
    }
}
