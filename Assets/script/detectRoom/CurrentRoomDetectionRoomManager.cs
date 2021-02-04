using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using UnityEngine;

namespace Script.detectRoom
{
    public class CurrentRoomDetectionRoomManager : MonoBehaviour
    {
        /// <summary>
        /// Room instance of the corresponding room.
        /// </summary>
        public RoomInstance RoomInstance;

        /// <summary>
        /// Gets called when a player enters the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomEnter(GameObject player)
        {
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.GetDisplayName()}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
            CurrentRoomDetectionGameManager.Instance.OnRoomEnter(RoomInstance);

            var ememiesHolder = RoomInstance.RoomTemplateInstance.transform.Find("Enemy");

            foreach(Transform enemyTransform in ememiesHolder)
            {
                var enemy = enemyTransform.gameObject;

                enemy.SetActive(true);
            }

           if(RoomInstance.Room.GetDisplayName().Equals("BossRoom"))
            {
                player.GetComponent<PlayerMovement>().changeBGM();
            } 


        }

        /// <summary>
        /// Gets called when a player leaves the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomLeave(GameObject player)
        {
            Debug.Log($"Room leave {RoomInstance.Room.GetDisplayName()}");
            CurrentRoomDetectionGameManager.Instance.OnRoomLeave(RoomInstance);
        }
    }
}