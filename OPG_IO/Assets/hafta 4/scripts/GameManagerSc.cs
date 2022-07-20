using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;// to camera follow player

public class GameManagerSc : Singleton<GameManagerSc>
{
    
    [SerializeField] private GameObject PlayerPrefab; 
    private GameObject PlayerGO;
    private PlayerHealth PlayerHealthSc;

    [SerializeField]  private GameObject InGameCanvasPrefab;
    private GameObject InGameCanvas;
    private InGameCanvasSc InGameCanvasScript;

 
    public static GameManagerSc instance;

    //scene information

    Scene currentScene;

    //start a new game

    private bool startAnewGame;

    //player data
    PlayerData PxData;


    void Start()
    {
        startAnewGame=false;
        // get currrent scene
        currentScene=SceneManager.GetActiveScene();

        //
       PlayerGO=Instantiate(PlayerPrefab,new Vector3(0f,0f,0f),Quaternion.identity);
       PlayerHealthSc=PlayerGO.GetComponent<PlayerHealth>();


       //instantiate ingamecanvas

       InGameCanvas=Instantiate(InGameCanvasPrefab,new Vector3(0f,0f,0f),Quaternion.identity);
       InGameCanvasScript=InGameCanvas.GetComponent<InGameCanvasSc>();

       if(currentScene.buildIndex==0)
       {
           PlayerGO.SetActive(false);
           InGameCanvas.SetActive(false);
       }

        
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("after scene loated");
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode mode )
    {

           Debug.Log("OnSceneLoaded"+ scene.name);
           Debug.Log("Scene mode:"+ mode);
           if(startAnewGame==false)
           {
            //load game
            PlayerHealthSc.setPlayerData(PxData);
            PlayerGO.transform.position=PlayerHealthSc.getPlayerPos();
           }
           else
           {
            PlayerGO.transform.position=new Vector3(0f,2f,0f);
           }
           InGameCanvasScript.SetPlayerGUIobjects();

           GameObject cineMachine=GameObject.FindWithTag("Cinemachine");

           if(cineMachine!=null)
           {
               CinemachineVirtualCamera vcam=cineMachine.GetComponent<CinemachineVirtualCamera>();
               vcam.Follow=PlayerGO.transform;

           }
           else
           {
               Debug.Log("could not find GO with tag cinemachine");
           }

    }
    public void MainMenuStart()
    {
        startAnewGame=true;
        Debug.Log("Start Game");
        
        PlayerGO.SetActive(true);   
        InGameCanvas.SetActive(true);
    
        //SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene(1);

        SceneManager.sceneLoaded+=OnSceneLoaded;
    }

    public void MainMenuLoad()
    {
        startAnewGame=false;

        PxData=SaveLoad.LoadData();

        PlayerGO.SetActive(true);   
        InGameCanvas.SetActive(true);

        SceneManager.LoadScene(PxData.getCurrentSceneId());
        SceneManager.sceneLoaded+=OnSceneLoaded;

    }

    public void MainMenuSettingsUI()
    {
        //MainMenu_Default.SetActive(false);
    }

    public void MainMenuExit()
    {  
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
