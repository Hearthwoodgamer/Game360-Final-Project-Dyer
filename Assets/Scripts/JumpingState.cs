using UnityEngine;

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
    int dashcharge = 1;
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

            dashcharge = 1;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashcharge == 1)
        {
            dashcharge = 0;
            Vector2 dashvelocity = player.rb.linearVelocity;
            dashvelocity.x = horizontal * player.dashForce;
            player.rb.linearVelocity = dashvelocity;
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