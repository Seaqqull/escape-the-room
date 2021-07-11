using System.Collections.Generic;
using UnityEngine;


namespace EscapeTheRoom.Rooms
{
    public class RoomController : Base.BaseMono
    {
        [SerializeField] private Transform _center;
        [SerializeField] private List<Walls.Wall> _walls;

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
        }


        public void Assemble()
        {
            foreach(var wall in _walls)
            {
                wall.Assemble();
            }

            _assembled = true;
        }

        public void Disassemble()
        {
            foreach(var wall in _walls)
            {
                wall.Disassemble();
            }

            _assembled = false;
        }
    }
}
