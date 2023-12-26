namespace Selivura
{
    public class Checkpoint : Trigger
    {
        protected override void OnTriggeredPlayer(Player player)
        {
            player.SetCheckpoint(this);
        }
    }
}
