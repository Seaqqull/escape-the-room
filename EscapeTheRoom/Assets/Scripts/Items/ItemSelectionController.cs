using System.Collections.Generic;
using UnityEngine;


namespace EscapeTheRoom.Items
{
    public class ItemSelectionController : MonoBehaviour
    {
        [SerializeField][Range(0, 1)] private float _colorScale;
        [SerializeField] private List<GameObject> _views;

        private List<MaterialPropertyBlock> _viewsBlock;
        private List<Renderer> _viewsRenderers;
        private List<Color> _colors;

        private bool _selected;
        private bool _analyzed;


        private void Awake()
        {
            _viewsBlock = new List<MaterialPropertyBlock>();
            _viewsRenderers = new List<Renderer>();
            _colors = new List<Color>();

            for (int i = 0; i < _views.Count; i++)
            {
                var renderer = _views[i].GetComponent<Renderer>();
                var propertyBlock = new MaterialPropertyBlock();

                _viewsRenderers.Add(renderer);
                _viewsBlock.Add(propertyBlock);
            }
        }


        private void UpdateData()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                _viewsRenderers[i].GetPropertyBlock(_viewsBlock[i]);
            }
        }

        private void Update()
        {
            UpdateData();

            if(!_selected && _analyzed)
                _colors.Clear();

            for (int i = 0; i < _views.Count; i++)
            {
                var property = _viewsBlock[i];
                var color = property.GetColor("_Color");
                _colors.Add(color);

                property.SetColor("_Color", color * _colorScale);
                _viewsRenderers[i].SetPropertyBlock(property);
            }
        }


        public void OnSelect()
        {
            _selected = true;
            _analyzed = false;
        }

        public void OnDeselect()
        {
            _selected = false;
            _analyzed = false;
        }
    }
}
