using System.Collections.Generic;
using Model.Fabrics;
using Model.HexModel;
using Model.HexModel.HexInfos;
using Model.Rotation;
using TMPro;
using UnityEngine;
using View.GenerationGrid;

namespace Model.GenerationHexGrid
{
    public class PerlinNoiseHexGridGenerator : IGeneratorMap
    {
        private float _scale = 10;

        private int _octaves = 6;
        private float _persistence = 0.5f;
        private float _lacunarity = 2;

        private int _seed = 1;
        private Vector2 _offset;
        
        private HexGrid _hexGrid;
        private FabricHex _fabricHex;
        private List<IHexInfo> _hexInfos; 
        
        
        public PerlinNoiseHexGridGenerator(HexGrid hexGrid)
        {
            _hexGrid = hexGrid;
            _fabricHex = new FabricHex(_hexGrid);
            _hexInfos = new List<IHexInfo>()
            {
                new HexSeaInfo(TerrainHexType.None),
                new HexShallowSeaInfo(TerrainHexType.None),
                new HexSandInfo(TerrainHexType.None),
                new HexForestInfo(TerrainHexType.None),
                new HexSnowInfo(TerrainHexType.None)
            };
        }

        public void GeneratorMap()
        {
            var noiseGenerator = new NoiseMapGenerator();
            var width = _hexGrid.Width;
            var height = _hexGrid.Height;
            var noiseMap = noiseGenerator.GenerateNoiseMap(
                width, height, _seed, _scale ,_octaves,_persistence , _lacunarity, _offset);
            
            for (int i = 0; i < _hexGrid.Grid.Capacity; i++)
            {
                foreach (var hexInfo in _hexInfos)
                {
                    if (noiseMap[i] < hexInfo.HeightSpawn)
                    {
                        _fabricHex.CreateHex(_hexGrid.Grid[i], new RotationLocal(), hexInfo.BiomeType, true);
                        /*if(TryCheckChanceSpawn(hexInfo.ChanceSpawn))
                            hexes.Add(hex);*/
                        break;
                    } 
                    if(noiseMap[i] == 1)
                    {
                    }
                }
            } 
        }

        private bool TryCheckChanceSpawn(float chanceSpawn)
        {
            var randomValue = Random.Range(0f, 1f);
            return chanceSpawn >= randomValue;
        }
    }
}