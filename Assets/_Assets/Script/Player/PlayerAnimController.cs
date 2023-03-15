using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script yang handle animasi player


public class PlayerAnimController : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<Animator>())
        {
            if (GetComponent<PlayerMovement>().isWalking == true) GetComponent<Animator>().SetBool("IsWalking", true);
            else GetComponent<Animator>().SetBool("IsWalking", false);
        }
    }
}
