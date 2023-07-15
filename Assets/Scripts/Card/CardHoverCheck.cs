using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoverCheck : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log("moused over " + this.transform);
        GameManagerScript.instance.StartHover(this.transform);
    }

    private void OnMouseExit()
    {
        GameManagerScript.instance.UpdateHandPositions();
    }
}
