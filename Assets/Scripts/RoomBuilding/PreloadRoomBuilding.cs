using UnityEngine;
using UnityEngine.SceneManagement;
 
public class PhysicsSceneLoader : MonoBehaviour
{
    public float physicsSceneTimeScale = 1;
    private PhysicsScene physicsScene;
 
    private void Start()
    {
        //load the scene to place in a local physics scene.
        LoadSceneParameters param = new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics3D);
        Scene scene = SceneManager.LoadScene("RoomBuilding", param);
        //Get the scene's physics scene.
        physicsScene = scene.GetPhysicsScene();
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