using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tonera : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Ball script = collision.gameObject.GetComponent<Ball>();
            int numero = script.valor;
            Debug.Log(numero);
            Destroy(collision.gameObject);
            GameManager.Instance.addPoint(numero);
            GameManager.Instance.repetirturno = true;
        }
        if (collision.gameObject.tag == "Blanca")
        {
            GameManager.Instance.fault = true;
        }
    }
}
