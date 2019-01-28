using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class robotCommunication : MonoBehaviour
{
    public bool isData = false;
    public static int portNumber = 4388;
    public int bytesAvailable;
    List<string> keys = new List<string>();
    public SortedList<string, float> robotValues =
            new SortedList<string, float>
            {
                { "Left Pos Inches", 0.0f },
                { "Yaw Angle Deg", 0.0f },
                { "Right Pos Inches", 0.0f },
                { "Elevator Pos Ticks", 0.0f },
            };

    public float encoder, navX;

    public string input, request = "";

    IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);
    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    short currentIndex;
    float currentValue;

    // Start is called before the first frame update
    void Start()
    {
        clientSocket.Connect(serverAddress);
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
            clientSocket.Send(new byte[] { 100 });
            isData = true;
        }
    }
}