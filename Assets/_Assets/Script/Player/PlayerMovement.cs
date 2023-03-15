using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script yg handle movement dari player

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 8.5f;
    [SerializeField] private GameInput gameinput;
    
    public bool isWalking;

    public void Update()
    {
        Vector2 inputVector = gameinput.GetMovementVector();

        Vector3 movedirection = new Vector3(inputVector.x, 0f, inputVector.y);
        

        isWalking = movedirection != Vector3.zero;

        float rotatespeed = 6f;
        transform.forward = Vector3.Slerp(transform.forward, movedirection, Time.deltaTime * rotatespeed);

        float playersize = .7f;
        float playerheight = 2f;
        float movedistance = speed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playersize, movedirection ,movedistance);

        if (!canMove)
        {
            Vector3 movedirectionX = new Vector3(movedirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playersize, movedirectionX, movedistance);
            
            if(canMove) movedirection = movedirectionX;
            else
            {
                Vector3 movedirectionZ = new Vector3(0, 0, movedirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playersize, movedirectionZ, movedistance);
                if (canMove) movedirection = movedirectionZ;
                else { }
            }
        }

        if (canMove) transform.position += movedirection * movedistance;
    }
}
