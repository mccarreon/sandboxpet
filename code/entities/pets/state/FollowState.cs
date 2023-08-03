using Sandbox;

public class FollowState : PetState
{
	public TimeSince TimeSinceGeneratedPath = 0;
	public FollowState( Entity actingEntity ) : base( actingEntity )
	{

	}

	public override void OnEnter()
	{
		Log.Info( "Entering follow mode" );
	}
	public override void OnTick()
	{
		if (DistanceFromOwner < Pet.followThreshold * 0.2)
		{
			Pet.StateController.ChangeState( new IdleState( Pet ) );
		}

		if ( TimeSinceGeneratedPath >= 1)
		{
			TimeSinceGeneratedPath = 0;
			Pet.GeneratePathToVector( Pet.Owner.Position );
		}

		Pet.TraversePath();
	}
	public override void OnExit()
	{
		Log.Info( "Exiting follow mode" );
	}
}
