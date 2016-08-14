using System.Collections.Generic;
using UnityEngine;

public class ScreenRotate : MonoBehaviour {

    float spriteRotation = 45;
    float rotAngle = 90;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void LateUpdate() {
        if (rotAngle <= 0) {
            GameManager.GM.rotating = false;
            rotAngle = 90f;
        }

        if (spriteRotation <= 0) {
            spriteRotation = 45f;
        }

        if (GameManager.GM.rotating)
        {
            float deltaAngle = 90 * Time.deltaTime;

            rotateCamera(deltaAngle);
            rotateSprite(deltaAngle);
        }
    }


    // replace following variables with calls to static  game manager vars

    void rotateCamera(float deltaAngle) {
        if (rotAngle > deltaAngle) {
            rotAngle -= deltaAngle;
            GameManager.GM.camera.transform.RotateAround(GameManager.GM.cameraAxis.transform.position,
                Vector3.up, GameManager.GM.rotDir * deltaAngle);
        } else {
            GameManager.GM.camera.transform.RotateAround(GameManager.GM.cameraAxis.transform.position, 
                Vector3.up, GameManager.GM.rotDir * rotAngle);
            rotAngle = 0;
        }
    }

    void rotateSprite(float deltaAngle) {
        if (spriteRotation > deltaAngle) {
            spriteRotation -= deltaAngle;
            //GameManager.GM.sprite.transform.RotateAround(GameManager.GM.cameraAxis.transform.position,
            //    Vector3.up, GameManager.GM.rotDir * deltaAngle);
        } else {
            //GameManager.GM.sprite.transform.RotateAround(GameManager.GM.sprite.transform.position, 
            //    Vector3.up, GameManager.GM.rotDir * spriteRotation);
            spriteRotation = 0;
        }
    }
}
