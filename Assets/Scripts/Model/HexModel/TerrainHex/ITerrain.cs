using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.HexModel.TerrainHex
{
    public interface ITerrain
    {
        public event Action Refreshed;
        
        List<HexBiomeType> BiomesOnWhichYouCanPlace { get; }

        void Delete();
        void Refresh();
    }
}