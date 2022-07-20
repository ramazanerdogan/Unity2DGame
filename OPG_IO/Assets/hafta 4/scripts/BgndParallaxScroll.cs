using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BgndParallaxScroll : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultipler=0.01f;
    [SerializeField] private float scrollSpeed=0.01f;

    private Vector2 _savedOffset;
    private Renderer _renderer;
    //player
    private Rigidbody2D playerRB;
    //[SerializeField] private Vector2 playerVelocity;

    //camera
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
     [SerializeField]  private Vector3 deltaCamMovement;

    // Start is called before the first frame update
    void Start()
    {
        _renderer=GetComponent<Renderer>();
        _savedOffset=_renderer.material.mainTextureOffset;

       // playerRB=GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        // playerRB=PlayerHealth.Instance.gameObject.GetComponent<Rigidbody2D>();
         //playerVelocity=playerRB.velocity;

         cameraTransform=Camera.main.transform;
         lastCameraPosition=cameraTransform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        deltaCamMovement=cameraTransform.position-lastCameraPosition;
        lastCameraPosition=cameraTransform.position;
       // playerVelocity=playerRB.velocity;

       float x=0f;
       float diffx=0f;

       diffx=(parallaxEffectMultipler*(Math.Sign(deltaCamMovement.x)*(Time.deltaTime*scrollSpeed)));
       x=_savedOffset.x+diffx;
       x=Mathf.Repeat(x,1);
       _savedOffset=new Vector2(x,_savedOffset.y);

       _renderer.material.mainTextureOffset=_savedOffset;

    }
}
