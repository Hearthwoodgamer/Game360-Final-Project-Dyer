using UnityEngine;

public class IdleState : PlayerState
{
    public override void EnterState(PlayerController player)
    {
        // Safe animation - only plays if everything is set up
        TryPlayAnimation(player, "Idle");
    }

    public override void UpdateState(PlayerController player)
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
        {
            player.ChangeState(new JumpingState());
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            player.ChangeState(new MovingState());
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

    public override string GetStateName() => "Idle";

    // Safe animation helper
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
                // Animation doesn't exist - that's okay, continue without it
            }
        }
    }
}

