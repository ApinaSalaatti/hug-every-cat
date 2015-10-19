using UnityEngine;
using System.Collections;

public enum FurRandomizationMethod { RANDOM_COLOR, CELLULAR_AUTOMATA, PERLIN_NOISE }

public class RandomFur : MonoBehaviour {
	public static Color Color1 { get; set; }
	public static Color Color2 { get; set; }

	public static bool UseRandomColors { get; set; }

	public static Color[,] PerlinNoise(int w, int h) {
		throw new System.Exception("NOT IMPLEMENTED YET!!");
	}

	/*
	 * Creating a random fur with cellular automata. Will create a fur with some spots sort of
	 */
	public static Color[,] CellularAutomata(int w, int h) {
		float chanceToStartAlive = 0.4f;
		int deathLimit = 4;
		int birthLimit = 5;
		int numberOfSteps = 35;
		
		bool[,] pixels = new bool[h, w];
		
		// Initialize pixels randomly
		for(int y = 0; y < h; y++) {
			for(int x = 0; x < w; x++) {
				float r = Random.value;
				if(r > chanceToStartAlive)
					pixels[y, x] = false;
				else
					pixels[y, x] = true;
			}
		}
		
		// Lets dance
		for(int i = 0; i < numberOfSteps; i++) {
			bool[,] newPixels = new bool[h, w];
			for(int y = 0; y < h; y++) {
				for(int x = 0; x < w; x++) {
					int n = CountAliveNeighbours(pixels, x, y);
					if(pixels[y, x]) {
						// Check if we should die
						if(n < deathLimit)
							newPixels[y, x] = false;
						else
							newPixels[y, x] = true;
					}
					else {
						// Check if we should be born
						if(n > birthLimit)
							newPixels[y, x] = true;
						else
							newPixels[y, x] = false;
					}
				}
			}
			
			pixels = newPixels;
		}
		
		// Set the colors
		Color[,] ret = new Color[h, w];
		Color alive;
		Color dead;
		if(UseRandomColors) {
			alive = new Color(Random.value, Random.value, Random.value, 1f);
			dead = new Color(Random.value, Random.value, Random.value, 1f);
		}
		else {
			alive = Color1;
			dead = Color2;
		}
		
		// Initialize pixels randomly
		for(int y = 0; y < h; y++) {
			for(int x = 0; x < w; x++) {
				if(pixels[y, x])
					ret[y, x] = alive;
				else
					ret[y, x] = dead;
			}
		}
		
		return ret;
	}

	private static int CountAliveNeighbours(bool[,] pixels, int x, int y) {
		int w = pixels.GetLength(1);
		int h = pixels.GetLength(0);
		int alive = 0;
		for(int dY = -1; dY < 2; dY++) {
			for(int dX = -1; dX < 2; dX++) {
				int nX = x + dX;
				int nY = y + dY;
				if(dX != 0 || dY != 0) {
					if(nX < 0 || nX >= w || nY < 0 || nY >= h)
						alive++;
					else if(pixels[y, x])
						alive++;
				}
			}
		}
		return alive;
	}
}
