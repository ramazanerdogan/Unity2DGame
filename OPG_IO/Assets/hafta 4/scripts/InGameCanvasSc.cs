using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvasSc : Singleton<InGameCanvasSc>
{

    private bool isActive_InGameMenu = false;

    [SerializeField] private GameObject InGameGUI;
    [SerializeField] private GameObject InGameMenuObj;

    [SerializeField] private GameObject PlayerGO;
    [SerializeField] private PlayerHealth PlayerHealthSc;

    public void Start()
    {
        InGameGUI = this.gameObject.transform.GetChild(0).gameObject;
        InGameMenuObj = this.gameObject.transform.GetChild(1).gameObject;

        isActive_InGameMenu = false;
        InGameMenuObj.SetActive(isActive_InGameMenu);

      /*  PlayerGO=GameObject.FindWithTag("Player");
        
        if(PlayerGO)
            PlayerHealthSc=PlayerGO.GetComponent<PlayerHealth>();
        else
            Debug.LogError("InGameCanvasSc - Start - Player Object cannot be found");*/
          if(PlayerHealth.Instance!=null)
          {
              PlayerHealthSc=PlayerHealth.Instance;
              PlayerGO=PlayerHealthSc.gameObject;
          }
          else
          {
               Debug.LogError("player singleton has no instance");
               PlayerGO=PlayerHealthSc.gameObject;
          }  
    }


    public void SetPlayerGUIobjects()
    {
        
            if(PlayerHealth.Instance!=null&&PlayerGO==null)
                {
                PlayerHealthSc=PlayerHealth.Instance;
                PlayerGO=PlayerHealthSc.gameObject;
                }
            if(InGameGUI==null)
            {
                 InGameGUI = this.gameObject.transform.GetChild(0).gameObject;
                 InGameMenuObj = this.gameObject.transform.GetChild(1).gameObject;
            }
            
            if(InGameGUI!=null&&PlayerHealthSc!=null)
            {
                GameObject HealthBarGO=InGameGUI.transform.GetChild(0).gameObject;
                GameObject HealthTextGO=InGameGUI.transform.GetChild(1).gameObject;
                GameObject SoulTextGO=InGameGUI.transform.GetChild(3).gameObject;
                PlayerHealthSc.setGUIobject(HealthBarGO,HealthTextGO,SoulTextGO);
            }
    }

     void Update()
    {
        if(Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.P))
        {
            InGameMenu_Continue();
        }
    }

     public void InGameMenu_Continue()
    {
        if(isActive_InGameMenu == false) {isActive_InGameMenu = true; Time.timeScale = 0f;}
            else{isActive_InGameMenu = false; Time.timeScale = 1f; }
            InGameMenuObj.SetActive(isActive_InGameMenu);
    }

    public void InGameMenu_Save()
    {
        if(PlayerGO != null)
        {
            SaveLoad.SaveData(PlayerHealthSc.getPlayerData());
        }
        else
        {
            Debug.LogError("Player cannot be found to save the game!");
        }
    }

    public void InGameMenu_Load()
    {
        if(PlayerGO != null)
        {
            PlayerHealthSc.setPlayerData( SaveLoad.LoadData() ); 
            InGameMenu_Continue();
        }
        else
        {
            Debug.LogError("Player cannot be found to load the game!");
        }
    }

    public void InGameMenu_Settings()
    {
        
    }

    public void InGameMenu_Exit()
    { 
        Application.Quit(); 
     }
}
