
using Editor;
using MyGame;
using Sandbox;
using System.Transactions;

[EditorModel( "models/citizen/citizen.vmdl" )]
[Library( "pet_terry" ), HammerEntity]
[Title( "Pet Terry" ), Category( "NPCs" )]
public class PetTerry : Pet
{
	public PetTerry()
	{

	}
	public PetTerry( IClient client ) : base(client)
	{
		ModelPath = "models/citizen/citizen.vmdl";
	}
}
