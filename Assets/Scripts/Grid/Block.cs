using System.Collections.Generic;

public class Block {
    
    public BlockType blockType;

    public List<MatrixVector> blockElementPositions;

    public bool Rotatable {
        get { return blockType != BlockType.Box; }
    }
}
