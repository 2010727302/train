using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

public class follow3 : MonoBehaviour {

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
    void Start() {
        dis = 0;
        speed = 5;
        //AudioSource.PlayClipAtPoint(shellExplosionAudioClip[1], new Vector3(-84, 9, -177));
        text1[0].GetComponentInChildren<TextMesh>().text = "正在前往校验学生证" + "\n" + text1[0].name;
    }
    int flag1 = 0, flag2 = 0,flag3=0;
    // Update is called once per frame
    [System.Obsolete]
    void Update() {
        //计算距离
        dis += Time.deltaTime * speed;
        Vector3 prev1 = transform.position;
        //获取相应距离在路径上的位置坐标
        transform.position = circuit.GetRoutePoint(dis).position;
        Debug.LogFormat("--- PathComplete to pos:{0}", transform.position);
        //Debug.LogFormat("--- PathComplete to pos:{0}", transform.position);
        //获取相应距离在路径上的方向
        transform.rotation = Quaternion.LookRotation(circuit.GetRoutePoint(dis).direction);

        //(-71.8, 8.5, -22.7)
        //储存自动寻路的点的坐标
        if (Math.Abs(transform.position.x - (-71.8f)) < 1.0f 
            && Math.Abs(transform.position.y - (8.5f)) < 1.0f 
            && Math.Abs(transform.position.z - (-22.7f)) < 1.0f && flag1 == 0){
            text1[1].name = "ticket check";
            text1[1].GetComponentInChildren<TextMesh>().text = "正在前往检票口2"  + "\n" + text1[1].name;
            AudioSource.PlayClipAtPoint(shellExplosionAudioClip[0], new Vector3(-72, 9, -23));
            Debug.LogFormat("--- PathComplete to pos:{0}", transform.position.x - (-19.9f));

            flag1 = 1;
            flag2 = 0;
            flag3 = 0;
        }
        //(-61.6, 8.7, -64.1)
        if (Math.Abs(transform.position.x - (-61.6f)) < 1.0f 
            && Math.Abs(transform.position.y - (8.8f)) < 1.0f 
            && Math.Abs(transform.position.z - (-64.1f)) < 1.0f && flag2 == 0) {
            AudioSource.PlayClipAtPoint(shellExplosionAudioClip[1], new Vector3(-63, 9, -85));
            text1[2].name = "coach";
            text1[2].GetComponentInChildren<TextMesh>().text = "正在前往车厢10"  + "\n" + text1[0].name;
            flag2 = 1;
            flag1 = 0;
            flag3 = 0;
        }
        //(-53.3, -0.2, -20.1)
        if (Math.Abs(transform.position.x - (-53.3f)) < 1.0f 
            && Math.Abs(transform.position.y - (-0.2f)) < 1.0f 
            && Math.Abs(transform.position.z - (-20.1f)) < 1.0f && flag3 == 0) {
            AudioSource.PlayClipAtPoint(shellExplosionAudioClip[1], new Vector3(-53, 0, -20));
            text1[0].name = "window";
            text1[0].GetComponentInChildren<TextMesh>().text = "正在前往校验学生证" + "\n" + text1[0].name;
            flag3 = 1;
            flag1 = 0;
            flag2 = 0;
        }

    }
}