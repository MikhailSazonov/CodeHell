using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movementInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // if(movementInput != Vector2.zero)
        // {
        //     transform.Translate(movementInput * Time.deltaTime);
        // }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
        // Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y);
        // transform.Translate(movement * Time.deltaTime);
    }
}
