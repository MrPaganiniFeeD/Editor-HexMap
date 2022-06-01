using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model.Data;
using Model.Fabrics;
using UnityEngine;

namespace Model.HexModel
{
    public class HexGrid : IModel
    {
        public event Action<Hex> AddedHex;
        public event Action<Hex> RemovedHex;
        public event Action RemovedAllHex;
        public event Action<List<Hex>> CreatedHexGrid;
        public int Width { get; }
        public int Height { get; }

        public List<HexCoordinates> Grid => _grid;
        public List<Hex> HexTiles => _hexTiles;

        private List<HexCoordinates> _grid;
        private List<Hex> _hexTiles = new List<Hex>();

        private string _savePath;
        private readonly string _saveFileName = "/GameDataSave.json";

        private IFabricHex _fabricHex; 

        public HexGrid(int height, int width)
        {
            _savePath = Application.streamingAssetsPath + _saveFileName;
            _fabricHex = new FabricHex(this);
            Width = width;
            Height = height;
            CreateGrid(Width, Height);
        }
        
        private void CreateGrid(int height, int width)
        {
            _grid = new List<HexCoordinates>(height * width);
            for (int z = 0; z < height; z++) 
            {
                for (var x = 0; x < width; x++)
                {
                    _grid.Add(new HexCoordinates(x - z /2 ,0, z));
                }
            }
        }

        public void CreateHexGrid(List<Hex> hexes)
        {
            for (int i = 0; i < _grid.Count; i++)
            {
                _hexTiles.Add(hexes[i]);
            }
            CreatedHexGrid?.Invoke(_hexTiles);
        }

        public bool TryAddHex(Hex hex)
        {
            if (_grid.Contains(hex.Position) == false) return false;
            if (_hexTiles.Contains(hex)) return false;
            
            AddHex(hex);
            return true;
        }
        public void AddHex(Hex hex)
        {
            _hexTiles.Add(hex);
            AddedHex?.Invoke(hex);
        }

        public void Save()
        {
            List<HexData> hexesData = new List<HexData>();
            foreach (Hex hex in _hexTiles)
            {
                HexData hexData = new HexData()
                {
                    Coordinates = hex.Position,
                    BiomeType = hex.BiomeType,
                    Rotation =  hex.Rotation
                };
                hexesData.Add(hexData);
            }

            var gameData = new GameData()
            {
                HexData = hexesData
            };
            var json = JsonUtility.ToJson(gameData, true);
            
            try
            {
                File.WriteAllText(_savePath, json);
            }
            catch
            {
                Debug.Log("Сохранение завалилось");
            }
        }

        public void Load()
        {
            RemoveAllHex();
            
            if (File.Exists(_savePath) == false)
            {
                Debug.Log("Файла несуществует");
                return;
            }

            var json = File.ReadAllText(_savePath);
            var gameData = JsonUtility.FromJson<GameData>(json);

            
            try
            {
                foreach (HexData hexData in gameData.HexData)
                {
                    Hex hex = _fabricHex.CreateHex(
                        hexData.Coordinates,
                        hexData.Rotation,
                        hexData.BiomeType,
                        false);
                    AddHex(hex);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void RemoveAllHex()
        {
            foreach (Hex hex in _hexTiles)
            {
                hex.Delete();
            }
            _hexTiles.Clear();
        }

        public Hex GetHex(HexCoordinates coordinates) => 
            _hexTiles.FirstOrDefault(hex => hex.Position == coordinates);
    }
}