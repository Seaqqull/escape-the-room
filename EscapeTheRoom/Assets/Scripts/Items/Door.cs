


namespace EscapeTheRoom.Items
{
    public class Door : Item
    {
        protected override void Pickup()
        {
            base.Pickup();

            Managers.RoundManager.Instance.Stop();
        }
    }
}
