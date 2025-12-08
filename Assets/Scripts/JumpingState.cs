using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class JumpingState : PlayerState
{
    public override void EnterState(PlayerController player)
    {
        TryPlayAnimation(player, "Jump");

        Vector2 velocity = player.rb.linearVelocity;
        velocity.y = player.jumpForce;
        player.rb.linearVelocity = velocity;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayJumpSound();
        }
    }
    public int canDash = 1;
    public bool isDashing;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    
    public override void UpdateState(PlayerController player)
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 velocity = player.rb.linearVelocity;
        velocity.x = horizontal * player.moveSpeed;
        player.rb.linearVelocity = velocity;

        if (horizontal < 0)
            player.spriteRenderer.flipX = true;
        else if (horizontal > 0)
            player.spriteRenderer.flipX = false;

        if (player.IsGrounded() && player.rb.linearVelocity.y <= 0)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                player.ChangeState(new MovingState());
            }
            else
            {
                player.ChangeState(new IdleState());
            }
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
        if (player.IsGrounded())


       
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == 1)
        {
            canDash = 0;
            isDashing = true;
            float orginialGravity = player.rb.gravityScale;
            while (isDashing && dashingTime >= 0f)
            {
                dashingTime -= 0.1f;    
                player.rb.gravityScale = 0f;
                velocity.x = horizontal * player.dashForce;
            }

            player.rb.gravityScale = orginialGravity;
            isDashing = false;
            dashingTime = 0.2f;
           
            canDash = 1;
            Debug.Log("Dash used");
        }
    }

    

    public override void ExitState(PlayerController player) { }

    public override string GetStateName() => "Jumping";

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