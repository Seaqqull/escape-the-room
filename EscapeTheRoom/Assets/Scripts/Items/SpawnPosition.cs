using System.Collections.Generic;
using UnityEngine;


namespace EscapeTheRoom.Items
{
    public class SpawnPosition : MonoBehaviour
    {
        [SerializeField] private GameObject _item;
        [SerializeField] private List<Transform> _positions;

        public List<Transform> Positions
        {
            get {return _positions;}
        }
        public GameObject SpawnedItem {get; set;}
        public GameObject Item
        {
            get {return _item;}
        }

    }
}
