// Some of this code is from a tutorial by Brackeys
// https://www.youtube.com/watch?v=_QajrabyTJc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public static float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float y;
        if(Input.GetKey("space") && !Input.GetKey("left shift"))
        {
            y = 1f;
        }
        else if(!Input.GetKey("space") && Input.GetKey("left shift"))
        {
            y = -1f;
        }
        else
        {
            y = 0f;
        }
        

        Vector3 move = transform.right * x + transform.up*y + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Double speed when left control
        if(Input.GetKey(KeyCode.LeftControl))
        {
            controller.Move(move * speed * Time.deltaTime);
        }
    }

    public static void ResetSpeed()
    {
        PlayerMovement.speed = 4f;
    }
}
