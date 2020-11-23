using Godot;
using System;



    public enum AnimationStates
    {
        MOVE, 
        ROLL,
        ATTACK
    }

public class Player : KinematicBody2D
{

    public Vector2 velocity = Vector2.Zero;
    public const float MaxSpeed = 100f;
    public const float Acceleration = 500f;
    public float Friction = 500;
    public AnimationStates animationStates;

    public AnimationPlayer animationPlayer;
    public AnimationTree animationTree;
    public AnimationNodeStateMachinePlayback animationNodeStateMachine;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       animationPlayer = GetNode("AnimationPlayer") as AnimationPlayer;
       animationTree = GetNode("AnimationTree") as AnimationTree;
       animationTree.Active = true;
       animationNodeStateMachine = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
    {



      switch (animationStates)
      {
          case AnimationStates.MOVE:          
            MoveState(delta);
            break;
          case AnimationStates.ATTACK:
            AttackState(delta);
            break;
          case AnimationStates.ROLL:
            break;            
          default: break;
      }
    }

    private void AttackState(float delta)
    {
      animationNodeStateMachine.Travel("Attack");
    }

    public void FinishAttack()
    {
      velocity = Vector2.Zero;
      animationStates = AnimationStates.MOVE;
    }
    private void MoveState(float delta)
    {
        Vector2 movement_vector = Vector2.Zero;
        movement_vector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        movement_vector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        movement_vector = movement_vector.Normalized();

        if (movement_vector != Vector2.Zero)
        {
            animationTree.Set("parameters/Idle/blend_position", movement_vector);
            animationTree.Set("parameters/Run/blend_position", movement_vector);
            animationTree.Set("parameters/Attack/blend_position", movement_vector);
            animationNodeStateMachine.Travel("Run");

            velocity = velocity.MoveToward(movement_vector * MaxSpeed, Acceleration * delta);

        }
        else
        {
            animationNodeStateMachine.Travel("Idle");
            velocity = velocity.MoveToward(Vector2.Zero, Friction * delta);
        }

        if (Input.IsActionJustPressed("attack"))
        {
            animationStates = AnimationStates.ATTACK;
        }

        velocity = MoveAndSlide(velocity);
    }
}
