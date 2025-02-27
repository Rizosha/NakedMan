using UnityEngine;
using UnityEngine.InputSystem;

public class MovementState: StateInterface
{ 
   

    private PlayerController controller;

    public MovementState(PlayerController playerController)
    {
        controller = playerController;
    }
    
    public void EnterState()
    {
        Debug.Log("Entering Movement State");
        //controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
    }

    public void UpdateState()
    {
        controller.x = controller.move.action.ReadValue<Vector2>().x;
        controller.z = controller.move.action.ReadValue<Vector2>().y;
        controller.moveDir = controller.transform.right * controller.x + controller.transform.forward * controller.z;

        if (controller.moveDir != Vector3.zero)
        {
            controller.lastDir = controller.moveDir;
        }
        
        controller.rb.MovePosition(controller.rb.position + Vector3.ClampMagnitude(controller.moveDir, 1) * controller.speed * Time.deltaTime);

        controller.rb.linearVelocity = new Vector3(controller.rb.linearVelocity.x, 0, controller.rb.linearVelocity.z);
        
        if(Input.GetKeyDown("space") && !controller.isDodging)
        {
            controller.ChangeState(new DodgeState(controller));
        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Movement State");
    }
}