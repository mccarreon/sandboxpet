using Sandbox;

public class IdleState : PetState
{
	public IdleState( Entity actingEntity ) : base( actingEntity )
	{
	}

	public override void OnEnter()
	{
		Log.Info( "Entering idle mode" );
		Pet.ClearPath();

	}
	public override void OnTick()
	{
		if (getDistanceFromOwner() > Pet.followThreshold)
		{
			Pet.StateController.ChangeState( new FollowState( Pet ) );
		}
	}
	public override void OnExit()
	{
		Log.Info( "Exiting idle mode" );
	}

	private float getDistanceFromOwner()
	{
		return actingEntity.Owner.Position.Distance( actingEntity.Position );
	}
}
