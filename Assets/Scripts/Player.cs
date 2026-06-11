using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking = false;
    [SerializeField] private GameInput gameInput;
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir =  new Vector3(inputVector.x, 0f, inputVector.y);
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,playerRadius,moveDir,moveDistance);
        if (!canMove) {
                // Cannot move towards moveDir, try only X movement
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

                if(canMove) {
                    moveDir = moveDirX;
                } 
                else {
                    // Cannot move towards X, try only Z movement
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                    
                    if (canMove)
                    {
                        moveDir = moveDirZ;
                    } else { }
                }
            
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
            isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime* rotateSpeed);    
    }

    public bool IsWalking() => isWalking;
   
}
