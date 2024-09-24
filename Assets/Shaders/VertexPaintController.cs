using UnityEngine;

namespace Shaders
{
    public class VertexPaintController : MonoBehaviour
    {
        [SerializeField] private Texture2D m_ColorMaskBase;
        [SerializeField] private Texture2D m_HitTexture;
        
        private Camera _camera;
        private Renderer _rend; 
        private Texture2D _templateColorMask;
        private static readonly int ColorMask = Shader.PropertyToID("_ColorMask");


        private void Start()
        { 
            _camera = Camera.main;
            
            _rend = GetComponent<MeshRenderer>();
            
           CreatTexture();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) CastClickRay();
        }

        private void CastClickRay()
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            Ray ray = _camera.ScreenPointToRay(new Vector3(screenCenter.x, screenCenter.y, _camera.nearClipPlane));

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                MeshCollider meshCollider = hit.collider as MeshCollider;
                
                if (_rend == null || _rend.sharedMaterial == null || meshCollider == null) return;
               
                Vector2 textureCoordinate = hit.textureCoord;
                StartPaint(textureCoordinate);
            }

        }
        
        private void StartPaint(Vector2 textureCoordinate)
        {
            int pixelX = (int)(textureCoordinate.x * _templateColorMask.width);
            int pixelY = (int)(textureCoordinate.y * _templateColorMask.height);
        
            for (int x = 0; x < m_HitTexture.width; x++)
            {
                for (int y = 0; y < m_HitTexture.height; y++)
                {
                    Color pixelColor = m_HitTexture.GetPixel(x, y);
                    Color pixelColorMask = _templateColorMask.GetPixel(pixelX + x, pixelY + y);
                    
                    _templateColorMask.SetPixel(pixelX + x, pixelY + y, new Color(0, pixelColorMask.g * pixelColor.g, 0));
                }
            }
            
            _templateColorMask.Apply();
        }
        
        
        private void CreatTexture()
        {
            _templateColorMask = new Texture2D(m_ColorMaskBase.width, m_ColorMaskBase.height);
            _templateColorMask.SetPixels(m_ColorMaskBase.GetPixels());
            _templateColorMask.Apply();
            
            _rend.material.SetTexture(ColorMask, _templateColorMask);
        }
        
        
    }
    
}
