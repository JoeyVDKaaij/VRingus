//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class StopRoomAdvance : MonoBehaviour
//{
//    [SerializeField]
//    [Tooltip("This variable controls how long the player will wait at the checkpoint before the next level starts")]
//    private float time = 1.0f;

//    private HashSet<GameObject> obstacles;
//    private float timeAux = 0.0f; //This variable keeps track of the time the checkpoint was reached;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Room"))
//        {
//            Debug.Log("Stopping the room...");
//            timeAux = Time.timeSinceLevelLoad;
//            SwitchObstacleMovingState();
//        }
//    }

//    void SwitchObstacleMovingState()
//    {
//        foreach (GameObject obj in obstacles)
//        {
//            obj.GetComponent<MovingObstacle>().CheckpointCheck();
//        }
//    }

//    public void AddRoom(GameObject room)
//    {
//        obstacles.Add(room);
//    }

//    private void Update()
//    {
//        if (Time.timeSinceLevelLoad >= time + timeAux)
//        {
//            SwitchObstacleMovingState();
//        }
//    }
//}
