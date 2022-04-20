using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int valor;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Blanca" && GameManager.Instance.firstCol)
        {
            GameManager.Instance.firstCol = false;
            GameManager.Instance.verifyFault(valor);
        }
    }
}
