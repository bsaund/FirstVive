using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class displayMatrix : MonoBehaviour {

    List<GameObject> pixels = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        initializePixels();
        colorPixels(readNistData());
    }

    private void initializePixels()
    {
        float screenHeight = 1;
        int numRows = 32;
        int numCols = 32;
        float cellDepth = 0.05f;

        float cellHeight = screenHeight / numCols;


        for (int x = 0; x < numCols; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                GameObject p = GameObject.CreatePrimitive(PrimitiveType.Cube);
                p.transform.SetParent(this.gameObject.transform);
                p.transform.localPosition = new Vector3(x*cellHeight, -y*cellHeight, 0);
                p.transform.localScale = new Vector3(cellHeight, cellHeight, cellDepth);
               
                pixels.Add(p);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}



    void colorPixels(Matrix<double> weights)
    {
        
    }

    void colorPixels(List<double> values)
    {
        if(values.Count != pixels.Count)
        {
            Debug.Log("Pixels and values dont contain the same number of elements");
            return;
        }

        for(int i=0; i<values.Count; i++)
        {
            int c = (int)(values[i] * 255);
            pixels[i].GetComponent<Renderer>().material.color = new Color(c, c, c);
        }
    }

    List<double> readNistData()
    {
        var reader = new StreamReader(File.OpenRead("Assets/Nist/nist26_train.csv"));
        List<double> values = new List<double>();

        var line = reader.ReadLine();
        var stringValues = line.Split(',');

        foreach(string s in stringValues)
        {
            values.Add(Convert.ToDouble(s));
        }
        return values;
    }
    
}
