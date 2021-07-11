using System.Collections.Generic;
using UnityEngine;


namespace EscapeTheRoom.Items
{
    public class ItemSelectionController : MonoBehaviour
    {
        [SerializeField][Range(0, 1)] private float _colorScale;
        [SerializeField] private List<GameObject> _views;

        private List<Renderer> _viewsRenderers;
        private List<Color> _colors;

        private bool _selected;


        private void Awake()
        {
            _viewsRenderers = new List<Renderer>();
            _colors = new List<Color>();

            for (int i = 0; i < _views.Count; i++)
            {
                var renderer = _views[i].GetComponent<Renderer>();
                _viewsRenderers.Add(renderer);

                _colors.Add(renderer.material.color);
            }
        }


        public void OnSelect()
        {
            _selected = true;

            for (int i = 0; i < _views.Count; i++)
            {
                _viewsRenderers[i].material.color = _colors[i] * _colorScale;
            }
        }

        public void OnDeselect()
        {
            _selected = false;

            for (int i = 0; i < _views.Count; i++)
            {
                _viewsRenderers[i].material.color = _colors[i];
            }
        }
    }
}
