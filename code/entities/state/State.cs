using Sandbox;
using System.Security.Cryptography.X509Certificates;

public abstract class State
{
	protected Entity actingEntity { get; set; }

	public State ( Entity actingEntity )
	{
		this.actingEntity = actingEntity;
	}

	public virtual void OnEnter()
	{
	}
	public virtual void OnTick()
	{
	}
	public virtual void OnHurt()
	{
	}
	public virtual void OnExit()
	{
	}
}
