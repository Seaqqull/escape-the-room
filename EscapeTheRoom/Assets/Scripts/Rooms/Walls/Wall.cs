using EscapeTheRoom.Utilities.Methods;
using EscapeTheRoom.Rooms.Walls.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace EscapeTheRoom.Rooms.Walls
{
    public class Wall : Base.BaseMono
    {
        [System.Serializable]
        private class OnWallObject
        {
            public GameObject Object;
            public float SizeAlong;
        }


        [SerializeField] private Side _side;
        [SerializeField] private bool _randomOrder;
        [SerializeField] private bool _randomPlacement;
        [Space]
        [Header("Objects")]
        [SerializeField] private GameObject _wallObject;
        [SerializeField] private List<OnWallObject> _onWallObjects;

        public int CountOfObjects
        {
            get {return _side.Spans.Count;}
        }
        public Vector3 Size
        {
            get {return _side.Size;}
        }
        public bool Assembled {get; private set;}

        protected override void Awake()
        {
            base.Awake();

            Random.InitState(System.DateTime.Now.Millisecond);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;


            var halfSize = _side.Size / 2;

            var horizontal = transform.right.normalized;
            var depth = transform.forward.normalized;
            var vertical =  transform.up.normalized;

            Gizmos.DrawLine(transform.position + (-horizontal) * halfSize.x, transform.position + horizontal * halfSize.x);
            Gizmos.DrawLine(transform.position + (-vertical) * halfSize.y, transform.position + vertical * halfSize.y);
            Gizmos.DrawLine(transform.position + (-depth) * halfSize.z, transform.position + depth * halfSize.z);
        }


        private Span GetSpan(GameObject wallObject, float randomPosition, float alignedPosition, float sizeAlong)
        {
            var instance = Instantiate(wallObject);

            // Apply rotation to shift Vector
            var rotatedPosition = transform.rotation * new Vector3(alignedPosition, 0, 0);

            // Apply parameters
            instance.transform.localScale = new Vector3(sizeAlong, _side.Size.y, _side.Size.z);
            instance.transform.position = (transform.position + rotatedPosition);
            instance.transform.rotation = transform.rotation;
            instance.transform.parent = transform;

            return new Span() {
                Object = instance,
                Position = randomPosition,
                Bounds = new Bound(
                    randomPosition - sizeAlong / 2,
                    randomPosition + sizeAlong / 2
                )
            };
        }


        public void Assemble()
        {
            var halfSize = _side.Size.x / 2;
            var onWallIndexes = Enumerable.Range(0, _onWallObjects.Count).ToList();
            if(_randomOrder)
                onWallIndexes.Shuffle(System.DateTime.Now.Millisecond);

            // Generate objects on wall
            foreach (int i in onWallIndexes)
            {
                float randomPosition = (_randomPlacement) ? _side.GetRandomPosition(_onWallObjects[i].SizeAlong) :
                    _side.GetNextPosition(_onWallObjects[i].SizeAlong);
                if(randomPosition == -1)
                    continue;


                float alignedPosition = randomPosition - halfSize;

                _side.Insert(
                    GetSpan(_onWallObjects[i].Object, randomPosition, alignedPosition, _onWallObjects[i].SizeAlong)
                );

            }


            // Generate walls
            var freePositions = _side.GetAllBounds(float.Epsilon);
            // Chache added walls, so we can properly calculate free space between the wall objects
            var wallSpans = new List<Span>();

            for (int i = 0; i < freePositions.Count; i++)
            {
                int index = freePositions[i].SpanIndex;
                var bounds = _side.GetBounds(index);

                float spanPosition = bounds.Left + (bounds.SizeAlong / 2);
                float alignedPosition = spanPosition - halfSize;

                wallSpans.Add(
                    GetSpan(_wallObject, spanPosition, alignedPosition, bounds.SizeAlong)
                );
            }

            // Save walls
            wallSpans.ForEach(span => _side.Insert(span));

            Assembled = true;
        }

        public void Disassemble()
        {
            for (int i = _side.Spans.Count - 1; i >= 0; i--)
            {
                Destroy(_side.Spans[i].Object);
            }
            _side.Spans.Clear();

            Assembled = false;
        }

        public void RemoveObject(int index)
        {
            if(index < 0 || index >= _side.Spans.Count)
                throw new System.IndexOutOfRangeException($"Wrong index({index}) for removing wall object");

            _side.Spans.RemoveAt(index);
        }

        public GameObject GetObject(int index)
        {
            if(index < 0 || index >= _side.Spans.Count)
                throw new System.IndexOutOfRangeException($"Wrong index({index}) for getting wall object");

            return _side.Spans[index].Object;
        }

    }
}
