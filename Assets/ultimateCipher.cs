using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;

public class ultimateCipher : MonoBehaviour {
    
    public TextMesh[] screenTexts;
    public string[] wordList;
    public KMBombInfo Bomb;
    public KMBombModule module;
    public AudioClip[] sounds;
    public KMAudio Audio;
    public TextMesh submitText;
   
    
    private string[] matrixWordList =
      {
                "ACID",
                "BUST",
                "CODE",
                "DAZE",
                "ECHO",
                "FILM",
                "GOLF",
                "HUNT",
                "ITCH",
                "JURY",
                "KING",
                "LIME",
                "MONK",
                "NUMB",
                "ONLY",
                "PREY",
                "QUIT",
                "RAVE",
                "SIZE",
                "TOWN",
                "URGE",
                "VERY",
                "WAXY",
                "XYLO",
                "YARD",
                "ZERO",
                "ABORT",
                "BLEND",
                "CRYPT",
                "DWARF",
                "EQUIP",
                "FANCY",
                "GIZMO",
                "HELIX",
                "IMPLY",
                "JOWLS",
                "KNIFE",
                "LEMON",
                "MAJOR",
                "NIGHT",
                "OVERT",
                "POWER",
                "QUILT",
                "RUSTY",
                "STOMP",
                "TRASH",
                "UNTIL",
                "VIRUS",
                "WHISK",
                "XERIC",
                "YACHT",
                "ZEBRA",
                "ADVICE",
                "BUTLER",
                "CAVITY",
                "DIGEST",
                "ELBOWS",
                "FIXURE",
                "GOBLET",
                "HANDLE",
                "INDUCT",
                "JOKING",
                "KNEADS",
                "LENGTH",
                "MOVIES",
                "NIMBLE",
                "OBTAIN",
                "PERSON",
                "QUIVER",
                "RACHET",
                "SAILOR",
                "TRANCE",
                "UPHELD",
                "VANISH",
                "WALNUT",
                "XYLOSE",
                "YANKED",
                "ZODIAC",
                "ALREADY",
                "BROWSED",
                "CAPITOL",
                "DESTROY",
                "ERASING",
                "FLASHED",
                "GRIMACE",
                "HIDEOUT",
                "INFUSED",
                "JOYRIDE",
                "KETCHUP",
                "LOCKING",
                "MAILBOX",
                "NUMBERS",
                "OBSCURE",
                "PHANTOM",
                "QUIETLY",
                "REFUSAL",
                "SUBJECT",
                "TRAGEDY",
                "UNKEMPT",
                "VENISON",
                "WARSHIP",
                "XANTHIC",
                "YOUNGER",
                "ZEPHYRS",
                "ADVOCATE",
                "BACKFLIP",
                "CHIMNEYS",
                "DISTANCE",
                "EXPLOITS",
                "FOCALIZE",
                "GIFTWRAP",
                "HOVERING",
                "INVENTOR",
                "JEALOUSY",
                "KINSFOLK",
                "LOCKABLE",
                "MERCIFUL",
                "NOTECARD",
                "OVERCAST",
                "PERILOUS",
                "QUESTION",
                "RAINCOAT",
                "STEALING",
                "TREASURY",
                "UPDATING",
                "VERTICAL",
                "WISHBONE",
                "XENOLITH",
                "YEARLONG",
                "ZEALOTRY"
        };

    private string[][] pages;
    private string answer;
    private int page;
    private bool submitScreen;
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    public KMSelectable leftArrow;
    public KMSelectable rightArrow;
    public KMSelectable submit;
    public KMSelectable[] keyboard;
    void Awake()
    {
        moduleId = moduleIdCounter++;
        leftArrow.OnInteract += delegate () { left(leftArrow); return false; };
        rightArrow.OnInteract += delegate () { right(rightArrow); return false; };
        submit.OnInteract += delegate () { submitWord(submit); return false; };
        foreach(KMSelectable keybutton in keyboard)
        {
            KMSelectable pressedButton = keybutton;
            pressedButton.OnInteract += delegate () { letterPress(pressedButton); return false; };
        }
    }
        // Use this for initialization
        void Start ()
    
