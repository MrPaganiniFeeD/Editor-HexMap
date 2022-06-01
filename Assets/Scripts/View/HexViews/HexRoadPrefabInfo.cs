using System.Collections.Generic;
using Model.HexModel;
using UnityEngine;

namespace View.HexViews
{
    public class HexRoadPrefabInfo : MonoBehaviour
    {
        [SerializeField] public List<HexDirection> NeighbourDireciton;
        [SerializeField] public HexBiomeType HexBiomeType;
        public int NumberOfRoad => NeighbourDireciton.Count;
    }
}