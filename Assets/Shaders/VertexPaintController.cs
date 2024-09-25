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
            Texture2D hitTexture = GetRandomHitTextureFromTheList();

            int pixelX = (int)(textureCoordinate.x * _templateColorMask.width);
            int pixelY = (int)(textureCoordinate.y * _templateColorMask.height);
            Vector2Int paintPixelPosition = new Vector2Int(pixelX, pixelY);

            int pixelXOffset = pixelX - (hitTexture.width / 2);
            int pixelYOffset = pixelY - (hitTexture.height / 2);
            
            for (int x = 0; x < hitTexture.width; x++)
            {
                for (int y = 0; y < hitTexture.height; y++)
                {
                    Color pixelColor = hitTexture.GetPixel(x, y);
                    Color pixelColorMask = _templateColorMask.GetPixel(pixelXOffset + x, pixelYOffset + y);

                    _templateColorMask.SetPixel(pixelXOffset + x, pixelYOffset + y,
                        new Color(0, pixelColorMask.g * pixelColor.g, 0));
                }
            }

            _templateColorMask.Apply();
        }

        private Texture2D GetRandomHitTextureFromTheList()
        {
            int hitTextureRandomInxed = Random.Range(0, m_HitTexture.Count);
            return m_HitTexture[hitTextureRandomInxed];
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