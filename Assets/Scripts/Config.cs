using System.Collections.Generic;
using UnityEngine;

public class Config {
    public static readonly Dictionary<BlockType, List<Vector2Int>> BlockElementOffsets =
        new Dictionary<BlockType, List<Vector2Int>> {
            {
                BlockType.I, new List<Vector2Int> {
                    new Vector2Int(-1, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(2, 0)
                }
            }, {
                BlockType.T, new List<Vector2Int> {
                    new Vector2Int(-1, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, -1)
                }
            }, {
                BlockType.S, new List<Vector2Int> {
                    new Vector2Int(-1, 0),
                    new Vector2Int(0, -1),
                    new Vector2Int(1, -1)
                }
            }, {
                BlockType.Z, new List<Vector2Int> {
                    new Vector2Int(1, 0),
                    new Vector2Int(0, -1),
                    new Vector2Int(-1, -1)
                }
            }, {
                BlockType.R, new List<Vector2Int> {
                    new Vector2Int(1, 0),
                    new Vector2Int(2, 0),
                    new Vector2Int(0, -1)
                }
            }, {
                BlockType.RR, new List<Vector2Int> {
                    new Vector2Int(-1, 0),
                    new Vector2Int(-2, 0),
                    new Vector2Int(0, 1)
                }
            }, {
                BlockType.Box, new List<Vector2Int> {
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(1, 1)
                }
            }
        };
}