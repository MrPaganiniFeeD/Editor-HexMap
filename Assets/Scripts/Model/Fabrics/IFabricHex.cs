using Model.HexModel;
using Model.Rotation;
using UnityEngine;

namespace Model.Fabrics
{
    public interface IFabricHex
    {
        Hex CreateHex(HexCoordinates coordinates, RotationLocal rotation, HexBiomeType hexBiomeType, bool isAddToGrid);
        Hex CreateHex(IHexInfo hexInfo);
    }
}