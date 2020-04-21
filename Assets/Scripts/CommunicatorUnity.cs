using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using NetMQ;
using NetMQ.Sockets;
using AsyncIO;
public class CommunicatorUnity : MonoBehaviour
{
    // Start is called before the first frame update
    public RequestSocket client;
    public string message;
    public bool gotMessage;
    private int count;
    void Start()
    {
        Process foo = new Process();
        foo.StartInfo.FileName = "E:\\PythonUnityCommunication\\Assets\\Scripts\\CommunicatorPython.py";
        foo.StartInfo.Arguments = "";
        foo.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        foo.Start();
        ForceDotNet.Force();
        client = new RequestSocket();
        client.Connect("tcp://localhost:55");
        client.SendFrame("Hello");
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 10)
        {
            gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
            if (gotMessage)
            {
                UnityEngine.Debug.Log("Received: " + message);
                client.SendFrame("Hello");
                count = count + 1;
            }
        }
    }

    private void OnDestroy()
    {
        NetMQConfig.Cleanup();
    }
}
