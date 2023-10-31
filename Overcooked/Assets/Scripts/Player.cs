using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 8f;
    [SerializeField]
    private float rotateSpeed = 10f;
    [SerializeField]
    private float playerRadius = .7f;
    [SerializeField]
    private float playerHeight = 10f;
    [SerializeField]
    private GameInput gameInput;

    private bool isWalking;
    void Start()
    {

    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveSpeed * Time.deltaTime);
        if (canMove)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        } 
        else
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveSpeed * Time.deltaTime);

            if (canMove)
            {
                moveDirection = moveDirectionX;
                //transform.position += moveDirection * moveSpeed * Time.deltaTime;
            } 
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveSpeed * Time.deltaTime);
                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                    //transform.position += moveDirection * moveSpeed * Time.deltaTime;
                }
            }
        }

        isWalking = moveDirection != Vector3.zero;
        if (inputVector != new Vector2(0, 0))
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
        Debug.Log(inputVector);
    }
    public bool IsWalking()
    {
        return isWalking;
    }
}
