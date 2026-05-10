using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Translates DrawIO blueprints to actual game geometry (If Set-Up Correctly)
/// </summary>
public class DrawIOTranslator : MonoBehaviour
{
    public string fileName;
    public GameObject floorBase;

    void Awake()
    {
        Destroy(gameObject);
    }

    string GetDataValueInLine(string line, string dataName)
    {
        string information = "";
        int dataStartIndex = line.IndexOf(dataName);
        if (dataStartIndex != -1)
        {
            dataStartIndex += dataName.Length + 2;
            int dataEndIndex = line.IndexOf('"', dataStartIndex);
            information = line.Substring(dataStartIndex, dataEndIndex - dataStartIndex);
        }
        return information;
    }

    [ContextMenu("Load Draw.io File")]
    public void LoadFloorMap()
    {
        string filePath = Application.streamingAssetsPath + "/" + fileName + ".drawio";
        if (!File.Exists(filePath))
        {
            Debug.LogError("ERROR! No file at: " + filePath);
            return;
        }

        string[] fileLines = File.ReadAllLines(filePath);

        for (int i = 0; i < fileLines.Length; i++)
        {
            string roomTypeValue = GetDataValueInLine(fileLines[i], "Room_Type");
            if (!roomTypeValue.Equals(""))
            {
                i += 2; // Next useful information is located after 2 lines

                float length = int.Parse(GetDataValueInLine(fileLines[i], "height"))/10.0f;
                float width = int.Parse(GetDataValueInLine(fileLines[i], "width"))/10.0f;

                float x = 0;
                float z = 0;

                string xText = GetDataValueInLine(fileLines[i], " x");
                if (!xText.Equals(""))
                {
                    x = int.Parse(xText)/10.0f;
                }
                x += width/2.0f;
                string zText = GetDataValueInLine(fileLines[i], " y");
                if (!zText.Equals(""))
                {
                    z = -int.Parse(zText)/10.0f;
                }
                z -= length/2.0f;

                Transform newFloor = Instantiate(floorBase).transform;
                newFloor.position = new Vector3(x, 0, z);
                newFloor.localScale = new Vector3(width, newFloor.localScale.y, length);
            }
        }
    }
}