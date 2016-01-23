using UnityEngine;
using System.Collections;

public class SorryManager : MonoBehaviour {

    public string keypresses = "";
    public double timer;
    private string[] combinationsRemaining;
    public string[] combinations;
    private string[] CHARAS = new string[4] { "w", "a", "s", "d" };

	// Use this for initialization
	void Start () {
        generateCombinations();
        combinationsRemaining = combinations;
    }

    void generateCombinations()
    {
        combinations = new string[5] { "", "", "", "", "" };
        for (int i = 0; i < 5; i++)
        {
            combinations[i] = generateLevel((i + 1) * 2);

            // make sure there's no conflict
            int j = 0;
            while (j < i)
            {
                if (hasConflict(combinations[j], combinations[i]))
                {
                    combinations[i] = generateLevel((i+1) * 2);
                    j = 0;
                } else
                {
                    j++;
                }
            }
        }
    }
	

    bool hasConflict(string combination, string other)
    {
        return combination.Substring(0, 2).Equals(other.Substring(0, 2));
    }

    string generateLevel(int count)
    {
        string combination = "";
        int c;
        // generate the first level
        for (int i = 0; i < count; i++)
        {
            c = Random.Range(0, 4);
            combination += CHARAS[c];
        }
        return combination;
        
    }


    void updateKeypress(string chara)
    {
        keypresses += chara;
        int len = keypresses.Length;
        print(keypresses);
        for (int i = 0; i < 5; i++)
        {
            if (!combinationsRemaining[i].Equals("") && !keypresses.Equals(combinationsRemaining[i].Substring(0, len)))
            {
                combinationsRemaining[i] = "";
            }
        }

        bool allEmpty = true;
        // check to see if there are any full matches

        for (int i = 0; i < 5; i++)
        {
            if (!combinationsRemaining[i].Equals(""))
                allEmpty = false;
            if (combinations[i].Equals(keypresses))
            {
                // cleared level i apology
            }
        }

        // if all the items are "" then we've failed -- preemptively end the game
        if (allEmpty)
        {
            timer = 0;
            // force fail
        }
    }

	// Update is called once per frame
	void Update () {
        // decrement timer
        timer -= Time.deltaTime;

        // if there's a keypress update keypresses
        if (timer > 0)
        {
            string chara = "";
            if (Input.GetKeyDown("up"))
            {
                chara = "w";
            }

            if (Input.GetKeyDown("down"))
            {
                chara = "s";
            }

            if (Input.GetKeyDown("left"))
            {
                chara = "a";
            }

            if (Input.GetKeyDown("right"))
            {
                chara = "d";
            }
            if (!chara.Equals(""))
            {
                updateKeypress(chara);
            }

        }
        else
        {
            // end minigame
        }
	}
}
