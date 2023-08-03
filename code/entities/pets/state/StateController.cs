using Sandbox;
using System.Runtime.CompilerServices;

public class StateController : EntityComponent<Pet>
{
	private State currentState;
	public void OnTick()
	{
		if (currentState != null)
		{
			currentState.OnTick();
		}
		else
		{
			currentState = new IdleState( Entity );
		}
	}
	public void ChangeState(State newState)
	{
		if (currentState != null)
		{
			currentState.OnExit();
		}
		currentState = newState;
		currentState.OnEnter();
	}
}
