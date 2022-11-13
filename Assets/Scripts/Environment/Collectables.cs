public abstract class Collectables : Interactables
{
    public override void Interact()
    {
        PickUpItem();
        Deselect();
    }

    private void PickUpItem()
    {
        
    }

    private void PlaceItem()
    {
        
    }
}
