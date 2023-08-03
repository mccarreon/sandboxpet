using Sandbox;

public abstract class PetState : State
{
	public Pet Pet { get; set; }
	public float DistanceFromOwner
	{
		get => Pet.Owner.Position.Distance( Pet.Position );
	}
	protected PetState( Entity actingEntity ) : base( actingEntity )
	{
		Pet = actingEntity as Pet;
	}


}
