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
    public static int portNumber = 4388;
    public int bytesAvailable;
    public SortedList<string, float> robotValues =
            new SortedList<string, float>
            {
                { "encoder", 0.0f },
                { "navX", 0.0f },
            };

    public string input;

    IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);
    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    short currentIndex;
    float currentValue;

    // Start is called before the first frame update
    void Start()
    {
        Console.WriteLine("Connecting...");
        clientSocket.Connect(serverAddress);
        Console.WriteLine("Connected");
    }

    // Update is called once per frame
    void Update()
    {
        bytesAvailable = clientSocket.Available;
        if (bytesAvailable == 12)
        {
            byte[] byteInput = new byte[100];
            clientSocket.Receive(byteInput);
            input = Encoding.UTF8.GetString(byteInput);
            string[] values = input.Split('|');
            robotValues["encoder"] = float.Parse(values[0], CultureInfo.InvariantCulture.NumberFormat);
            robotValues["navX"] = float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat);
            clientSocket.Send(new byte[] { 100 });
        }
    }
}