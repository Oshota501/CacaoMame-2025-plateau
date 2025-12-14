using UnityEngine;
using System.IO.Ports;
using System;

public class M5Receiver : MonoBehaviour
{
    // M5Stackがつながっているポート名に合わせてください (例: "COM3", "COM4", "/dev/tty...")
    public string portName = "/dev/cu.usbserial-59740081181";
    public int baudRate = 115200;
    public float sentibility = 0.001f ;
    SerialPort serialPort;
    
    // 回転を適用するオブジェクト
    public GameObject targetObj;

    void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 50;
            serialPort.Open();
            Debug.Log("M5Stack Connected!");
        }
        catch (Exception e)
        {
            Debug.LogError("Connection Failed: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                // M5Stackからのデータ読み取り ("pitch,roll,isBlinking")
                string message = serialPort.ReadLine();
                string[] values = message.Split(',');

                if (values.Length >= 3)
                {
                    float pitch = float.Parse(values[0]);
                    float roll = float.Parse(values[1]);
                    int blinking = int.Parse(values[2]);

                    // Unity上のオブジェクトを回転させる
                    if (targetObj != null)
                    {
                        Debug.Log(pitch);
                        Debug.Log(roll); 
                        // M5Stackの動きに合わせて回転 (軸は適宜調整してください)
                        ObjectCloner isFallObj = targetObj.GetComponent<ObjectCloner> () ;

                        targetObj.transform.position += new Vector3(-pitch*sentibility,0f,roll*sentibility);
                    }
                    
                    // まばたきデータ(0 or 1)も values[2] で取得可能です
                }
            }
            catch (TimeoutException) { }
            catch (Exception e)
            {
                Debug.LogWarning("Read Error: " + e.Message);
            }
        }
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}