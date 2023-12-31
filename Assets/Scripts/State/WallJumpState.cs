using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : BaseState
{
    public WallJumpState( PlayerController pController, Inputs inputManager) : base(pController,inputManager,stateName: StateName.WALLJUMP)
    {
        //Debug.Log("WallJumpState 생성");
    }

    public override void OnEnterState()
    {
        pController.isWallJumping = true;
    }

    public override void OnUpdateState()
    {
        if (pController.wallJumpCounter >  0) // isWallJump-ing
        {
            pController._controller.Move(pController.wallJumpVector.normalized * (Time.deltaTime * 15.0f) +
                             new Vector3(0.0f, pController._verticalVelocity, 0.0f) * Time.deltaTime);

            pController.wallJumpCounter -= Time.deltaTime; 
        }
        else
        { 
            pController.stateMachine.ChangeState(StateName.WALK); 
        }

        if (inputManager.dash && pController.dashCounter > 0) // 벽점프 중 Dash입력시 벽점프 캔슬
        {
            pController.wallJumpCounter = 0f;
            pController.stateMachine.ChangeState(StateName.DASH);
        }
    }

    public override void OnFixedUpdateState()
    {}

    public override void OnExitState()
    {
        pController.isWallJumping = false;
    }

    public override void HandleInputs(){}

}