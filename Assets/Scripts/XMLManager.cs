using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerInformation
{
    public int playerNumber;
    public string animalName;
    public string cardSign;
}

[Serializable]
public class TournamentDatabase
{
    [XmlArray("TournamentDatabase")]
    public List<PlayerInformation> informations;
}


public class XMLManager : MonoBehaviour
{
    public TournamentDatabase tournamantDB;

    #region Singlton
    public static XMLManager xMLManager;
    private void Awake()
    {
        xMLManager = this;
    }
    #endregion

    public void Save()
    {
        //create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(TournamentDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/TournamentInformation.xml", FileMode.Create);

        //serialize data to xml file
        serializer.Serialize(stream, tournamantDB);

        //close stream
        stream.Close();
    }

    public void Load()
    {
        //open xml file
        XmlSerializer serializer = new XmlSerializer(typeof(TournamentDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/TournamentInformation.xml", FileMode.Open);

        //deserialize data from xml file to ShapeDataBase object
        tournamantDB = serializer.Deserialize(stream) as TournamentDatabase;

        //close stream
        stream.Close();
    }

}
