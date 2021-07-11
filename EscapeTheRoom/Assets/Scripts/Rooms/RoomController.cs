using System.Collections.Generic;
using UnityEngine;


namespace EscapeTheRoom.Rooms
{
    public class RoomController : Base.BaseMono
    {
        [SerializeField] private List<Walls.Wall> _walls;

        private bool _assembled;

        public bool Assembled
        {
            get {return _assembled;}
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
