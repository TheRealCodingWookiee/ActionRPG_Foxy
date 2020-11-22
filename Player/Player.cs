using Godot;
using System;

public class Player : KinematicBody2D
{

    public Vector2 velocity = Vector2.Zero;
    public const float MaxSpeed = 100f;
    public const float Acceleration = 500f;
    public float Friction = 500;

    public AnimationPlayer animationPlayer;
    public AnimationTree animationTree;
    public AnimationNodeStateMachinePlayback animationState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       animationPlayer = GetNode("AnimationPlayer") as AnimationPlayer;
       animationTree = GetNode("AnimationTree") as AnimationTree;
       animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    Vector2 input_vector = Vector2.Zero;
    input_vector.x  = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
    input_vector.y  = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
    input_vector = input_vector.Normalized();
    
    if (input_vector != Vector2.Zero)
    {
        animationTree.Set("parameters/Idle/blend_position", input_vector);
        animationTree.Set("parameters/Run/blend_position", input_vector);
        animationState.Travel("Run");
        
        velocity = velocity.MoveToward(input_vector * MaxSpeed, Acceleration * delta);

    } else
    {
        animationState.Travel("Idle");
        velocity = velocity.MoveToward(Vector2.Zero, Friction * delta);
    }
      GD.Print(velocity);
      velocity = MoveAndSlide(velocity);
  }
}
