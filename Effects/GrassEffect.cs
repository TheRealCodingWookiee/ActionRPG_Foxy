using Godot;
using System;

public class GrassEffect : Node2D
{

    AnimatedSprite animatedGrass;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedGrass = GetNode<AnimatedSprite>("AnimatedGrass");
        animatedGrass.Frame = 0;
        animatedGrass.Play("AnimateGrass");
    }

    public void OnAnimationFinished()
    {
        QueueFree();
    }

}
