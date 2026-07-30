using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen object here, give it to the player
            if (player.HasKitchenObject())
            {
                //player is carrying
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Do nothing
            }
        }
        else
        {
            // There is a kitchen object here, give it to the player
            if (player.HasKitchenObject())
            {
                //player is carrying something, do nothing
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        KitchenObject.DestroyKitchenObject(GetKitchenObject());
                    }
                }
                else
                {
                    //player is not carrying plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            KitchenObject.DestroyKitchenObject(player.GetKitchenObject());
                        }
                    }
                }
            }
            else
            {
                //player is not carrying anything, give it to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
