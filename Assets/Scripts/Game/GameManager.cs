using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text player1text;
    public Text player2text;
    [HideInInspector]
    public int player1Points = 0;
    [HideInInspector]
    public int player2Points = 0;
    [HideInInspector]
    public turn turno =  turn.player1;
    [HideInInspector]
    public turn? stripes = null;
    [HideInInspector]
    public bool repetirturno;
    [HideInInspector]
    public turnphase faseactual = turnphase.apuntar;
    public GameObject blanca;
    public Text turnText;
    public Text stripeText;
    [HideInInspector]
    public bool fault = false;
    [HideInInspector]
    public bool firstCol = true;
    public Text winText;
    public GameObject panel;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        if (turno == turn.player1)
        {
            turnText.text = "Red Turn";
        }
        else
        {
            turnText.text = "Yellow Turn";
        }
    }
    public enum turn
    {
        player1, 
        player2
    }
    public enum turnphase
    {
        apuntar,
        moviendo
    }
    public void changeTurn()
    {
        if(turno == turn.player1)
        {
            turnText.text = "Yellow Turn";
            turnText.color = Color.yellow;
            stripeText.color = Color.yellow;
            if (stripes == turn.player1)
            {
                stripeText.text = "Solid";
            }
            if (stripes == turn.player2)
            {
                stripeText.text = "Stripes";

            }
            turno = turn.player2;
        }
        else
        {
            turnText.text = "Red Turn";
            turnText.color = Color.red;
            stripeText.color = Color.red;
            if (stripes == turn.player2)
            {
                stripeText.text = "Solid";
            }
            if (stripes == turn.player1)
            {
                stripeText.text = "Stripes";

            }
            turno = turn.player1;
        }
    }
    public void addPoint(int num)
    {
        if (player1Points == 0 && player2Points == 0)
        {
            if (num>8)
            {
                stripes = turno;
            }
            else
            {
                if (turno == turn.player1)
                {
                    stripes = turn.player2;
                }
                else
                {
                    stripes = turn.player1;
                }
            }
        }
        if (num >= 8)
        {
            if(stripes == turn.player1)
            {
                player1Points++;
                player1text.text = "Player 1: "+ player1Points+" Pts";
                if (player1Points == 8)
                {
                    panel.SetActive(true);
                    winText.text = "Player 1 Wins";
                }
            }
            else
            {
                player2Points++;
                player2text.text = "Player 2: " + player2Points + " Pts";
                if (player2Points == 8)
                {
                    panel.SetActive(true);
                    winText.text = "Player 2 Wins";
                }
            }
        }
        else
        {
            if (stripes == turn.player1)
            {
                player2Points++;
                player2text.text = "Player 2: " + player2Points + " Pts";
                if (player2Points == 8)
                {
                    panel.SetActive(true);
                    winText.text = "Player 2 Wins";
                }
            }
            else
            {
                player1Points++;
                player1text.text = "Player 1: " + player1Points + " Pts";
                if (player1Points == 8)
                {
                    panel.SetActive(true);
                    winText.text = "Player 1 Wins";
                }
            }
        }
    }
    public void verifyFault(int num)
    {
        Debug.Log("num: "+num);
        Debug.Log("turno: " + turno);
        Debug.Log("stripe: " + stripes);
        if (num >= 8)
        {
            if (turno == turn.player1)
            {
                if(stripes== turn.player2)
                {
                    fault = true;
                }
            }
            if (turno == turn.player2)
            {
                if (stripes == turn.player1)
                {
                    fault = true;
                }
            }
        }
        if (num <= 8)
        {
            if (turno == turn.player1)
            {
                if (stripes == turn.player1)
                {
                    fault = true;
                }
            }
            if (turno == turn.player2)
            {
                if (stripes == turn.player2)
                {
                    fault = true;
                }
            }
        }
    }
    public void setStripes(turn player)
    {
        stripes = player;
    }
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
