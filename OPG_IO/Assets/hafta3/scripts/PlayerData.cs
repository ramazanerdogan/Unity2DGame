using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
  public float maxHealth=100f;
  public float minHealth=0f;
  public float curr_Health=40f;
  public int  soul_count = 0;
  

  public float playerPosX, playerPosY;

  //Scene Id Save
  public int sceneIndexNo;

  public PlayerData(){}
  public PlayerData(PlayerData Pxdata){ setPlayerData(Pxdata); }

  public void setPlayerData(PlayerData Pxdata)
  {
    this.maxHealth = Pxdata.maxHealth;
    this.minHealth = Pxdata.minHealth;
    this.curr_Health = Pxdata.curr_Health;
    this.soul_count = Pxdata.soul_count;
    this.playerPosX = Pxdata.playerPosX;
    this.playerPosY = Pxdata.playerPosY;
    this.sceneIndexNo  = Pxdata.sceneIndexNo;
  }
  
  public void setHealth(float setAmount)
  {
      this.curr_Health=setAmount;
      if(this.curr_Health > this.maxHealth) {this.curr_Health=this.maxHealth;}
      if(this.curr_Health < this.minHealth) {this.curr_Health=this.minHealth;}
  }

  public float getMaxHealth() { return this.maxHealth;}
  public float getMinHealth() { return this.minHealth;}
  public float getCurrentHealth() { return this.curr_Health;}

  public int getSoulCount() { return this.soul_count; }
  public void increaseSoulCount() { this.soul_count ++;}
  public void setSoulCount(int soulNum) { this.soul_count = soulNum; }

  public void setPlayerPos(float posX, float posY){ this.playerPosX = posX; this.playerPosY = posY; }

  public void setCurrentSceneId(int sceneIdVal)
  {
    this.sceneIndexNo=sceneIdVal;
  }

  public int getCurrentSceneId()
  {
    return this.sceneIndexNo;
  }


}
