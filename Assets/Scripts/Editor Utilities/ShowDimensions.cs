using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Editor_Utilities
{
    public class ShowDimensions : MonoBehaviour
    {
#if UNITY_EDITOR
        
        [Header("Dimensions")]
        [ShowInInspector, ReadOnly] private float _width;
        [ShowInInspector, ReadOnly] private float _height;
        [ShowInInspector, ReadOnly] private float _depth;

        private Collider _collider;

        private void OnDrawGizmos()
        {
            _collider = GetComponent<Collider>();
            
            if (_collider != null)
            {
                _width = _collider.bounds.size.x;
                _height = _collider.bounds.size.y;
                _depth = _collider.bounds.size.z;
                
                GUIStyle textStyle = new GUIStyle();
                textStyle.normal.textColor = Color.white; 
                textStyle.fontStyle = FontStyle.Bold; 
                
                Vector3 labelPosition = _collider.bounds.center;
                Handles.Label(labelPosition, $"{gameObject.name} : {_width:F2} / {_height:F2} / {_depth:F2}", textStyle);
                
                
            }
        }
        
#endif
    }
}
