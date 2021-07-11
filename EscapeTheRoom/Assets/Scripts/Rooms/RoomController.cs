using System.Collections.Generic;
using System.Linq;
using EscapeTheRoom.Items;
using UnityEngine;


namespace EscapeTheRoom.Rooms
{
    public class RoomController : Base.BaseMono
    {
        [SerializeField] private Transform _center;
        [SerializeField] private List<Walls.Wall> _walls;

        private List<SpawnPosition> _additionalObjects;

        private bool _assembled;

        public Vector3 Center
        {
            get {return _center.position;}
        }
        public bool Assembled
        {
            get {return _assembled;}
        }


        protected override void Awake()
        {
            base.Awake();

            if(_center == null)
                _center = Transform;

            _additionalObjects = GetComponentsInChildren<SpawnPosition>().ToList();
        }


        public void Assemble()
        {
            foreach(var wall in _walls)
            {
                wall.Assemble();
            }

            // Additional objects
            foreach(var spawn in _additionalObjects)
            {
                var instance = Instantiate(spawn.Item, spawn.transform);
                spawn.SpawnedItem = instance;
                instance.transform.position =
                    spawn.Positions[Random.Range(0, spawn.Positions.Count)].position;
            }

            _assembled = true;
        }

        public void Disassemble()
        {
            foreach(var wall in _walls)
            {
                wall.Disassemble();
            }

            foreach(var spawn in _additionalObjects)
            {
                Destroy(spawn.SpawnedItem);
            }

            _assembled = false;
        }
    }
}
