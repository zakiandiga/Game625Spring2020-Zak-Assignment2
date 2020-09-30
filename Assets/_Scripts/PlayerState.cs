using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private PlayerStates playerState = PlayerStates.normal;

    public enum PlayerStates 
    { 
        grow, 
        shrink, 
        normal,
    }
    
    PlayerController playerController;

    private float originSpeed;    
    private Vector3 originSize;
    [SerializeField] Vector3 bigSize;
    [SerializeField] Vector3 smallSize;
    [SerializeField] private float bigSpeed;
    [SerializeField] private float smallSpeed;

    [SerializeField] private float smallCoolDown;

    private bool isNormal = true;
    private bool isBig = false;
    private bool isSmall = false;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
        originSize = transform.localScale;
        originSpeed = playerController.speed;

        PlayerController.OnStayBig += BigTrue; //Subscribe to PlayerController collision function
        PlayerController.OnExitBig += BigFalse; //Subscribe to PlayerController collision function
    } 

    private void Recover()
    {
        playerState = PlayerStates.normal;

        transform.localScale = originSize;
        playerController.speed = originSpeed;

        Debug.Log("Player size recovered");
    }

    private void BigTrue(PlayerController p)
    {
        if (playerState == PlayerStates.normal)
        {
            isBig = true;
            GetBig();
        }
        
    }

    private void BigFalse(PlayerController p)
    {
        isBig = false;
        Recover();
    }

    private void GetBig()
    {
        playerState = PlayerStates.grow;

        transform.localScale = bigSize;
        playerController.speed = bigSpeed;
        
        Debug.Log("Player growing");
        Debug.Log("Max HP ++");
    }

    public void SmallButtonClick()
    {
        if (playerState == PlayerStates.normal)
        {
            GetSmall();
        }
    }

    private void GetSmall()
    {
        playerState = PlayerStates.shrink;

        transform.localScale = smallSize;
        playerController.speed = smallSpeed;

        Debug.Log("Player shrinking");
        Debug.Log("Max HP --");

        StartCoroutine("SmallCoolDown", smallCoolDown);
    }

    IEnumerator SmallCoolDown (float cd)
    {
        yield return new WaitForSeconds(cd);
        Debug.Log("Small CD coroutine finished");
        Recover();
        isNormal = true;        
    }



    void Update()
    {
        /*
        switch (playerState)
        {
            case PlayerStates.normal:
                //run all normal behavior here       
                
                //state change conditions:
                if (isBig) //fill the condition to grow
                {
                    playerState = PlayerStates.grow;
                }
                else if (isSmall) //fill the condition to shrink
                {
                    playerState = PlayerStates.shrink;
                }
                break;

            case PlayerStates.grow:
                //run all big behavior here

                //state change conditions:
                if (isNormal) //fill the condition to normal
                {
                    playerState = PlayerStates.normal;
                }
                break;

            case PlayerStates.shrink:
                //run all small behavior here

                if (isNormal) //fill the condition to normal
                {
                    playerState = PlayerStates.normal;
                }
                break;

            default:
                break;
         }
         */
    }
}
