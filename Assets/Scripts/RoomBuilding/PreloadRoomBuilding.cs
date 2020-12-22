using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;
 
public class PhysicsSceneLoader : MonoBehaviour
{
    public float physicsSceneTimeScale = 1;
    private PhysicsScene physicsScene;
    public static string getSceneName(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars);
    }

    
    private void Start()
    {
        string sceneName = getSceneName(5);
        Scene newScene = SceneManager.CreateScene(sceneName);
        SceneManager.MergeScenes(SceneManager.GetSceneByName("RoomBuilding"), newScene);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        
    }
     
    private void FixedUpdate()
    {
        //Simulate the scene on FixedUpdate.
        if (physicsScene != null)
        {
            physicsScene.Simulate(Time.fixedDeltaTime * physicsSceneTimeScale);
        }
    }
}