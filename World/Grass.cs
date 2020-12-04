using Godot;
using System;

public class Grass : Node2D
{
  //GrassEffect kann hier rein refactored werden für Performance
  //
  PackedScene grassEffectScene;
  GrassEffect grassEffect;
  Node2D world;

    public override void _Ready()
    {
        grassEffectScene = (PackedScene)ResourceLoader.Load("res://Effects/GrassEffect.tscn");
        world = (Node2D)GetTree().CurrentScene;
        grassEffect = (GrassEffect)grassEffectScene.Instance();
    }



    //Beim refactoren, disable Hurtbox/Collisionshape damit nicht mehrmals getroffen wird
    public void CreateGrassEffect()
    {
              grassEffectScene = (PackedScene)ResourceLoader.Load("res://Effects/GrassEffect.tscn");
              world = (Node2D)GetTree().CurrentScene;
              grassEffect = (GrassEffect)grassEffectScene.Instance();          
              world.AddChild(grassEffect);
              grassEffect.GlobalPosition = GlobalPosition;

    }

    public void OnHurtboxAreaEntered(Area2D area)
    {

        CreateGrassEffect();
        QueueFree();               


    }
}
