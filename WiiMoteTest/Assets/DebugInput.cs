﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Text;
using Assets;
using System.IO;
using System.Collections.Generic;

public class DebugInput : MonoBehaviour {
    private WiiMote mote;
    public GameObject cube;
    private int frames;
	// Use this for initialization
	void Start () {
	
	}
    float deltaTime = 0.0f;


    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
	
	// Update is called once per frame
	void Update () {
        frames++;
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        if (mote != null)
        {
            if (mote.isConnected)
            {
                if (frames % 6 == 0)
                {
                    mote.read2();
                    frames = 0;
                }
            }
            //Debug.Log(mote.Read());
            //Debug.Log(new Vector3(mote.wiiMoteState.accelState.xPos, mote.wiiMoteState.accelState.yPos, mote.wiiMoteState.accelState.zPos));
            cube.transform.position = new Vector3(mote.wiiMoteState.accelState.xPos, mote.wiiMoteState.accelState.yPos, mote.wiiMoteState.accelState.zPos);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(SystemInfo.operatingSystem);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            HIDAPI api = HIDAPI.GetAPI();
			api.GetDevices();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
			Debug.Log("AA");
            HIDAPI api = HIDAPI.GetAPI();
            Debug.Log("Checking");
            List<HIDDevice> list = api.GetDevices();
            Debug.Log("Found "+list.Count+" HID-Devices");
            List<WiiMote> motes = WiimoteController.FindWiimotes(list);
            Debug.Log(motes.Count + " Wiimotes found");
            mote = motes[0];
            mote.ConnectAndInitialise();
            mote.SetDataReportingMode(true, ReportingState.Buttons);

            byte[] buff = new byte[22];
            mote.read2();
            
            //Debug.Log(file.Read(buff, 22));

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            mote.ConnectAndInitialise();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            mote.RequestStatus();
            mote.SetDataReportingMode(true, ReportingState.ButtonsAndAccel);
            
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            mote.read2();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            mote.SetRumble(true);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            mote.SetRumble(false);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            Debug.Log("Disconnect: " + mote.Disconnect());
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mote.SetPlayerNumber(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mote.SetPlayerNumber(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mote.SetPlayerNumber(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            mote.SetPlayerNumber(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            mote.SetPlayerNumber(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            mote.SetPlayerNumber(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            mote.SetPlayerNumber(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            mote.SetPlayerNumber(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            mote.SetPlayerNumber(9);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            mote.SetPlayerNumber(10);
        }
        if (Input.GetKey(KeyCode.R))
        {
            mote.Connect();
            mote.read2();
        }
	}
}
