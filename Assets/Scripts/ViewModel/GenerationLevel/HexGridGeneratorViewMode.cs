using System.Collections.Generic;
using Model.GenerationHexGrid;
using Model.HexModel;
using UnityEngine;

namespace ViewModel.GenerationLevel
{
    public class HexGridGeneratorViewMode
    {
        private PerlinNoiseHexGridGenerator _perlinNoiseHexGridGenerator;
        
        public HexGridGeneratorViewMode(PerlinNoiseHexGridGenerator hexGridGenerator)
        {
            _perlinNoiseHexGridGenerator = hexGridGenerator;
        }

        private void OnUpdateableHexGrid(Dictionary<Vector3Int, Hex> hexGrid)
        {
            throw new System.NotImplementedException();
        }
    }
}