using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeFall : MonoBehaviour
{
    public string stringToEdit = "Score: ";
    public AudioSource m_MyAudioSource3;
    public int total_score;
    public string endGame = "GAME OVER";
    public string portName = "";
    private GameObject newTrafficBlock;
    bool stillPlaying;
    public int count = 0;
    private string[] necessaryPorts = { "SMTP", "DNS", "HTTPS", "HTTP"};
    private string[] sketchyPorts = { "SSH", "Telnet", "Chargen", "VNC"};
    // Allow only 3 at once for now
    List<GameObject> currentlyFallingCubes = new List<GameObject>();
    private int prob_something_falls;

    int initialHeight = 10;

    public void OnGUI()
    {
        string addToPrint = stringToEdit + total_score.ToString();
        GUI.TextField(new Rect(350, 210, 100, 20), addToPrint, 10);

        string formalPortIntro = "Port Traffic: " + portName;
        GUI.TextField(new Rect(350, 450, 150, 20), formalPortIntro);

        if (stillPlaying == false)
        {
            GUI.TextField(new Rect(600, 325, 100, 20), endGame);
        }
        if(!GlobalVariables.buttonPressed)
        {
            string instructText = "Hello, trusty firewall!!!\nYour job is to block bad network traffic (red cubes)\nfrom our network to keep our website safe.\nBe careful though-- we still want the good traffic!\nPress the button below to start.";
            GUI.TextField(new Rect(500, 300, 300, 100), instructText);
        }
    }

    public void printEndGame()
    {
        GUI.TextField(new Rect(200, 200, 200, 20), endGame, 25);
    }

    public static bool AboutEqual(double x, double y, double precision)
    {
        double epsilon = Math.Max(Math.Abs(x), Math.Abs(y)) * precision;
        return Math.Abs(x - y) <= epsilon;
    }

    public void SpawnTraffic(int type, bool malicious)
    {
        this.newTrafficBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer newBlockRenderer = this.newTrafficBlock.GetComponent<Renderer>();

        // Give it a position - need to generate an x, y, and z coordinate to fit in screen
        int z = 0;
        int y = 3;
        // Range for z is about -6 to positive 1
        bool validZ = false;
        int x = UnityEngine.Random.Range(2, 8);

        this.newTrafficBlock.transform.localPosition = new Vector3(x, y, z);

        // Change the color to indicate maliciousness
        if (malicious)
        {
            newBlockRenderer.material.color = Color.red;
        }
        else
        {
            newBlockRenderer.material.color = Color.green;
        }

        // Add new block to vector of cubes
        this.currentlyFallingCubes.Add(this.newTrafficBlock);
    }

    // Start is called before the first frame update
    void Start()
    {
        total_score = 0;
        stillPlaying = true;
        System.Threading.Thread.Sleep(5000);
    }

    // Update is called once per frame
    void Update()
    {
        if (stillPlaying && (GlobalVariables.buttonPressed))
        {
            count++;
            // See if you should make another random hecking block fall from the sky
            prob_something_falls = UnityEngine.Random.Range(0, 1000);
            if (prob_something_falls < count/25)
            {
                // One in four chance the block will be malicious
                if (UnityEngine.Random.Range(0, 100) >= 75)
                {
                    SpawnTraffic(1, true);
                }
                else
                {
                    SpawnTraffic(1, false);
                }
            }
            // Iterate through each of the falling blocks in the vector and move it
            for (int i = 0; i < this.currentlyFallingCubes.Count; i++)
            {
                this.currentlyFallingCubes[i].transform.position += 2 * Vector3.down * Time.deltaTime * 2;
                Renderer eachBlock = currentlyFallingCubes[i].GetComponent<Renderer>();
                // Check z coordinate
                if (AboutEqual(this.currentlyFallingCubes[i].transform.position.y, -4, 1E-2))
                {
                    // If malicious and it hit the floor, game over
                    if (eachBlock.material.color == Color.red)
                    {
                        stillPlaying = false;
                        m_MyAudioSource3.Play();
                        portName = sketchyPorts[UnityEngine.Random.Range(0, 4)];

                    }
                    else if (eachBlock.material.color == Color.green)
                    {
                        total_score++;
                        portName = necessaryPorts[UnityEngine.Random.Range(0, 4)];
                    }
                    // If it is not, add one to the score
                    Destroy(currentlyFallingCubes[i]);
                    currentlyFallingCubes.RemoveAt(i);
                    i = i - 1;
                }
            }
            // this.transform.position += Vector3.down * Time.deltaTime * 2;
        }
        return;
    }
}