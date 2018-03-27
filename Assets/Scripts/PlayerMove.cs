using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    CharacterController charControl;

    float walkSpeed = 8;
    bool isInWater = false;   //check if player is in water - can use this in drinking/health script
    //[SerializeField]
    //float jumpSpeed = 10;
    //Vector3 moveDirUp = transform.up * jumpSpeed;

    void Awake()
    {
        charControl = GetComponent<CharacterController>();
    }

    void Update()
    {
        movePlayer();
        if (Input.GetKeyDown (KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0 && isInWater == false)
        {
            walkSpeed = 25;  //player can sprint when not in water
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isInWater == false)
        {
            walkSpeed = 8;    //return to normal speed when shift released
        }
        //if(Input.GetKey("space"))
        {
            //charControl.moveDirUp = jumpSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "water")     //check if player has hit water volume
        {
            isInWater = true;
            Debug.Log("inwater");
            walkSpeed = 3;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "water")
        {
            walkSpeed = 8;
            isInWater = false;
            Debug.Log("outofwater");
        }

    }
    
   

    void movePlayer()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 moveDirSide = transform.right * horiz * walkSpeed;              //move player sideways at walkspeed
        Vector3 moveDirForward = transform.forward * vert * walkSpeed;          //move player foward at walkspeed   

        charControl.SimpleMove(moveDirSide);
        charControl.SimpleMove(moveDirForward);
    }

}

