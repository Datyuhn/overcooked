using System.Collections;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float playerRadius = .7f;
    [SerializeField] private float playerHeight = 10f;
    [SerializeField] float interactDistance = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;
    private bool isWalking;
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedClearCounter;
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }
    private void HandleMovement()
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
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            } 
            else SetSelectedCounter(null);
        }
        else SetSelectedCounter(null);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedClearCounter = selectedCounter
        });

    }

    private void Awake()
    {
        if (Instance != null) Debug.Log("There are more than one Player instance");
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    public bool IsWalking()
    {
        return isWalking;
    }
}
