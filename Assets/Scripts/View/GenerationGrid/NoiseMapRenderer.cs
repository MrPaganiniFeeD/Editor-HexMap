using System;
using System.Collections.Generic;
using UnityEngine;

namespace View.GenerationGrid
{
    public class NoiseMapRenderer : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer spriteRenderer = null;
        
        [Serializable]
        public struct TerrainLevel
        {
            public string name;
            public float height;
            public Color color;
        }

        [SerializeField] public List<TerrainLevel> terrainLevel = new List<TerrainLevel>();
        
        public void RenderMap(int width, int height, float[] noiseMap, MapType type)
        {
            if (type == MapType.Noise)
            {
                ApplyColorMap(width, height, GenerateNoiseMap(noiseMap));
            }
            else if (type == MapType.Color)
            {
                ApplyColorMap(width, height, GenerateColorMap(noiseMap));
            }
        }
   
        private void ApplyColorMap(int width, int height, Color[] colors)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;
            texture.SetPixels(colors);
            texture.Apply();

            spriteRenderer.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
        }
        
        private Color[] GenerateNoiseMap(float[] noiseMap)
        {
            Color[] colorMap = new Color[noiseMap.Length];
            for (int i = 0; i < noiseMap.Length; i++)
            {
                colorMap[i] = Color.Lerp(Color.black, Color.white, noiseMap[i]);
            }

            return colorMap;
        }
        private Color[] GenerateColorMap(float[] noiseMap)
        {
            Color[] colorMap = new Color[noiseMap.Length];
            for (int i = 0; i < noiseMap.Length; i++)
            {
                colorMap[i] = terrainLevel[terrainLevel.Count - 1].color;
                foreach (var level in terrainLevel)
                {
                    if (noiseMap[i] < level.height)
                    {
                        colorMap[i] = level.color;
                        break;
                    }
                }
            }

            return colorMap;
        }
    }

    public enum MapType
    {
        Noise,
        Color
    }
}