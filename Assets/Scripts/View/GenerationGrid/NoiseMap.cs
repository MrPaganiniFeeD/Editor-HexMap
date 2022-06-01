using Model.GenerationHexGrid;
using Unity.Mathematics;
using UnityEngine;

namespace View.GenerationGrid
{
    public class NoiseMap : MonoBehaviour
    {
        [SerializeField] public int width;
        [SerializeField] public int height;
        [SerializeField] public float scale;

        [SerializeField] public int octaves;
        [SerializeField] public float persistence;
        [SerializeField] public float lacunarity;

        [SerializeField] public int seed;
        [SerializeField] public Vector2 offset;

        [SerializeField] public MapType type = MapType.Noise;

        [SerializeField] private NoiseMapRenderer _mapRenderer;

        private void Start() => 
            GenerateMap();

        public void GenerateMap()
        {
            NoiseMapGenerator noiseGenerator = new NoiseMapGenerator();
            float[] noiseMap = noiseGenerator.GenerateNoiseMap(width, height, seed, scale, octaves, persistence, lacunarity, offset);
            _mapRenderer.RenderMap(width, height, noiseMap, type);
        }
    }
    
}