using MyGame;
using Sandbox;
using Sandbox.UI;
using System.Buffers;
using System.Linq;

public class Pet : AnimatedEntity
{
	[BindComponent] public StateController StateController { get; }
	public float distanceFromOwner => Owner.Position.Distance( Position );
	public string ModelPath { get; set; }
	public virtual float followThreshold { get; set; } = 250f;
	public float MovementSpeed { get; set; } = 2f;
	public string PetName { get; set; }

	private Vector3[] Path;
	private int CurrentPathSegment;
	

	public Pet()
	{
		Owner = null;
		PetName = "Stray";
	}
	public Pet( IClient client )
	{
		Owner = client.Pawn as Pawn;
		PetName = $"{Owner.Client.Name}'s Pet";
	}

	public override void Spawn()
	{
		SetModel( ModelPath );
		this.Transform = this.Transform.WithScale( 0.25f );
		Components.Create<StateController>();
	}

	[GameEvent.Tick.Server]
	private void Tick()
	{
		StateController.OnTick();
	}
	public void ClearPath() { Path = null; }

	public void GeneratePathToVector( Vector3 target )
	{
		Path = NavMesh.PathBuilder( Position )
			.WithMaxClimbDistance( 16f )
			.WithMaxDropDistance( 16f )
			.WithStepHeight( 16f )
			.WithMaxDistance( 99999999 )
			.WithPartialPaths()
			.Build( target )
			.Segments.Select( x => x.Position )
			.ToArray();

		CurrentPathSegment = 0;
	}
	public void TraversePath()
	{
		if ( Path == null )
		{
			return;
		}

		var distanceToTravel = MovementSpeed;

		while ( distanceToTravel > 0 )
		{
			var currentTarget = Path[CurrentPathSegment];
			var distanceToCurrentTarget = Position.Distance( currentTarget );

			if ( distanceToCurrentTarget > distanceToTravel )
			{
				var direction = (currentTarget - Position).Normal;
				Position += direction * distanceToTravel;
				return;
			}
			else
			{
				var direction = (currentTarget - Position).Normal;
				Position += direction * distanceToCurrentTarget;
				distanceToTravel -= distanceToCurrentTarget;
				CurrentPathSegment++;
			}

			if ( CurrentPathSegment == Path.Count() )
			{
				Path = null;
				return;
			}
		}
	}

}
