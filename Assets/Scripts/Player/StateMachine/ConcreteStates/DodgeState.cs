using System.Collections;
using UnityEngine;

    public class DodgeState: StateInterface
    {
        private PlayerController controller;

     
        public DodgeState(PlayerController playerController)
        {
            controller = playerController;
        }

        public void EnterState()
        {
            controller.isDodging = true;
            controller.StartCoroutine(Dodge());
       
            Debug.Log("Entering Dodge State");
            
        }

        public void UpdateState()
        {
           
        }

        IEnumerator Dodge()
        { 
            float elapsedTime = 0f;

         while (elapsedTime < controller.dodgeTime)
         { 
             float t = elapsedTime / controller.dodgeTime;
             
             float speedMultiplier = Mathf.Sin(t * Mathf.PI); 
             controller.rb.MovePosition(controller.rb.position + controller.moveDir * controller.dodgeSpeed * speedMultiplier * Time.deltaTime);

             elapsedTime += Time.deltaTime;
             yield return null;
         }

         controller.isDodging = false;
         controller.ChangeState(new MovementState(controller));
        }

        public void ExitState()
        {
            Debug.Log("Exiting Dodge State");
        }
    }
