using System.Collections.Generic;
using UnityEngine;

namespace Shaders
{
    public class VertexPaintController : MonoBehaviour
    {
        [SerializeField] private Texture2D m_ColorMaskBase;
        [SerializeField] private List<Texture2D> m_HitTexture;

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

        public void Paint(RaycastHit hit)
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;

            if (_rend == null || _rend.sharedMaterial == null || meshCollider == null) return;

            Vector2 textureCoordinate = hit.textureCoord;

            int pixelX = (int)(textureCoordinate.x * _templateColorMask.width);
            int pixelY = (int)(textureCoordinate.y * _templateColorMask.height);

            int hitTextureRandomInxed = Random.Range(0, m_HitTexture.Count);
            Texture2D hitTexture = m_HitTexture[hitTextureRandomInxed];
            
            for (int x = 0; x < hitTexture.width; x++)
            {
                for (int y = 0; y < hitTexture.height; y++)
                {
                    Color pixelColor = hitTexture.GetPixel(x, y);
                    Color pixelColorMask = _templateColorMask.GetPixel(pixelX + x, pixelY + y);

                    _templateColorMask.SetPixel(pixelX + x, pixelY + y,
                        new Color(0, pixelColorMask.g * pixelColor.g, 0));
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