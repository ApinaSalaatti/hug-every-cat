using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum DrawTool { PEN, BUCKET, COLOR_PICKER, RANDOM_FUR }

// ====================================
// Helper classes used with the tools
// ====================================
public class Point {
	public int x;
	public int y;
	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}
}
public class AffectedPixel {
	public int x;
	public int y;
	public Color oldColor;
	public AffectedPixel(int x, int y, Color oc) {
		this.x = x;
		this.y = y;
		this.oldColor = oc;
	}
}
// =======================
// End of helper classes
// =======================

public abstract class CatCreatorTool {
	public DrawTool toolType;
	protected CatCreator creator;
	public CatCreatorTool(CatCreator cc, DrawTool type) {
		creator = cc;
		toolType = type;
	}
	
	public abstract void Execute();
	public abstract void UndoExecute();
	public abstract void RedoExecute();
}

public class PenTool : CatCreatorTool {
	private Color newColor;
	private Sprite catSprite;
	private List<AffectedPixel> affectedPixels = new List<AffectedPixel>();

	private bool[,] alreadyAffected;
	
	public PenTool(CatCreator cc, Color newCol) : base(cc, DrawTool.PEN) {
		newColor = newCol;
		catSprite = cc.CatSprite;
		alreadyAffected = new bool[catSprite.texture.width, catSprite.texture.height];
	}
	
	public override void Execute ()
	{
		int pixelX = creator.GetMousePixelX();
		int pixelY = creator.GetMousePixelY();
		Color currentCol = creator.GetColorAt(pixelX, pixelY);
		
		if(creator.IsLegal(pixelX, pixelY) && !alreadyAffected[pixelX, pixelY]) {
			alreadyAffected[pixelX, pixelY] = true;
			creator.SetColorAt(pixelX, pixelY, newColor);
			affectedPixels.Add(new AffectedPixel(pixelX, pixelY, currentCol));
		}
	}
	public override void UndoExecute ()
	{
		foreach(AffectedPixel p in affectedPixels) {
			catSprite.texture.SetPixel(p.x, p.y, p.oldColor);
		}
		catSprite.texture.Apply();
	}
	public override void RedoExecute ()
	{
		foreach(AffectedPixel p in affectedPixels) {
			catSprite.texture.SetPixel(p.x, p.y, newColor);
		}
		catSprite.texture.Apply();
	}
}

public class BucketTool : CatCreatorTool {
	private Sprite catSprite;
	private Color newColor;
	private List<AffectedPixel> affectedPixels = new List<AffectedPixel>();

	public BucketTool(CatCreator cc, Color col) : base(cc, DrawTool.BUCKET) {
		catSprite = cc.CatSprite;
		newColor = col;
	}

	public override void Execute ()
	{
		int pixelX = creator.GetMousePixelX();
		int pixelY = creator.GetMousePixelY();
		if(creator.IsLegal(pixelX, pixelY))
			FillArea(pixelX, pixelY, newColor);
	}
	public override void UndoExecute ()
	{
		foreach(AffectedPixel p in affectedPixels) {
			catSprite.texture.SetPixel(p.x, p.y, p.oldColor);
		}
		catSprite.texture.Apply();
	}
	public override void RedoExecute ()
	{
		foreach(AffectedPixel p in affectedPixels) {
			catSprite.texture.SetPixel(p.x, p.y, newColor);
		}
		catSprite.texture.Apply();
	}

	private void FillArea(int startX, int startY, Color newCol) {
		Color oldCol = creator.GetColorAt(startX, startY);
		if(creator.SameColor(oldCol, newCol)) {
			// Early out, nothing to do here
			return;
		}
		
		List<Point> toFill = new List<Point>();
		List<Point> queue = new List<Point>();
		
		bool[,] done = new bool[catSprite.texture.width, catSprite.texture.height];
		
		queue.Add(new Point(startX, startY));
		done[startX, startY] = true;
		
		while(queue.Count > 0) {
			Point current = queue[0];
			queue.RemoveAt(0);

			if(creator.IsLegal(current.x, current.y)) toFill.Add(current);

			Point n = new Point(current.x+1, current.y);
			//Debug.Log("IS DONE: " + n.x + "," + n.y + ": " + done[n.x, n.y]);
			if(n.x < done.GetLength(0) && !done[n.x, n.y] && creator.SameColor(creator.GetColorAt(n.x, n.y), oldCol)) {
				done[n.x, n.y] = true;
				queue.Add(n);
			}
			n = new Point(current.x-1, current.y);
			//Debug.Log("IS DONE: " + n.x + "," + n.y + ": " + done[n.x, n.y]);
			if(n.x >= 0 && !done[n.x, n.y] && creator.SameColor(creator.GetColorAt(n.x, n.y), oldCol)) {
				done[n.x, n.y] = true;
				queue.Add(n);
			}
			n = new Point(current.x, current.y+1);
			//Debug.Log("IS DONE: " + n.x + "," + n.y + ": " + done[n.x, n.y]);
			if(n.y < done.GetLength(1) && !done[n.x, n.y] && creator.SameColor(creator.GetColorAt(n.x, n.y), oldCol)) {
				done[n.x, n.y] = true;
				queue.Add(n);
			}
			n = new Point(current.x, current.y-1);
			//Debug.Log("IS DONE: " + n.x + "," + n.y + ": " + done[n.x, n.y]);
			if(n.y >= 0 && !done[n.x, n.y] && creator.SameColor(creator.GetColorAt(n.x, n.y), oldCol)) {
				done[n.x, n.y] = true;
				queue.Add(n);
			}
		}
		
		foreach(Point p in toFill) {
			if(creator.IsLegal(p.x, p.y)) {
				catSprite.texture.SetPixel(p.x, p.y, newCol);
				affectedPixels.Add(new AffectedPixel(p.x, p.y, oldCol));
			}
		}
		
		catSprite.texture.Apply();
	}
}

