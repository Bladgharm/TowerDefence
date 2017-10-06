using System.Linq;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerResourcesInventory : MonoBehaviour
    {
        public Resource[] Resources;

        public void AddResource(ResourceType resourceType, int quantity = 1)
        {
            if (Resources.Any(r => r.Type == resourceType))
            {
                var resource = Resources.FirstOrDefault(r => r.Type == resourceType);

                if (resource != null)
                {
                    if (resource.Quantity < resource.MaxQuantity)
                    {
                        resource.Quantity += quantity;
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class Resource
    {
        public ResourceType Type;
        public int Quantity;
        public int MaxQuantity;
    }
}