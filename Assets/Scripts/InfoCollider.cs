using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfoCollider : MonoBehaviour
{
    [TextArea] public string infoText;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.ActiveInfoText(infoText);
        }

        if (tag == "Finish")
        {
            GameObject.Find("FlagHead").GetComponent<Animator>().SetTrigger("FlagDown");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.DeactivateInfoText(infoText);
        }
    }
}