public class RandomFurTool : CatCreatorTool {
	private Color[,] oldColors;
	private Color[,] newColors;

	private Sprite catSprite;
	private Color color1;
	private Color color2;
	private bool useRandom;
	private FurRandomizationMethod method;

	public RandomFurTool(CatCreator cc, FurRandomizationMethod randomizationMethod, bool useRandomCols, Color col1, Color col2) : base(cc, DrawTool.RANDOM_FUR) {
		catSprite = cc.CatSprite;
		method = randomizationMethod;
		color1 = col1;
		color2 = col2;
		useRandom = useRandomCols;
	}

	public override void Execute ()
	{
		RandomizeFur();
	}

	private void RandomizeFur() {
		oldColors = new Color[catSprite.texture.height, catSprite.texture.width];
		newColors = new Color[catSprite.texture.height, catSprite.texture.width];

		// Set old colors
		for(int y = 0; y < oldColors.GetLength(0); y++) {
			for(int x = 0; x < oldColors.GetLength(1); x++) {
				Color c = catSprite.texture.GetPixel(x, y);
				oldColors[y, x] = new Color(c.r, c.g, c.b, c.a);
			}
		}

		Color[,] cols = null; 

		RandomFur.Color1 = color1;
		RandomFur.Color2 = color2;
		RandomFur.UseRandomColors = useRandom;

		switch(method) {
		case FurRandomizationMethod.CELLULAR_AUTOMATA:
			cols = RandomFur.CellularAutomata(catSprite.texture.width, catSprite.texture.height);
			break;
		case FurRandomizationMethod.PERLIN_NOISE:
			cols = RandomFur.PerlinNoise(catSprite.texture.width, catSprite.texture.height);
			break;
		}

		for(int y = 0; y < newColors.GetLength(0); y++) {
			for(int x = 0; x < newColors.GetLength(1); x++) {
				if(creator.IsLegal(x, y)) {
					catSprite.texture.SetPixel(x, y, cols[y, x]);
					newColors[y, x] = cols[y, x];
				}
				else {
					Color c = catSprite.texture.GetPixel(x, y);
					newColors[y, x] = new Color(c.r, c.g, c.b, c.a);
				}
			}
		}
		catSprite.texture.Apply();
	}
	
	public override void UndoExecute ()
	{
		for(int y = 0; y < oldColors.GetLength(0); y++) {
			for(int x = 0; x < oldColors.GetLength(1); x++) {
				catSprite.texture.SetPixel(x, y, oldColors[y, x]);
			}
		}
		catSprite.texture.Apply();
	}

	public override void RedoExecute() {
		for(int y = 0; y < newColors.GetLength(0); y++) {
			for(int x = 0; x < newColors.GetLength(1); x++) {
				catSprite.texture.SetPixel(x, y, newColors[y, x]);
			}
		}
		catSprite.texture.Apply();
	}
}

public class ColorPickerTool : CatCreatorTool {
	
	public ColorPickerTool(CatCreator cc) : base(cc, DrawTool.COLOR_PICKER) {
		
	}
	
	public override void Execute ()
	{
		int pixelX = creator.GetMousePixelX();
		int pixelY = creator.GetMousePixelY();
		if(creator.IsLegal(pixelX, pixelY)) {
			Color c = creator.GetColorAt(pixelX, pixelY);
			creator.SetColor(c.r, c.g, c.b);
		}
	}
	public override void UndoExecute ()
	{
		
	}
	public override void RedoExecute ()
	{
		
	}
}