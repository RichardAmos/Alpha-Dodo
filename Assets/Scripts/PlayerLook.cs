using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    public float mouseSensitivity;
    public Transform playerBody;
    float xaxisClamp = 0;             //clamp camera rotation on y axis from 90 to -90
    

	void Update()
    {
        RotateCamera();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xaxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = playerBody.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;                 //rotate camera horizontally around y axis
        targetRotCam.z = 0;                           //prevent camera flipping
        targetRotBody.y += rotAmountX;                //rotate player body vertically around x axis to look up and down
        
        if(xaxisClamp > 90)                           //quaternion to transform
        {
            xaxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if(xaxisClamp < -90)
        {
            xaxisClamp = -90;
            targetRotCam.x = 270;
        }

        transform.rotation = Quaternion.Euler(targetRotCam);
        playerBody.rotation = Quaternion.Euler(targetRotBody);


    }
}