    {
        submitText.text = "1";
        //Generating random word
        answer = wordList[UnityEngine.Random.Range(0, wordList.Length)].ToUpper();
        Debug.LogFormat("[Black Cipher #{0}] Generated Word: {1}", moduleId, answer);
       
        pages = new string[2][];
        pages[0] = new string[3];
        pages[1] = new string[3];
        pages[0][0] = "";
        pages[0][1] = "";
        pages[0][2] = "";
        string encrypt = blackcipher(answer);
        pages[0][0] = encrypt.ToUpper();
        page = 0;
        getScreens();
    }
    string blackcipher(string word)
    {
        Debug.LogFormat("[Black Cipher #{0}] Begin Enigma Encryption", moduleId);
        string encrypt = EnigmaEnc(word.ToUpper());
        Debug.LogFormat("[Black Cipher #{0}] Begin Railfence Transposition", moduleId);
        encrypt = RailfenceTrans(encrypt.ToUpper());
        Debug.LogFormat("[Black Cipher #{0}] Begin Digrafid Encryption", moduleId);
        encrypt = DigrafidEnc(encrypt.ToUpper());
        return encrypt;
    }
    string EnigmaEnc(string word)
    {
        string[][] rotors =
        {
           new string[]
             {
                "EKMFLGDQVZNTOWYHXUSPAIBRCJ",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "DQ"
            },
           new string[]
           {
                "AJDKSIRUXBLHWTMCQGZNPYFVOE",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "ER"
            },
           new string[]
           {
                "BDFHJLCPRTXVZNYEIWGAKMUSQO",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "IV"
            },
           new string[]
           {
                "ESOVPZJAYQUIRHXLNFTGKDCMWB",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "JW"
            },
           new string[]
           {
                "VZBRGITYUPSDNHLXAWMJQOFECK",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "ZM"
            },
           new string[]
           {
                "JPGVOUMFYQBENHZRDKASXLICTW",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "LY"
            },
           new string[]
           {
                "NZJHGRCXMYSWBOUFAIVLPEKQDT",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "HU"
            },
           new string[]
           {
                "FKQHTLXOCBJSPDZRAMEWNIUYGV",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "CP"
            }
        };
        string[][] reflectors =
        {
            new string[]
             {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "LUSNPQOMJIYAHDGEFXCVBTZRKW"
             },
            new string[]
             {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "XQUMFEPOWLTJDZHGBVYKCRIASN"
             },
            new string[]
            {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "ESKOAQMJYHCPGTDLFUBNRXZVIW"
            }
        };
        string[] roman = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII"};
        string rotornums = "01234567";
        string reflectorlets = "ABC";
        string[][] rotorsetup = new string[5][];
        //Generate Reflector
        int num = UnityEngine.Random.Range(0, 3);
        rotorsetup[0] = reflectors[num];
        string rotorconfig = reflectorlets[num] + "";
        string rotorlets = "";
        //Generate Rotors
        for (int aa = 0; aa < 3; aa++)
        {
            char pickedrotor = rotornums[UnityEngine.Random.Range(0, rotornums.Length)];
            rotornums = rotornums.Replace(pickedrotor + "", "");
            rotorsetup[aa + 1] = rotors[pickedrotor - '0'];
            rotorconfig = rotorconfig + "-" + roman[pickedrotor - '0'];
            num = UnityEngine.Random.Range(0, 26);
            rotorsetup[aa + 1][0] = rotorsetup[aa + 1][0].Substring(num) + "" + rotorsetup[aa + 1][0].Substring(0, num);
            rotorsetup[aa + 1][1] = rotorsetup[aa + 1][1].Substring(num) + "" + rotorsetup[aa + 1][1].Substring(0, num);
            rotorlets = rotorlets + "" + rotorsetup[aa + 1][1][0];
        }
        rotorsetup[4] = new string[]
            {"ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"};
        //Generating Plugboard
        string plugboard = "";
        int numletterswaps = UnityEngine.Random.Range(3, 6);
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int aa = 0; aa < numletterswaps; aa++)
        {
            char l1 = alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
            alphabet = alphabet.Replace(l1 + "", "");
            char l2 = alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
            alphabet = alphabet.Replace(l2 + "", "");
            plugboard = plugboard + "" + l1 + "" + l2 + "-";
            for (int bb = 0; bb < 2; bb++)
            {
                rotorsetup[4][bb] = rotorsetup[4][bb].Replace(l1, '1');
                rotorsetup[4][bb] = rotorsetup[4][bb].Replace(l2, l1);
                rotorsetup[4][bb] = rotorsetup[4][bb].Replace('1', l2);
            }
        }
        plugboard = plugboard.Substring(0, plugboard.Length - 1);
        pages[1][0] = rotorconfig.ToUpper();
        pages[1][1] = rotorlets.ToUpper();
        pages[1][2] = plugboard.ToUpper();
        //Start Encrypting
        string encrypt = "";
        for(int cc = 0; cc < 6; cc++)
        {
            //Letter Encryption
            char let = word[cc];
            string logoutput = let + "";
            for(int dd = rotorsetup.Length - 1; dd > 0; dd--)
            {
                let = rotorsetup[dd - 1][0][rotorsetup[dd][1].IndexOf(let)];
                logoutput = logoutput + "->" + let;
            }
            let = rotorsetup[0][0][rotorsetup[0][1].IndexOf(let)];
            logoutput = logoutput + "->" + let;
            for(int ee = 0; ee < rotorsetup.Length - 1; ee++)
            {
                let = rotorsetup[ee + 1][1][rotorsetup[ee][0].IndexOf(let)];
                logoutput = logoutput + "->" + let;
            }
            encrypt = encrypt + "" + let;
            //Rotor Turning
            if(rotorsetup[2][1][0] == rotorsetup[2][2][0] || rotorsetup[2][1][0] == rotorsetup[2][2][1])
            {
                rotorsetup[2][0] = rotorsetup[2][0].Substring(1) + "" + rotorsetup[2][0][0];
                rotorsetup[2][1] = rotorsetup[2][1].Substring(1) + "" + rotorsetup[2][1][0];
                rotorsetup[1][0] = rotorsetup[1][0].Substring(1) + "" + rotorsetup[1][0][0];
                rotorsetup[1][1] = rotorsetup[1][1].Substring(1) + "" + rotorsetup[1][1][0];
            }
            else if(rotorsetup[3][1][0] == rotorsetup[3][2][0] || rotorsetup[3][1][0] == rotorsetup[3][2][1])
            {
                rotorsetup[2][0] = rotorsetup[2][0].Substring(1) + "" + rotorsetup[2][0][0];
                rotorsetup[2][1] = rotorsetup[2][1].Substring(1) + "" + rotorsetup[2][1][0];
            }
            rotorsetup[3][0] = rotorsetup[3][0].Substring(1) + "" + rotorsetup[3][0][0];
            rotorsetup[3][1] = rotorsetup[3][1].Substring(1) + "" + rotorsetup[3][1][0];
            Debug.LogFormat("[Black Cipher #{0}] {1}", moduleId, logoutput);
        }
        return encrypt;
    }
    string RailfenceTrans(string word)
    {
        string[] letterrows = new string[(Bomb.GetPortCount() % 4) + 2];
        int offset = 1;
        int cur = 0;
        letterrows[0] = word[0].ToString();
        for(int aa = 1; aa < 6; aa++)
        {
            cur += offset;
            letterrows[cur] = letterrows[cur] + "" + word[aa];
            if (cur == letterrows.Length - 1)
               offset = -1;
            else if(cur == 0)
               offset = 1;
        }
        string encrypt = "";
        for(int bb = 0; bb < letterrows.Length; bb++)
        {
            encrypt = encrypt + "" + letterrows[bb].ToUpper();
            Debug.LogFormat("[Black Cipher #{0}] Railfence Row #{1}: {2}", moduleId, bb + 1, letterrows[bb]);
        }
        return encrypt;
    }
    string DigrafidEnc(string word)
    {
        string kw1 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        string kw2 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        while(kw1.Equals(kw2))
            kw2 = matrixWordList[UnityEngine.Random.Range(0, matrixWordList.Length)];
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string key1 = getKey(kw1.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumberLetters().First()) % 2 == 1);
        string key2 = getKey(kw2.ToUpper(), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(Bomb.GetSerialNumberLetters().Last()) % 2 == 0);
        Debug.LogFormat("[Black Cipher #{0}] Key A: {1}", moduleId, key1);
        Debug.LogFormat("[Black Cipher #{0}] Key B: {1}", moduleId, key2);
        key1 = key1 + "#";
        key2 = key2 + "#";
        string[] numbers = {"", "", ""};
        string[] grid3x3 = {"123", "456", "789"};
        for(int aa = 0; aa < 3; aa++)
        {
            int num = key1.IndexOf(word[(aa * 2)]);
            numbers[0] = numbers[0] + "" + ((num % 9) + 1);
            int row = num / 9;
            num = key2.IndexOf(word[(aa * 2) + 1]);
            numbers[2] = numbers[2] + "" + ((num % 9) + 1);
            int col = num / 9;
            numbers[1] = numbers[1] + "" + grid3x3[row][col];
            Debug.LogFormat("[Black Cipher #{0}] {1}{2} -> {3}{4}{5}", moduleId, word[aa * 2], word[(aa * 2) + 1], numbers[0][aa], numbers[1][aa], numbers[2][aa]);
        }
        string encrypt = "";
        for(int bb = 0; bb < 3; bb++)
        {
            encrypt = encrypt + "" + key1[((numbers[bb][0] - '0') - 1) + (((numbers[bb][1] - '0') - 1) / 3) * 9];
            encrypt = encrypt + "" + key2[((numbers[bb][2] - '0') - 1) + (((numbers[bb][1] - '0') - 1) % 3) * 9];
            Debug.LogFormat("[Black Cipher #{0}] Digrafid Row #{1}: {2} -> {3}{4}", moduleId, bb + 1, numbers[bb], encrypt[bb * 2], encrypt[(bb * 2) + 1]);
        }
        if (encrypt.Contains('#'))
        {
            Debug.LogFormat("[Black Cipher #{0}] ERROR!!! REGENERATING KEYWORDS!!!", moduleId);
            Debug.LogFormat("[Black Cipher #{0}] PAY NO ATTENTION TO THE ENCRYPTION ABOVE!!!", moduleId);
            Debug.LogFormat("[Black Cipher #{0}] --------------------------------------------", moduleId);
            return DigrafidEnc(word.ToUpper());
        }
           
        else
        {
            pages[0][1] = kw1.ToUpper();
            pages[0][2] = kw2.ToUpper();
            return encrypt;
        }
    }
    string getKey(string k, string alpha, bool start)
    {
        for (int aa = 0; aa < k.Length; aa++)
        {
            for (int bb = aa + 1; bb < k.Length; bb++)
            {
                if (k[aa] == k[bb])
                {
                    k = k.Substring(0, bb) + "" + k.Substring(bb + 1);
                    bb--;
                }
            }
            alpha = alpha.Replace(k[aa].ToString(), "");
        }
        if (start)
            return (k + "" + alpha);
        else
            return (alpha + "" + k);
    }
	int correction(int p, int max)
    {
        while (p < 0)
            p += max;
        while (p >= max)
            p -= max;
        return p;
    }
    void left(KMSelectable arrow)
    {
        if(!moduleSolved)
        {
            Audio.PlaySoundAtTransform(sounds[0].name, transform);
            submitScreen = false;
            arrow.AddInteractionPunch();
            page--;
            page = correction(page, pages.Length);
            getScreens();
        }
    }
    void right(KMSelectable arrow)
    {
        if(!moduleSolved)
        {
            Audio.PlaySoundAtTransform(sounds[0].name, transform);
            submitScreen = false;
            arrow.AddInteractionPunch();
            page++;
            page = correction(page, pages.Length);
            getScreens();
        }
    }
    private void getScreens()
    {
        submitText.text = (page + 1) + "";
        screenTexts[0].text = pages[page][0];
        screenTexts[1].text = pages[page][1];
        screenTexts[2].text = pages[page][2];
        screenTexts[1].fontSize = 40;
        if(page == 0)
        {
            screenTexts[0].fontSize = 40;
            screenTexts[2].fontSize = 40;
        }
        else
        {
            screenTexts[0].fontSize = 35;
            screenTexts[2].fontSize = 25;
        }
            

    }
    void submitWord(KMSelectable submitButton)
    {
        if(!moduleSolved)
        {
            submitButton.AddInteractionPunch();
            if(screenTexts[2].text.Equals(answer))
            {
                Audio.PlaySoundAtTransform(sounds[2].name, transform);
                module.HandlePass();
                moduleSolved = true;
                screenTexts[2].text = "";
            }
            else
            {
                Audio.PlaySoundAtTransform(sounds[3].name, transform);
                module.HandleStrike();
                page = 0;
                getScreens();
                submitScreen = false;
            }
        }
    }
    void letterPress(KMSelectable pressed)
    {
        if(!moduleSolved)
        {
            pressed.AddInteractionPunch();
            Audio.PlaySoundAtTransform(sounds[1].name, transform);
            if (submitScreen)
            {
                if(screenTexts[2].text.Length < 6)
                {
                    screenTexts[2].text = screenTexts[2].text + "" + pressed.GetComponentInChildren<TextMesh>().text;
                }
            }
            else
            {
                submitText.text = "SUB";
                screenTexts[0].text = "";
                screenTexts[1].text = "";
                screenTexts[2].text = pressed.GetComponentInChildren<TextMesh>().text;
                submitScreen = true;
            }
        }
    }
#pragma warning disable 414
    private string TwitchHelpMessage = "Move to other screens using !{0} right|left|r|l|. Submit the decrypted word with !{0} submit qwertyuiopasdfghjklzxcvbnm";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {

        if (command.EqualsIgnoreCase("right") || command.EqualsIgnoreCase("r"))
        {
            yield return null;
            rightArrow.OnInteract();
            yield return new WaitForSeconds(0.1f);

        }
        if (command.EqualsIgnoreCase("left") || command.EqualsIgnoreCase("l"))
        {
            yield return null;
            leftArrow.OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        string[] split = command.ToUpperInvariant().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        if (split.Length != 2 || !split[0].Equals("SUBMIT") || split[1].Length != 6) yield break;
        int[] buttons = split[1].Select(getPositionFromChar).ToArray();
        if (buttons.Any(x => x < 0)) yield break;

        yield return null;

        yield return new WaitForSeconds(0.1f);
        foreach (char let in split[1])
        {
            keyboard[getPositionFromChar(let)].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        submit.OnInteract();
        yield return new WaitForSeconds(0.1f);
    }

    private int getPositionFromChar(char c)
    {
        return "QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(c);
    }
}
