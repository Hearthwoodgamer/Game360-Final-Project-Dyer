using UnityEngine;

public class MovingState : PlayerState
{
    public override void EnterState(PlayerController player)
    {
        TryPlayAnimation(player, "Run");
    }


    int dashcharge = 1;
    public override void UpdateState(PlayerController player)
    {
        if (player.IsGrounded())

            dashcharge = 1;

        float horizontal = Input.GetAxis("Horizontal");
        
        Vector2 velocity = player.rb.linearVelocity;
        velocity.x = horizontal * player.moveSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashcharge == 1)
        {
            dashcharge = 0;
            
            velocity.x = horizontal * player.dashForce * player.moveSpeed;
            player.rb.linearVelocity = velocity;
            Debug.Log("Dash used");
        }

        player.rb.linearVelocity = velocity;

        if (horizontal < 0)
            player.spriteRenderer.flipX = true;
        else if (horizontal > 0)
            player.spriteRenderer.flipX = false;
            

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
        {
            player.ChangeState(new JumpingState());
        }
        else if (Mathf.Abs(horizontal) < 0.1f)
        {
            player.ChangeState(new IdleState());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.Fire();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.Fire1();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.Fire2();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.Fire3();
        }



        
        
    }

    public override void ExitState(PlayerController player) { }

    public override string GetStateName() => "Moving";

    private void TryPlayAnimation(PlayerController player, string animName)
    {
        if (player.animator != null &&
            player.animator.runtimeAnimatorController != null &&
            player.animator.isActiveAndEnabled)
        {
            try
            {
                player.animator.Play(animName);
            }
            catch
            {
                // Animation doesn't exist - continue without it
            }
        }
    }
}
