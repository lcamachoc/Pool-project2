using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    public GameObject blanca;
    public float Multiplicador;
    private Vector3 diferencia;
    private Camera mainCamera;
    private float zangle;
    private bool dragging = false;
    private float fuerza = 0f;
    private Vector3 direccion;
    private Rigidbody rb;
    public List<GameObject> balls;
    private LineRenderer lr;

    private void Start()
    {
        diferencia = gameObject.transform.position - blanca.transform.position;
        mainCamera = Camera.main;
        zangle = gameObject.transform.rotation.z * Mathf.Rad2Deg;
        rb = blanca.GetComponent<Rigidbody>();
        lr = gameObject.GetComponent<LineRenderer>();
        lr.SetWidth(0f, 0f);
    }
    void Update()
    {
        
        if (GameManager.Instance.faseactual == GameManager.turnphase.apuntar && GameManager.Instance.fault==false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragging = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                GameObject child = gameObject.transform.GetChild(0).gameObject;
                child.transform.localPosition = new Vector3(-2.5f, child.transform.localPosition.y, child.transform.localPosition.z);
                direccion = gameObject.transform.GetChild(0).position - blanca.transform.position;
                if (fuerza > 4f)
                {
                    fuerza = 4f;
                }
                Debug.Log(fuerza);
                rb.AddForce(-direccion * (Mathf.Pow(fuerza, 2)) * Multiplicador);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                lr.SetWidth(0f, 0f);
                GameManager.Instance.faseactual = GameManager.turnphase.moviendo;
                GameManager.Instance.firstCol = true;
            }
            setRotation();
            if (!dragging)
            {
            }
            else
            {
                setPosition();
            }
        }
        if (GameManager.Instance.faseactual == GameManager.turnphase.apuntar && GameManager.Instance.fault == true)
        {
            blanca.GetComponent<Collider>().isTrigger = true;
            Vector3 mouse = Input.mousePosition;
            Ray castpoint = mainCamera.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castpoint, out hit, Mathf.Infinity))
            {
                blanca.transform.position = new Vector3(hit.point.x, blanca.transform.position.y, hit.point.z) ;

            }
            if (Input.GetMouseButtonDown(1))
            {
                blanca.GetComponent<Collider>().isTrigger = false;
                GameManager.Instance.fault = false;
            }
        }
        gameObject.transform.position = blanca.transform.position + diferencia;

        if (Mathf.Abs(rb.velocity.magnitude) < 0.05f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (GameManager.Instance.faseactual == GameManager.turnphase.moviendo && !gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                GameManager.Instance.faseactual = GameManager.turnphase.apuntar;
                Invoke("activeTrue", 3f);
            }
        }
        foreach (var ball in balls)
        {
            if (ball != null)
            {
                Rigidbody ballrb = ball.GetComponent<Rigidbody>();
                if (Mathf.Abs(ballrb.velocity.magnitude) < 0.05f)
                {
                    ballrb.velocity = Vector3.zero;
                    ballrb.angularVelocity = Vector3.zero;

                }

            }
        }
    }
    private void setRotation()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castpoint = mainCamera.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castpoint, out hit, Mathf.Infinity))
        {
            Vector3 aux = hit.point - blanca.transform.position;
            float angle = Mathf.Atan2(aux.z, -aux.x);
            angle = Mathf.Rad2Deg * angle;
            gameObject.transform.rotation = Quaternion.Euler(0f, angle, zangle);
        }
    }
    private void setPosition()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castpoint = mainCamera.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castpoint, out hit, Mathf.Infinity))
        {
            float distance = Vector3.Distance(hit.point, blanca.transform.position);
            fuerza = distance;
            GameObject child = gameObject.transform.GetChild(0).gameObject;
            child.transform.localPosition = new Vector3(-2.5f - (distance / 2), child.transform.localPosition.y, child.transform.localPosition.z);
            Vector3 linePoint = blanca.transform.position+(blanca.transform.position - child.transform.position);
            linePoint.y = blanca.transform.position.y;
            DrawLine(gameObject.transform.position, linePoint, Color.blue);
            
        }
    }
    private void activeTrue()
    {
        Debug.Log(GameManager.Instance.repetirturno.ToString());
        if (!GameManager.Instance.repetirturno)
        {
            GameManager.Instance.changeTurn();
        }
        else
        {

            GameManager.Instance.repetirturno = false;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

    }
    private void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        lr.material.color = color;
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
