
public struct MatrixVector {

	public static readonly MatrixVector down = new MatrixVector(0, 1);
	public static readonly MatrixVector up = new MatrixVector(0, -1);
	public static readonly MatrixVector left = new MatrixVector(-1, 0);
	public static readonly MatrixVector right = new MatrixVector(1, 0);
	public static readonly MatrixVector zero = new MatrixVector(0, 0);
	
	public readonly int x;
	public readonly int y;
	
	public MatrixVector(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static MatrixVector Sum(MatrixVector a, MatrixVector b) {
		return new MatrixVector(a.x + b.x, a.y + b.y);
	}
	
	public static MatrixVector Substract(MatrixVector a, MatrixVector b) {
		return new MatrixVector(a.x - b.x, a.y - b.y);
	}

	public static MatrixVector RotateHalfPi(MatrixVector a) {

		int x = 0 * a.x + -1 * a.y;
		int y = 1 * a.x + 0 * a.y;

		return new MatrixVector(x, y);
	}
	
	public override string ToString() {
		return string.Format("({0:F1}, {1:F1})", x, y);
	}
}
