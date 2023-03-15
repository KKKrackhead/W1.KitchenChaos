using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script yg handle moevement dari particle player

public class ParticleMovement : MonoBehaviour
{
    void Update()
    {
        Vector3 tempx = new Vector3(0.5f, 0, 0);
        Vector3 tempz = new Vector3(0, 0, 0.5f);

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = GameObject.Find("PlayerVisual").GetComponent<Transform>().position - tempx;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = GameObject.Find("PlayerVisual").GetComponent<Transform>().position + tempx;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = GameObject.Find("PlayerVisual").GetComponent<Transform>().position - tempz;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = GameObject.Find("PlayerVisual").GetComponent<Transform>().position + tempz;
        }

    }
}
