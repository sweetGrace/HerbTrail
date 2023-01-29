using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public float speed = 5.0f;
    void Update()
    {

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed; //左右移动
        float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;   //前后移动
        transform.Translate(x, y,0);   //相机移动
    }
}
