using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Translates DrawIO blueprints to actual game geometry (If Set-Up Correctly)
/// </summary>
public class DrawIOTranslator : MonoBehaviour
{
    public string fileName;
    public MapRandomSector[] availableRandomSectors;

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
            dataStartIndex += dataName.Length;
            // We move fowards until we find something that is not an = or a "
            while (line[dataStartIndex].Equals('=') || line[dataStartIndex].Equals('"'))
            {
                dataStartIndex++;
            }
            int dataEndIndex = dataStartIndex;
            // We move fowards until we find a " or a ;
            while (!line[dataEndIndex].Equals('"') && !line[dataEndIndex].Equals(';'))
            {
                dataEndIndex++;
            }
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

        Dictionary<string, MapRandomSector> idSectorPairs = new Dictionary<string, MapRandomSector>();
        Dictionary<string, List<Vector2>> idDoorPointsPairs = new Dictionary<string, List<Vector2>>();

        for (int i = 0; i < fileLines.Length; i++)
        {
            string sectorTypeValue = GetDataValueInLine(fileLines[i], "Sector_Type");
            if (!sectorTypeValue.Equals(""))
            {
                // ID should be in the same line as sector
                string sectorTypeIDValue = GetDataValueInLine(fileLines[i], " id");
                bool foundAttributes = false;
                // We find attempt to find the attributes of
                while (!foundAttributes && i < fileLines.Length)
                {
                    string lengthValue = GetDataValueInLine(fileLines[i], "height");
                    // Length and all other values are on the same line
                    if (!lengthValue.Equals(""))
                    {
                        foundAttributes = true;
                        float length = int.Parse(lengthValue) / 10.0f;
                        float width = int.Parse(GetDataValueInLine(fileLines[i], "width")) / 10.0f;

                        Vector2 dimensions = new Vector2(width, length);
                        MapRandomSector matchingRandomSector = null;

                        foreach (MapRandomSector randomSector in availableRandomSectors)
                        {
                            if (randomSector.Dimensions == dimensions)
                            {
                                matchingRandomSector = Instantiate(randomSector);
                            }
                        }

                        if (matchingRandomSector == null)
                        {
                            Debug.LogWarning($"A matching Random Sector of type \"{sectorTypeValue}\" and of dimensions {dimensions} was not found. It will not be created.");
                            break;
                        }

                        float x = 0;
                        float z = 0;

                        string xText = GetDataValueInLine(fileLines[i], " x");
                        if (!xText.Equals(""))
                        {
                            x = int.Parse(xText) / 10.0f;
                        }
                        string zText = GetDataValueInLine(fileLines[i], " y");
                        if (!zText.Equals(""))
                        {
                            z = -int.Parse(zText) / 10.0f;
                        }

                        Debug.Log($"Will add Sector of ID {sectorTypeIDValue} to Dictionary");
                        matchingRandomSector.transform.position = new Vector3(x, 0, z);
                        idSectorPairs.Add(sectorTypeIDValue, matchingRandomSector);
                    }
                    i++;
                }
                if (!foundAttributes)
                {
                    Debug.LogError($"The attributes for a Sector of type \"{sectorTypeValue}\" was not found. Terminating Translation...");
                }
            }
            else
            {
                // For Doors
                string sourceIDValue = GetDataValueInLine(fileLines[i], " source");
                if (!sourceIDValue.Equals(""))
                {
                    string dXValue = GetDataValueInLine(fileLines[i], "exitX");
                    if (dXValue.Equals(""))
                    {
                        Debug.LogWarning("FIX BANDAID FIX");
                        continue;
                    }
                    float dX = float.Parse(dXValue);
                    float dY = float.Parse(GetDataValueInLine(fileLines[i], "exitY"));
                    List<Vector2> sourceDoorPoints;
                    if (!idDoorPointsPairs.TryGetValue(sourceIDValue, out sourceDoorPoints))
                    {
                        sourceDoorPoints = new List<Vector2>();
                        idDoorPointsPairs.Add(sourceIDValue, sourceDoorPoints);
                    }
                    Debug.Log($"Will add {dX},{dY} to Sector of ID {sourceIDValue}");
                    sourceDoorPoints.Add(new Vector2(dX, dY));
                }
            }
        }
        // DOOR CONNECTION
        foreach (KeyValuePair<string, MapRandomSector> idSectorPair in idSectorPairs)
        {
            Debug.Log("HII: " + idSectorPair.Key);
            List<Vector2> sectorDoorPoints;
            if (idDoorPointsPairs.TryGetValue(idSectorPair.Key, out sectorDoorPoints))
            {
                foreach (Vector2 point in sectorDoorPoints)
                {
                    Debug.Log($"Got point {point} for Sector of ID: {idSectorPair.Key}");
                }
                idSectorPair.Value.SetActiveDoorPoints(sectorDoorPoints);
            }
            else
            {
                Debug.LogWarning($"Sector {idSectorPair.Value.transform.name} does not have any room connections. Ignore if intended.");
            }
        }
        Debug.Log("Translation Successful!");
    }
}