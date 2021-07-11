using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace EscapeTheRoom.Rooms.Walls.Data
{
    public struct Bound
    {
        public readonly static Bound zero = new Bound(0,0);

        public float Right;
        public float Left;

        public float SizeAlong
        {
            get {return Right - Left;}
        }



        public Bound(float left, float right)
        {
            Left = left;
            Right = right;
        }
    }

    [System.Serializable]
    public class Side
    {
        [SerializeField] private Vector3 _size;

        public List<Span> Spans {get; private set;}
        public Vector3 Size
        {
            get {return _size;}
        }


        public Side()
        {
            Spans = new List<Span>();
        }



        public float GetFreeAmount()
        {
            return (_size.x - Spans.Sum(span => span.Bounds.SizeAlong));
        }

        public void Insert(Span span)
        {
            int insertIndex = 0;

            for (; insertIndex < Spans.Count; insertIndex++)
            {
                if(Spans[insertIndex].Position > span.Position)
                    break;
            }

            Spans.Insert(insertIndex, span);
        }

        public Bound GetBounds(int spanIndex)
        {
            if(Spans.Count == 0 || spanIndex < 0 || spanIndex > Spans.Count)
                return Bound.zero;

            if(spanIndex == 0)
                return new Bound(0, Spans[0].Bounds.Left);
            if(spanIndex == Spans.Count)
                return new Bound(Spans[spanIndex - 1].Bounds.Right, _size.x);

            return new Bound(Spans[spanIndex - 1].Bounds.Right, Spans[spanIndex].Bounds.Left);
        }

        public float GetFreeAmount(int spanIndex)
        {
            if(Spans.Count == 0)
                return _size.x;
            if(spanIndex < 0 || spanIndex > Spans.Count)
                return 0;

            if(spanIndex == 0)
                return Spans[0].Bounds.Left;
            if(spanIndex == Spans.Count)
                return _size.x - Spans[spanIndex - 1].Bounds.Right;

            return (Spans[spanIndex].Bounds.Left - Spans[spanIndex - 1].Bounds.Right);
        }

        public float GetNextPosition(float sizeAlong)
        {
            if(Spans.Count == 0)
                return sizeAlong / 2;
            if(GetFreeAmount() < sizeAlong)
                return -1;

            var allowedPositions = GetAllBounds(sizeAlong);

            if(allowedPositions.Count == 0)
                return -1;

            int selectedIndex = allowedPositions[0].SpanIndex;
            var selectedBounds = GetBounds(selectedIndex);

            return selectedBounds.Left + (sizeAlong / 2);
        }

        public float GetRandomPosition(float sizeAlong)
        {
            if(Spans.Count == 0)
                return Random.Range(sizeAlong / 2, _size.x - (sizeAlong / 2));
            if(GetFreeAmount() < sizeAlong)
                return -1;

            var allowedPositions = GetAllBounds(sizeAlong);

            if(allowedPositions.Count == 0)
                return -1;


            int selectedIndex = allowedPositions[Random.Range(0, allowedPositions.Count)].SpanIndex;
            var selectedBounds = GetBounds(selectedIndex);

            return Random.Range(selectedBounds.Left + (sizeAlong / 2), selectedBounds.Right - (sizeAlong /2));
        }

        public bool IsIntersect(float positionAlong, float halfSizeAlong)
        {
            var boundsAlong = new Vector2(positionAlong - halfSizeAlong, positionAlong + halfSizeAlong);

            for(int i = 0; i < Spans.Count; i++)
            {
                if(Spans[i].InBounds( boundsAlong.x) || Spans[i].InBounds( boundsAlong.y)) // Add check whether span inside given parameters
                    return true;
            }

            return false;
        }

        public List<(int SpanIndex, float FreeSpace)> GetAllBounds(float minSize)
        {
            var allowedPositions = new List<(int SpanIndex, float FreeSpace)>();

            for (int i = 0; i <= Spans.Count; i++)
            {
                allowedPositions.Add((i, GetFreeAmount(i)));
            }

            return allowedPositions
                .Where(position => (position.FreeSpace >= minSize)).ToList();
        }

    }

    public class Span
    {
        public GameObject Object {get; set;}
        public float Position {get; set;}
        public Bound Bounds {get; set;}



        public bool InBounds(float positionAlong)
        {
            return (positionAlong >= Bounds.Left) && (positionAlong <= Bounds.Right);
        }
    }
}
