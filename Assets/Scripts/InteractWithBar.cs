using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactwithbar : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CourageMetre.instance.increaseCourage(20);
            DramaMetre.instance.increaseDrama(20);
            Debug.Log("increase");
        }
    }
}
