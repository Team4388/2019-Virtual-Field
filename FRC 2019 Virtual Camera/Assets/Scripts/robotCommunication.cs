using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class robotCommunication : MonoBehaviour
{
    public SortedList<string, float> robotValues =
            new SortedList<string, float>
            {
                { "Yaw Angle Deg", 0.0f },
                { "Distance", 0.0f },
                { "Team", 0.0f },
            };
    public static int portNumber = 4388;
    public int bytesAvailable;
    List<string> keys = new List<string>();
    public float encoder, navX;
    public string input, request = "";
    IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);
    Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    Socket clientSocket;
    short currentIndex;
    float currentValue;
    public float latency;

    // Start is called before the first frame update
    void Start()
    {
        startRobopipe();
        serverSocket.Bind(serverAddress);
        serverSocket.Listen(10);

        clientSocket = serverSocket.Accept();
        foreach (var valuePair in robotValues)
        {
            request += valuePair.Key;
            keys.Add(valuePair.Key);
            request += ",";
        }
        request = request.Substring(0,request.Length-1);
        byte[] byteRequest = Encoding.UTF8.GetBytes(request);
        clientSocket.Send(byteRequest);
    }

    // Update is called once per frame
    void Update()
    {
        latency = Time.deltaTime * 1000;
        bytesAvailable = clientSocket.Available;
        if (bytesAvailable > 10)
        {
            byte[] byteInput = new byte[100];
            clientSocket.Receive(byteInput);
            input = Encoding.UTF8.GetString(byteInput);
            string[] values = input.Split('|');
            int i = 0;
            foreach (string key in keys)
            {
                robotValues[keys[i]] = float.Parse(values[i], CultureInfo.InvariantCulture.NumberFormat);
                i++;
            }
            clientSocket.Send(Encoding.UTF8.GetBytes("CONT"));
        }
    }

    private void OnApplicationQuit()
    {
        clientSocket.Send(Encoding.UTF8.GetBytes("EXIT"));
    }

    private void startRobopipe()
    {
        Process foo = new Process();
        foo.StartInfo.FileName = "Robopipe.jar";
        foo.StartInfo.Arguments = "" + portNumber;
        foo.Start();
    }
}