using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanavarSc : MonoBehaviour
{

    [SerializeField] private GameObject TextMeshProObj;
    [SerializeField] private GameObject FlamesObj;

    private GameObject PlayerObj;
    private PlayerHealth PlayerHealthSc;

    private bool isPlayerEntered = false;
    private bool healState = false;

    private float timerMax = 2f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProObj.SetActive(false);
        FlamesObj.SetActive(false);
        timer = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("Fire1") && isPlayerEntered == true )
        {
            if( healState == false ) healState=true;
            //Debug.Log("Fire1 Pressed");
            TextMeshProObj.SetActive(false);
            FlamesObj.SetActive(true);
        }

        if(healState == true)
        {
            timer = timer - Time.deltaTime;
            if( timer <= 0 )
            {
                HealPlayer();
                timer = timerMax;
            }
            HealPlayer();
            //Debug.Log("Heal Player");
        }
    }

    private void HealPlayer()
    {
        if(PlayerHealthSc != null)
        {
            if(PlayerHealthSc.getPlayerHealth() >= 100)
            {
                healState = false;
            }
            else 
            {
                PlayerHealthSc.changeHealth(15f);
                Debug.Log("Heal player " + " - Player health: " + PlayerHealthSc.getPlayerHealth());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("You entered ice are");
        //Debug.Log("GameObject " + other.name);
        //if((other.gameObject.tag).Equals("Player"))
        if( other.gameObject.CompareTag("Player") )
        {
            PlayerObj = other.gameObject;
            PlayerHealthSc = PlayerObj.GetComponent<PlayerHealth>();
            isPlayerEntered = true;
           //Debug.Log("You entered ice area");
            TextMeshProObj.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if( other.gameObject.CompareTag("Player") )
        if( other.gameObject == PlayerObj)
        {
            isPlayerEntered = false;
            //Debug.Log("You exited ice area");
            TextMeshProObj.SetActive(false);
            FlamesObj.SetActive(false);
            healState = false;
        }
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanavarSc : MonoBehaviour
{ 
    [SerializeField] private GameObject TextMeshProObj;
    [SerializeField] private GameObject Flames;

    private GameObject playerObj;
    private PlayerHealth playerHealthScript;

    private bool isPlayerEntered = false;
    private bool healthState = false;
    //private bool isSetActive = false;


    private float timeMax = 2f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProObj.SetActive(false);
        Flames.SetActive(false);
        timer = timeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && isPlayerEntered == true) 
        {
            if(healthState == false)
            {
                healthState = true;
            }
            TextMeshProObj.SetActive(false);
            
        }
        if(healthState == true)
        {
            HealPlayer();
            Flames.SetActive(true);
        }
    }

    private void HealPlayer()
    {
        timer = timer - Time.deltaTime;

        if (timer <= 0f)
        {

            if (playerHealthScript.getPlayerHealth() >= 100)
            {
                healthState = false;
            }
            playerHealthScript.changeHealth(10f);
            timer = timeMax;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            playerObj = other.gameObject;
            Debug.Log("Campfire Region!");
            TextMeshProObj.SetActive(true);
            isPlayerEntered = true;
            playerHealthScript = playerObj.GetComponent<PlayerHealth>();
        }
        
        // Debug.Log("Entry : " + other.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject == (playerObj))
        {
            TextMeshProObj.SetActive(false);
            isPlayerEntered = false;
            healthState = false;
            Flames.SetActive(false);
            
        }
    }  



}
*/