using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

namespace Project.Networking
{
    public class NetworkClient : SocketIOComponent
    {
        [Header("Network Client")]
        [SerializeField]
        private Transform networkContainer;
        [SerializeField]
        private GameObject playerPrefab;

        public static string ClientID { get; private set; }

        private Dictionary<string, NetworkIdentity> serverObjects = new Dictionary<string, NetworkIdentity>();
        public override void Start()
        {
            base.Start();
            SetupEvents();
        }
        public override void Update()
        {
            base.Update();
        }
        private void SetupEvents()
        {
            On("open", e =>
            {
                Debug.Log("Connection made to the server");
            });
            On("register", e =>
            {
                ClientID = e.data["id"].ToString();
                Debug.LogFormat($"Our Client's ID {ClientID}");
            });
            On("spawn", e =>
            {
                string id = e.data["id"].ToString();

                GameObject go = Instantiate(playerPrefab, networkContainer);
                go.name = $"Player ({id})";
                NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
                ni.SetControllerID(id);
                ni.SetSocketReference(this); 
                serverObjects.Add(id, ni);
            });
            On("disconnected", e =>
            {
                string id = e.data["id"].ToString();
                GameObject go = serverObjects[id].gameObject;
                Destroy(go);
                serverObjects.Remove(id);
            });
            On("updatePosition", e =>
            {
                string id = e.data["id"].ToString();
                float x = e.data["position"]["x"].f;
                float y = e.data["position"]["y"].f;

                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(x, y, 0);
            });
        }
    }
    [Serializable]
    public class Player
    {
        public string ID;
        public Position position;
    }
    [Serializable]
    public class Position
    {
        public float x;
        public float y;
    }
}