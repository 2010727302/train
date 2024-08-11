﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

public class follow2 : MonoBehaviour
{

    // 路径脚本
    [SerializeField] private WaypointCircuit circuit;
    private LineRenderer lineRenderer;
    public NavMeshAgent agent;
    public AudioClip[] shellExplosionAudioClip = new AudioClip[] { };
    public GameObject[] text1 = new GameObject[] { };
    private int num = 0;
    private const float vcon = 0.1f;
    // public NavMeshAgent agent;

    //移动距离
    private float dis;
    //移动速度
    private float speed;
    // Use this for initialization
    void Start()
    {
        dis = 0;
        speed = 5;
       // text1[0].GetComponentInChildren<TextMesh>().text = "正在前往候车厅"  + "\n" + text1[0].name;
    }
    int flag1 = 0, flag2 = 0,flag3=0;
    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        //计算距离
        dis += Time.deltaTime * speed;
        Vector3 prev1 = transform.position;
        //获取相应距离在路径上的位置坐标
        transform.position = circuit.GetRoutePoint(dis).position;
        Debug.LogFormat("--- PathComplete to pos:{0}", transform.position);
        //Debug.LogFormat("--- PathComplete to pos:{0}", transform.position);
        //获取相应距离在路径上的方向
        transform.rotation = Quaternion.LookRotation(circuit.GetRoutePoint(dis).direction);

        //(-80.5, 8.6, -19.7)
        //储存自动寻路的点的坐标
        if (Math.Abs(transform.position.x - (-80.5f)) < 1.0f && Math.Abs(transform.position.y - (8.6f)) < 1.0f && Math.Abs(transform.position.z - (-19.7f)) < 1.0f&& flag1 == 0)
        {
            //text1[1].name = "coach";
           // text1[1].GetComponentInChildren<TextMesh>().text = "正在前往" + (-13.61, -0.08, -155.5) + "\n" + text1[1].name;
            AudioSource.PlayClipAtPoint(shellExplosionAudioClip[0], new Vector3(-80, 9, -20));
            //Debug.LogFormat("--- PathComplete to pos:{0}", transform.position.x - (-19.9f));

            flag1 = 1;
            flag2 = 0;
            flag3 = 0;
        }
        //(-63.8, 8.8, -84.7)
        if (Math.Abs(transform.position.x - (-62.0f)) < 1.0f && Math.Abs(transform.position.y - (8.7f)) < 1.0f && Math.Abs(transform.position.z - (-63.7f)) < 1.0f && flag2 == 0) 
        { 
            AudioSource.PlayClipAtPoint(shellExplosionAudioClip[1], new Vector3(-62, 9, -64));
           // text1[0].name = "waiting hall";
           // text1[0].GetComponentInChildren<TextMesh>().text = "正在前往" + (-19.05, 9.09, -118.2) + "\n" + text1[0].name;
            flag2 = 1;
            flag1 = 0;
            flag3 = 0;
        }
        if (Math.Abs(transform.position.x - (-63.8f)) < 1.0f && Math.Abs(transform.position.y - (8.8f)) < 1.0f && Math.Abs(transform.position.z - (-84.7f)) < 1.0f && flag3 == 0)
        {
           // text1[1].name = "coach";
           // text1[1].GetComponentInChildren<TextMesh>().text = "正在前往" + (-13.61, -0.08, -155.5) + "\n" + text1[1].name;
            AudioSource.PlayClipAtPoint(shellExplosionAudioClip[2], new Vector3(-63, 9, -85));
           // Debug.LogFormat("--- PathComplete to pos:{0}", transform.position.x - (-19.9f));

            flag3 = 1;
            flag1 = 0;
            flag2 = 0;
        }
        //remain = agent.remainingDistance;
        //text1[0].GetComponentInChildren<TextMesh>().text = "正在前往" + transform.position + "\n" + text1[0].name;

       
    }
}