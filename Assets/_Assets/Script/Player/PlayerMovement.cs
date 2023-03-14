using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isWalking;

    public void Update()
    {
        float speed = 4.8f;
        Vector2 inputVector = new Vector2(0, 0);
        isWalking = false;

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
            isWalking = true;
        }

        inputVector = inputVector.normalized;
        Vector3 movedirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += movedirection * speed * Time.deltaTime;
        transform.forward = movedirection;
    }
}
