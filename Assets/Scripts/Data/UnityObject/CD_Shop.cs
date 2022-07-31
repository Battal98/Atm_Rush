using UnityEngine;
using Data.ValueObject;

namespace Data.UnityObject
{

    [CreateAssetMenu(menuName = "CD_Shop/Charcters",fileName = "CD_Shop/Charcters", order = 0)]
    public class CD_Shop : ScriptableObject
    {
        public ShopData shopData;
    } 
}
