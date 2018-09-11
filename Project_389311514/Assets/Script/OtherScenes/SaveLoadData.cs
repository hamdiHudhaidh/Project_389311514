using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadData
{
    public static void savePlayer(Info iS)
    {
        BinaryFormatter bf = new BinaryFormatter();//

        //seves a new file somewhere
        FileStream stream = new FileStream(Application.persistentDataPath + "/Player.mine", FileMode.Create);

        playerData data = new playerData(iS);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadPlayerName()
    {
        if (File.Exists(Application.persistentDataPath + "/Player.mine"))
        {
            BinaryFormatter bf = new BinaryFormatter();//

            //seves a new file somewhere
            FileStream stream = new FileStream(Application.persistentDataPath + "/Player.mine", FileMode.Open);

            playerData data = bf.Deserialize(stream) as playerData;

            stream.Close();
            return data.name;
        }
        else
        {
            Debug.Log("No File Found");
            return null;
        }
    }

    public static bool LoadPlayerBool()
    {
        if (File.Exists(Application.persistentDataPath + "/Player.mine"))
        {
            BinaryFormatter bf = new BinaryFormatter();//

            //seves a new file somewhere
            FileStream stream = new FileStream(Application.persistentDataPath + "/Player.mine", FileMode.Open);

            playerData data = bf.Deserialize(stream) as playerData;

            stream.Close();
            return data.state;
        }
        else
        {
            Debug.Log("No File Found");
            return false;
        }
    }

    public static float LoadPlayerTime()
    {
        if (File.Exists(Application.persistentDataPath + "/Player.mine"))
        {
            BinaryFormatter bf = new BinaryFormatter();//

            //seves a new file somewhere
            FileStream stream = new FileStream(Application.persistentDataPath + "/Player.mine", FileMode.Open);

            playerData data = bf.Deserialize(stream) as playerData;

            stream.Close();
            return data.time;
        }
        else
        {
            Debug.Log("No File Found");
            return 0;
        }
    }

    public static int LoadPlayerRound()
    {
        if (File.Exists(Application.persistentDataPath + "/Player.mine"))
        {
            BinaryFormatter bf = new BinaryFormatter();//

            //seves a new file somewhere
            FileStream stream = new FileStream(Application.persistentDataPath + "/Player.mine", FileMode.Open);

            playerData data = bf.Deserialize(stream) as playerData;

            stream.Close();
            return data.rounds;
        }
        else
        {
            Debug.Log("No File Found");
            return 0;
        }
    }
}

[Serializable]
public class playerData
{
    public int rounds;
    public float time;
    public bool state;
    public string name;

    public playerData(Info iS)
    {
        rounds = iS.roundsSurvived;
        time = iS.secondsInLastRound;
        state = iS.won;
        name = iS.playerName;
    }
}
