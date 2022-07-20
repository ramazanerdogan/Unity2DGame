using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth> 
{

    private PlayerData Player1data;

    //HealthBar Game Objects
    [SerializeField] private float HealthBarWidth = 290f;
    public GameObject HealthBarImageObj;
    public GameObject HealthBarTextObj;
    public GameObject SoulTextObj;

    private RectTransform HealthBarTransform;
    private Text HealthBarText;
    private Text SoulCountText;

    Scene scene;

   
    public void setGUIobject(GameObject HealthBarImageGo,GameObject HealthBarTextGo,GameObject SoulTextGo)
    {
        
 
        HealthBarImageObj=HealthBarImageGo;
        HealthBarTextObj=HealthBarTextGo;
        SoulTextObj=SoulTextGo;
        
        HealthBarTransform = HealthBarImageObj.GetComponent<RectTransform>(); 
        HealthBarText = HealthBarTextObj.GetComponent<Text>();
        SoulCountText = SoulTextObj.GetComponent<Text>();

        //Debug.Log("Player Health: " +Player1data.getCurrentHealth());
        SoulCountText.text = Player1data.getSoulCount().ToString();
        HealthBarText.text = Player1data.getCurrentHealth().ToString();
        HealthBarTransform.sizeDelta = new Vector2( (Player1data.getCurrentHealth() / Player1data.getMaxHealth())*HealthBarWidth , HealthBarTransform.sizeDelta.y);
    }

    // Start is called before the first frame update
    void Start()
    {
         Player1data = new PlayerData();
        scene = SceneManager.GetActiveScene();

          if(scene.buildIndex>0)
          {
              HealthBarTransform = HealthBarImageObj.GetComponent<RectTransform>(); 
              HealthBarText = HealthBarTextObj.GetComponent<Text>();
              SoulCountText = SoulTextObj.GetComponent<Text>();

              SoulCountText.text = Player1data.getSoulCount().ToString();
              HealthBarText.text = Player1data.getCurrentHealth().ToString();
              HealthBarTransform.sizeDelta = new Vector2( (Player1data.getCurrentHealth() / Player1data.getMaxHealth())*HealthBarWidth , HealthBarTransform.sizeDelta.y);
          }
         

          
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

        void UpdateHealthBar()
        {
            if(Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Player1data.setHealth(Player1data.getCurrentHealth() + 10f);
            }
            if(Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                Player1data.setHealth(Player1data.getCurrentHealth() - 10f);
            }
              SoulCountText.text = Player1data.getSoulCount().ToString();
            //SoulCountText.text = Player1data.getSoulCount().ToString();
            HealthBarText.text = Player1data.getCurrentHealth().ToString();
            HealthBarTransform.sizeDelta = new Vector2( (Player1data.getCurrentHealth() / Player1data.getMaxHealth())*HealthBarWidth , HealthBarTransform.sizeDelta.y);
        }

        public void changeHealth(float Amount)
        {
            Player1data.setHealth(Player1data.getCurrentHealth() + Amount);
            HealthBarText.text = Player1data.getCurrentHealth().ToString();

            HealthBarTransform.sizeDelta = new Vector2( (Player1data.getCurrentHealth() / Player1data.getMaxHealth())*HealthBarWidth , 
            HealthBarTransform.sizeDelta.y);
        }

        public float getPlayerHealth()
        {
            return Player1data.getCurrentHealth();
        }

        void OnTriggerEnter2D( Collider2D col)
        {
            if(col.gameObject.CompareTag("Soul"))
            {
                Debug.Log("Soul Take");
                Player1data.increaseSoulCount();
                Destroy(col.gameObject);
            }
        }

        public PlayerData getPlayerData() //when save player data
        {
             Player1data.setPlayerPos(this.transform.position.x, this.transform.position.y);

             Scene scene = SceneManager.GetActiveScene();
             
             Player1data.setCurrentSceneId (scene.buildIndex);
             return Player1data; 
        }

        public void setPlayerData(PlayerData Pxdata) //when load player data 
        {
             Player1data.setPlayerData(Pxdata);
             this.transform.position = new Vector3(Player1data.playerPosX, Player1data.playerPosY, 0f);
        }

        public Vector3 getPlayerPos(){return new Vector3(Player1data.playerPosX,Player1data.playerPosY,0f);}

}