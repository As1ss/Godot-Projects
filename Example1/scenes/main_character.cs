using Godot;
using System;

public partial class main_character : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = -900.0f;
	AnimatedSprite2D sprite;
	Label labelSaltos;
	Label labelFPS;
	int contadorSaltos=0;
	





	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		

		Vector2 velocity = Velocity;
		sprite = (AnimatedSprite2D)GetNode("Sprite2D");
		labelSaltos = (Label)GetNode("../Label");
		labelSaltos.Text = "Saltos: " + contadorSaltos;
		labelFPS = (Label)GetNode("../LabelFps");
		labelFPS.Text = "FPS: " + Engine.GetFramesPerSecond();
	

		if (Input.IsActionPressed("right"))
		{
			sprite.FlipH = false;
		}
		else if (Input.IsActionPressed("left"))
		{
			sprite.FlipH = true;
		}
		if (velocity.X > 0 || velocity.X < 0)
		{
			sprite.Animation = "Running";
		}
		else
		{
			sprite.Animation = "Idle";
		}



		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
			sprite.Animation = "Jump";
		
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			contadorSaltos++;
			
			

		}





		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;

		}
		else
		{

			velocity.X = Mathf.MoveToward(Velocity.X, 0, 50);
		}


		Velocity = velocity;
		MoveAndSlide();







	}
	

}
