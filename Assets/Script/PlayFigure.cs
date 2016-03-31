using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Remoting.Messaging;
public class PlayFigure : MonoBehaviour {
    
    public GameObject figure;
    private bool playable = true;
    private static IPAddress ipAdd = IPAddress.Parse("127.0.0.1");
    private IPEndPoint remoteEP = new IPEndPoint(ipAdd, 666);
    private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
    private string data;
  
    void Start()
    {
        socket.Connect(remoteEP);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
               
               
               if (hit.transform.gameObject.CompareTag("FiguresUI"))
               {
                   figure = hit.transform.gameObject;
                   sendData(figure.name);
                   playable = false;
                               
               }
               if (hit.transform.gameObject.CompareTag("Place"))
               {
                   playable = true;
                   Debug.Log(playable);
               }
               
            }
            
        }
      
       
    }
    public void sendData(string data)
    {
        
       
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(data);
        socket.Send(byteData);
        socket.Close();
        
    }
    public string receiveData()
    {
       
        byte[] byteData= new byte[100];
        socket.Receive(byteData, 0, SocketFlags.None);
        data = System.Text.Encoding.ASCII.GetString(byteData,0,byteData.Length);
        socket.Close();
        return data;
    }
   
}
