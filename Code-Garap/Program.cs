using System;
using System.Globalization;

//TEST BALUNGAN:
// LADRANG PANKUR - SLENDRO MANYURA (CAN BE PUT IN OTHER PATHET/LARAS) - 3231 3216 1632 5321 3532 6532 5321 3216
// GAMBIR SAWIT - SLENDRO SANGA - 0352 0356 2200 2321 0032 0126 2200 2321 0032 0165 0056 1653 2203 5321 3532 0165

//TODO
//CHALLENGE - ADD OPTION FOR OCCAISIONAL SELEH NOTES PLAYED BY BONANG
//OCTAVES CAN BE CUSTOMISED BY THE USER WHEN THEY WANT SOMETHING PLAYED BACK/PARTS PRINTED
//CHAllENGE - FIND OUT HOW NOTE 4 WORKS IN PELOG PROPERLY AND ADD EXTRA RULES
//EXCEPTION HANDLING CAN BE OPTIMISED IN FUTURE - TO SHOW ALL EXCEPTIONS INSTEAD OF JUST THE FIRST, TRY EACH SMALLER METHOD SEPARATELY
//WRITE COMMENTS THROUGHOUT THE CODE EXPLAINING THINGS ABOUT GAMELAN TO OTHER READERS:

//The Javanese Gamelan uses 2 different sets of instruments with different tuning systems (laras) - slendro (using 5 notes labelled 12356) and pelog (using 7 notes labelled 1234567)
char[] larasSlendro = { '1', '2', '3', '5', '6' };
char[] larasPelog = { '1', '2', '3', '4', '5', '6', '7' };
char[] chosenLaras = new char[7];

//Within these tuning systems, there are different scales/modes of 5 or 6 notes (pathets). 3 are listed for each laras
//Their notes are listed in ascending order, and their strong note is listed first (like C in C major).
char[] pathetSlendroManyura = { '6', '1', '2', '3', '5' };
char[] pathetSlendroSanga = { '5', '6', '1', '2', '3' };
char[] pathetSlendroNem = { '2', '3', '5', '6', '1' };
char[] pathetPelogBarang = { '6', '7', '2', '3', '5' };
char[] pathetPelogNem = { '1', '2', '3', '4', '5', '6' };
char[] pathetPelogLima = { '5', '6', '1', '2', '3', '4' };
char[] chosenPathet = new char[6];

Console.WriteLine("\nHello! This program generates a basic literal representation of some of the Javanese Gamelan parts for a balungan entered by the user.\n(Disclaimer - do not use these parts as a substitute for garap - this is only a demonstration, and definitely not a replacement!)\n\n");

//OPTIONS MENU HERE
bool checkSelehTrue = false;
bool cont = false;
Console.WriteLine("\nMenu: enter \"1\" to begin the application, enter \"2\" to change options, or enter \"3\" to exit.\n");
do
{
    string? firstInput = Console.ReadLine();
    if (firstInput != null)
    {
        firstInput = firstInput.Trim();

        switch (firstInput)
        {
            case "1":
                cont = true;
                break;
            case "2":
                //ALL THIS GOES IN Options(); ?
                //1) CHECKSELEHTRUE IS OFF BY DEFAULT, HAVE AN OPTION TO TURN IT ON

                //2) OPTION TO DEFINE LIMITED KEMPUL PITCHES IN AN ARRAY:
                //USE ARRAYS AVAILABLE KEMPUL NOTES LIKE THE LARAS ARRAYS (ONE FOR SLENDRO AND ONE FOR PELOG) AND ASK THE USER TO LIST ALL THE NOTES THEY WANT
                //USE A METHOD IN KEMPUL GENERATION CALLED CHOOSEKEMPULPITCH(NOTE) TO PICK AN APPROPRIATE PITCH 
                //PRIORITY - SAME PITCH; SECOND - A PITCH 3 NOTES DOWN; THIRD - A PITCH 2 NOTES UP; FOURTH - THE CLOSEST PITCH
                //IF NOTE IS PELOG 4 - PRIORITY NOTE 4; SECOND NOTE 2; OTHERWISE REST
                DefineKempulNotes();

                //3) OPTION TO TURN ON ALTERNATIVE BONANG PATTERNS (1 - SELEH NOTES, 2 - BARUNG DADOS MISSING A NOTE 03230023)

                //4) OPTION TO TURN OFF "NO DOUBLES" RULE FOR PEKING (SOME BALUNGAN MIGHT HAVE IMPORTANT UNISON BITS)

                Console.WriteLine("Under construction.");
                cont = true; //DELETE THIS AFTERWARDS
                break;
            case "3":
                Console.WriteLine("Under construction/not necessary?");
                cont = true;
                break;
            default:
                Console.WriteLine("Invalid entry. Please enter \"1\" to begin the application, enter \"2\" to change options, or enter \"3\" to exit.");
                break;
        }
    }
    else
        Console.WriteLine("\nMenu: enter \"1\" to begin the application, enter \"2\" to change options, or enter \"3\" to exit.\n");
} while (!cont);

Console.WriteLine("Firstly, please choose the laras of your balungan: enter \"1\" for slendro or \"2\" for pelog.\n");
string userLaras = "initialise";
bool userLarasValid = false;
do
{
    string? larasInput = Console.ReadLine();
    if (larasInput != null)
    {
        larasInput = larasInput.Trim();

        try
        {
            userLaras = GetUserLaras(larasInput);
            userLarasValid = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    else
    {
        Console.WriteLine("Please choose the laras of your balungan: enter \"1\" for slendro or \"2\" for pelog.\n");
        userLarasValid = false;
    }
} while (userLarasValid == false);
Console.WriteLine($"You have chosen laras {userLaras}.\n");

string userPathet = "initialise";
bool userPathetValid = false;

Console.WriteLine("\nNow, please choose the pathet of your balungan.");
DisplayPathetOptions(userLaras);
do
{
    string? pathetInput = Console.ReadLine();
    if (pathetInput != null)
    {
        pathetInput = pathetInput.Trim();

        try
        {
            userPathet = GetUserPathet(pathetInput);
            userPathetValid = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            DisplayPathetOptions(userLaras);
        }
    }
    else
    {
        Console.WriteLine("\nPlease choose the pathet of your balungan.");
        DisplayPathetOptions(userLaras);
    }
} while (userPathetValid == false);
Console.WriteLine($"You have chosen pathet {userPathet}.\n");

string? userInput;
char[] userInputArr = new char[1000];
int noteCounter = 0;
int userTotalGatras;
bool userNotesValid = false;
bool userInputValid = false;

do
{
    Console.WriteLine($"Please enter a 4, 8, or 16 gatra balungan in {userLaras} {userPathet}.\nYou may use spaces between gatra or anywhere you like. Please use \"-\" or \"0\" to depict a rest.\n");

    if (checkSelehTrue)
        Console.WriteLine($"Tip: For {userLaras} {userPathet}, the \"strong note\" (which should be the final seleh) is {chosenPathet[0]}.\n");

    Array.Clear(userInputArr);
    userInput = Console.ReadLine();
    if (userInput != null)
    {
        userInput = userInput.Replace(" ", "");
        userInput = userInput.Replace("-", "0");
        noteCounter = 0;
        userNotesValid = false;

        userInputArr = userInput.ToCharArray();
        noteCounter = userInputArr.Length;

        try
        {
            userNotesValid = CheckNotesValid(userInputArr, chosenLaras, chosenPathet);
        }
        catch (FormatException ex)
        {
            Console.WriteLine(ex.Message);
            foreach (char note2 in chosenPathet)
            {
                Console.Write($"{note2} ");
            }
            Console.Write("\n\n");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }


        if (userNotesValid == true)
        {
            userTotalGatras = noteCounter / 4;
            try
            {
                userInputValid = CheckLengthAndSeleh(userInputArr, chosenPathet, noteCounter, checkSelehTrue);
                Console.WriteLine($"You have entered a valid balungan of {userTotalGatras} gatras.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
} while (!userInputValid || !userNotesValid);

NoteFourWarning(userInputArr, chosenPathet);

int displayCounter = 0;
char[] pekingPartTanggung = new char[noteCounter * 2];
char[] bonangPanerusPartTanggung = new char[noteCounter * 4];
char[] bonangBarungPartTanggung = new char[noteCounter * 2];
char[] saronSlenthemPartTanggung = new char[noteCounter];
char[] kenongPartTanggung = new char[noteCounter];
char[] kempulPartTanggung = new char[noteCounter];

//GENERATING EACH PART IN IRAMA TANGGUNG

//BONANG PANERUS GENERATION
//Bonang panerus plays at a speed of 4 notes per beat, with a doubled-up 0aba pattern once per beat e.g. 0aba0aba0cdc0cdc for gatra abcd.
//The pattern happens before the beat instead of afterwards (0 a b a BEATA a b a BEATB - rather than - BEATA a b a BEATB a b a ).
//Instead of using a rest note (0), the bonang uses the previous note that is not a 0, or the final seleh note if there is none.
//One exception is that the bonang panerus (and barung) play the final seleh on the beat instead of a rest.

int generateCounter = 0;
foreach (char note in userInputArr)
{
    generateCounter++;
    if (generateCounter % 2 == 0)
    {
        if ((userInputArr[generateCounter - 2] == '0') && (userInputArr[generateCounter - 1] != '0'))
        {
            if (FindPreviousNonZero(generateCounter - 2) != '0')
                GenerateBonangPanerusNotesGroup(generateCounter, FindPreviousNonZero(generateCounter - 2), userInputArr[generateCounter - 1]);
            else
                GenerateBonangPanerusNotesGroup(generateCounter, userInputArr[noteCounter - 1], userInputArr[generateCounter - 1]);
        }

        else if ((userInputArr[generateCounter - 2] == '0') && (userInputArr[generateCounter - 1] == '0'))
        {
            if (FindPreviousNonZero(generateCounter - 1) != '0')
                GenerateBonangPanerusNotesGroup(generateCounter, FindPreviousNonZero(generateCounter - 1), FindPreviousNonZero(generateCounter - 1));
            else
                GenerateBonangPanerusNotesGroup(generateCounter, userInputArr[noteCounter - 1], userInputArr[noteCounter - 1]);
        }

        else if ((userInputArr[generateCounter - 1] == '0') && (userInputArr[generateCounter - 2] != '0'))
            GenerateBonangPanerusNotesGroup(generateCounter, userInputArr[generateCounter - 2], userInputArr[generateCounter - 2]);

        else
            GenerateBonangPanerusNotesGroup(generateCounter, userInputArr[generateCounter - 2], userInputArr[generateCounter - 1]);
    }
}
GenerateBonangPanerusNotes(generateCounter, 4, -4, -1, bonangPanerusPartTanggung[1], bonangPanerusPartTanggung[2], userInputArr[noteCounter - 1]);

//BONANG BARUNG GENERATION
//Bonang barung plays at the a speed of 2 notes per beat. It plays the same pattern as the bonang panerus but at half speed, and only one occurance of each.
//Instead of using a rest note (0), the bonang uses the previous note that is not a 0, or the final seleh note if there is none.
generateCounter = 0;
foreach (char note in userInputArr)
{
    generateCounter++;
    if (generateCounter % 2 == 0)
    {
        if ((userInputArr[generateCounter - 2] == '0') && (userInputArr[generateCounter - 1] != '0'))
        {
            if (FindPreviousNonZero(generateCounter - 2) != '0')
                GenerateBonangBarungNotesGroup(generateCounter, FindPreviousNonZero(generateCounter - 2), userInputArr[generateCounter - 1]);
            else
                GenerateBonangBarungNotesGroup(generateCounter, userInputArr[noteCounter - 1], userInputArr[generateCounter - 1]);
        }
        else if ((userInputArr[generateCounter - 2] == '0') && (userInputArr[generateCounter - 1] == '0'))
        {
            if (FindPreviousNonZero(generateCounter - 1) != '0')
                GenerateBonangBarungNotesGroup(generateCounter, FindPreviousNonZero(generateCounter - 1), FindPreviousNonZero(generateCounter - 1));
            else
                GenerateBonangBarungNotesGroup(generateCounter, userInputArr[noteCounter - 1], userInputArr[noteCounter - 1]);
        }
        else if ((userInputArr[generateCounter - 1] == '0') && (userInputArr[generateCounter - 2] != '0'))
            GenerateBonangBarungNotesGroup(generateCounter, userInputArr[generateCounter - 2], userInputArr[generateCounter - 2]);

        else
            GenerateBonangBarungNotesGroup(generateCounter, userInputArr[generateCounter - 2], userInputArr[generateCounter - 1]);
    }
}
bonangBarungPartTanggung[(generateCounter * 2) - 2] = userInputArr[noteCounter - 1];
if (userInputArr[0] != '0')
    bonangBarungPartTanggung[(generateCounter * 2) - 1] = userInputArr[0];
else if (userInputArr[0] == '0')
    bonangBarungPartTanggung[(generateCounter * 2) - 1] = FindPreviousNonZero(noteCounter - 1);

//PEKING GENERATION
//Each note is doubled up in tanggung at twice the speed of the saron/slenthem. The peking plays continuously, including over rests.
//(the surakarta style that this code uses is a change of note on the beat, whereas yogyanese style is a change of note a half-beat before).

generateCounter = 0;
foreach (char note in userInputArr)
{
    if (note == '0')
    {
        generateCounter++;
        if (generateCounter == 1)
        {
            pekingPartTanggung[(generateCounter * 2) - 2] = userInputArr[noteCounter - 1];
            pekingPartTanggung[(generateCounter * 2) - 1] = userInputArr[noteCounter - 1];
        }
        else if (generateCounter > 1)
        {
            pekingPartTanggung[(generateCounter * 2) - 2] = pekingPartTanggung[(generateCounter * 2) - 3];
            pekingPartTanggung[(generateCounter * 2) - 1] = pekingPartTanggung[(generateCounter * 2) - 3];
        }
    }
    else
    {
        generateCounter++;
        pekingPartTanggung[(generateCounter * 2) - 2] = userInputArr[generateCounter - 1];
        pekingPartTanggung[(generateCounter * 2) - 1] = userInputArr[generateCounter - 1];
    }
}

//CORRECTIONS FOR PEKINGAN RULES
//Note: You cannot play two pairs of the same note consecutively. Some extra statements have been added for this below.
int pekingCounter = 0;
foreach (char note in pekingPartTanggung)
{
    pekingCounter++;
    char pekingNoteUp = '0';
    char pekingNoteDown = '0';
    char pekingNotePrevious = '0';
    char pekingNotePreviousSpecial = '0';
    int pekingNoteUpIndex = 0;
    int pekingNoteDownIndex = 0;
    int pekingNotePreviousIndex = 0;

    //Looking at (double) notes 1 and 3 in each gatra - these are the notes that change
    if (pekingCounter % 2 == 0 && pekingCounter % 4 != 0)
    {
        if (pekingCounter > 2 && pekingPartTanggung[pekingCounter - 1] == pekingPartTanggung[pekingCounter - 3])
            pekingNotePreviousSpecial = pekingPartTanggung[pekingCounter - 3];

        if (pekingCounter == 2 && pekingPartTanggung[pekingCounter - 1] == pekingPartTanggung[pekingPartTanggung.Length - 1])
            pekingNotePreviousSpecial = pekingPartTanggung[pekingPartTanggung.Length - 1];

        if (pekingPartTanggung[pekingCounter - 1] == pekingNotePreviousSpecial)
        {
            for (int i = 0; i < chosenPathet.Length; i++)
            {
                if (chosenPathet[i] == pekingPartTanggung[pekingCounter + 1])
                {
                    if (i != 0 && i != chosenPathet.Length - 1)
                    {
                        pekingNoteUp = chosenPathet[i + 1];
                        pekingNoteDown = chosenPathet[i - 1];
                    }
                    else if (i == 0)
                    {
                        pekingNoteUp = chosenPathet[i + 1];
                        pekingNoteDown = chosenPathet[chosenPathet.Length - 1];
                    }
                    else if (i == chosenPathet.Length - 1)
                    {
                        pekingNoteUp = chosenPathet[0];
                        pekingNoteDown = chosenPathet[i - 1];
                    }
                }
            }

            pekingNotePrevious = pekingPartTanggung[pekingCounter - 1];

            if (pekingNotePrevious == pekingNoteUp)
                ChangePekingNotes(pekingCounter, 2, 1, true, pekingNoteDown);

            else if (pekingNotePrevious == pekingNoteDown)
                ChangePekingNotes(pekingCounter, 2, 1, true, pekingNoteUp);

            else if (pekingNotePrevious != pekingNoteUp && pekingNotePrevious != pekingNoteDown)
            {
                for (int i = 0; i < chosenPathet.Length; i++)
                {
                    if (chosenPathet[i] == pekingNotePrevious)
                        pekingNotePreviousIndex = i;

                    else if (chosenPathet[i] == pekingNoteUp)
                        pekingNoteUpIndex = i;

                    else if (chosenPathet[i] == pekingNoteDown)
                        pekingNoteDownIndex = i;
                }
                ChangePekingNotesBasedOnDifference(pekingNoteUpIndex, pekingNoteDownIndex, pekingNotePreviousIndex, pekingCounter, 2, 1, true, pekingNoteUp, pekingNoteDown);
            }
        }
    }

    pekingNoteUp = '0';
    pekingNoteDown = '0';
    pekingNotePrevious = '0';
    pekingNoteUpIndex = 0;
    pekingNoteDownIndex = 0;
    pekingNotePreviousIndex = 0;

    //Looking at (double) notes 2 and 4 in each gatra - trying to find pairs
    if (pekingCounter % 4 == 0)
    {
        if (pekingPartTanggung[pekingCounter - 1] == pekingPartTanggung[pekingCounter - 3])
        {
            for (int i = 0; i < chosenPathet.Length; i++)
            {
                if (chosenPathet[i] == pekingPartTanggung[pekingCounter - 1])
                {
                    if (i != 0 && i != chosenPathet.Length - 1)
                    {
                        pekingNoteUp = chosenPathet[i + 1];
                        pekingNoteDown = chosenPathet[i - 1];
                    }
                    else if (i == 0)
                    {
                        pekingNoteUp = chosenPathet[i + 1];
                        pekingNoteDown = chosenPathet[chosenPathet.Length - 1];
                    }
                    else if (i == chosenPathet.Length - 1)
                    {
                        pekingNoteUp = chosenPathet[0];
                        pekingNoteDown = chosenPathet[i - 1];
                    }
                }
            }

            if (pekingCounter > 4)
                pekingNotePrevious = pekingPartTanggung[pekingCounter - 5];

            else if (pekingCounter == 4)
                pekingNotePrevious = pekingPartTanggung[pekingPartTanggung.Length - 1];

            if (pekingNotePrevious == pekingNoteUp)
                ChangePekingNotes(pekingCounter, 4, 1, true, pekingNoteDown);

            else if (pekingNotePrevious == pekingNoteDown)
                ChangePekingNotes(pekingCounter, 4, 1, true, pekingNoteUp);

            else if (pekingNotePrevious != pekingNoteUp && pekingNotePrevious != pekingNoteDown)
            {
                for (int i = 0; i < chosenPathet.Length; i++)
                {
                    if (chosenPathet[i] == pekingNotePrevious)
                        pekingNotePreviousIndex = i;

                    else if (chosenPathet[i] == pekingNoteUp)
                        pekingNoteUpIndex = i;

                    else if (chosenPathet[i] == pekingNoteDown)
                        pekingNoteDownIndex = i;
                }
                ChangePekingNotesBasedOnDifference(pekingNoteUpIndex, pekingNoteDownIndex, pekingNotePreviousIndex, pekingCounter, 4, 1, true, pekingNoteUp, pekingNoteDown);
            }
        }
    }

    pekingNoteUp = '0';
    pekingNoteDown = '0';
    pekingNotePrevious = '0';
    pekingNoteUpIndex = 0;
    pekingNoteDownIndex = 0;
    pekingNotePreviousIndex = 0;

    //Looking at the entire gatra to see if the second and third (double) notes are the same (if so, the whole gatra changes to 'anticipate' the seleh).
    if (pekingCounter % 8 == 0 && pekingPartTanggung[pekingCounter - 4] == pekingPartTanggung[pekingCounter - 6])
    {
        for (int i = 0; i < chosenPathet.Length; i++)
        {
            if (chosenPathet[i] == pekingPartTanggung[pekingCounter - 1])
            {
                if (i != 0 && i != chosenPathet.Length - 1)
                {
                    pekingNoteUp = chosenPathet[i + 1];
                    pekingNoteDown = chosenPathet[i - 1];
                }
                else if (i == 0)
                {
                    pekingNoteUp = chosenPathet[i + 1];
                    pekingNoteDown = chosenPathet[chosenPathet.Length - 1];
                }
                else if (i == chosenPathet.Length - 1)
                {
                    pekingNoteUp = chosenPathet[0];
                    pekingNoteDown = chosenPathet[i - 1];
                }
            }
        }
        if (pekingCounter > 8)
            pekingNotePrevious = pekingPartTanggung[pekingCounter - 9];

        else if (pekingCounter == 8)
            pekingNotePrevious = pekingPartTanggung[pekingPartTanggung.Length - 1];

        if (pekingNotePrevious == pekingNoteUp)
            ChangePekingNotes(pekingCounter, 8, 3, true, pekingNoteDown, pekingPartTanggung[pekingCounter - 1]);

        else if (pekingNotePrevious == pekingNoteDown)
            ChangePekingNotes(pekingCounter, 8, 3, true, pekingNoteUp, pekingPartTanggung[pekingCounter - 1]);

        else if (pekingNotePrevious != pekingNoteUp && pekingNotePrevious != pekingNoteDown)
        {
            for (int i = 0; i < chosenPathet.Length; i++)
            {
                if (chosenPathet[i] == pekingNotePrevious)
                    pekingNotePreviousIndex = i;

                else if (chosenPathet[i] == pekingNoteUp)
                    pekingNoteUpIndex = i;

                else if (chosenPathet[i] == pekingNoteDown)
                    pekingNoteDownIndex = i;
            }
            ChangePekingNotesBasedOnDifference(pekingNoteUpIndex, pekingNoteDownIndex, pekingNotePreviousIndex, pekingCounter, 8, 3, true, pekingNoteUp, pekingNoteDown, pekingPartTanggung[pekingCounter - 1]);
        }
    }
}
if (chosenPathet == pathetPelogLima || chosenPathet == pathetPelogNem)
{
    //ADD AN AMENDMENT HERE FOR PEKING: RULES INVOLVING NOTE 4
    //E.G. FOR 5565 5456 WOULD MAKE 6655665533445566, SHOULD INSTEAD MAKE 66556655 55445566 (OVERRIDING THE DOUBLE RULE)
    //THIS WOULDN'T OVERRIDE IF THE BALUNGAN WENT 3->4, BUT WOULD IF THE PEKINGAN MAKES A CHOICE TO GO FOR A LOWER OVER AN UPPER NOTE (OVERRIDE TO 5544)
    //VICE VERSA FOR 4433 WHEN THE BALUNGAN DOESN'T GO 43 (OVERRIDE TO CHOOSE THE LOWER PAIR OF 2233)
}


//SARON AND SLENTHEM GENERATION
//Saron and slenthem play the balungan verbatim.
saronSlenthemPartTanggung = userInputArr;

//KENONG GENERATION
//Kenong plays on the even seleh in tanggung - if the place of the note in sequence is divisible by 8, write it verbatim in the part. Otherwise, write rests (0).
generateCounter = 0;
foreach (char note in userInputArr)
{
    generateCounter++;
    if (generateCounter % 8 == 0)
    {
        if (note == '0')
        {
            if (FindPreviousNonZero(generateCounter - 1) != '0')
                kenongPartTanggung[generateCounter - 1] = FindPreviousNonZero(generateCounter - 1);
            else
                kenongPartTanggung[generateCounter - 1] = userInputArr[noteCounter - 1];
        }
        else
            kenongPartTanggung[generateCounter - 1] = userInputArr[generateCounter - 1];
    }
    else
        kenongPartTanggung[generateCounter - 1] = '0';
}

//KEMPUL GENERATION
//The kempul plays on the odd seleh in tangung (but not the first!) - THIS IS ASSUMING THAT THE GAMELAN HAS A KEMPUL OF EVERY PITCH - WHICH IS BASICALLY NEVER TRUE.
//!!!NEW RULE YET TO BE IMPLEMENTED!!! THE KEMPUL CANNOT PLAY ON SELEH 4
generateCounter = 0;
foreach (char note in userInputArr)
{
    generateCounter++;
    if (generateCounter >= 12 && (generateCounter - 4) % 8 == 0)
    {
        if (note == '0')
        {
            if (FindPreviousNonZero(generateCounter - 1) != '0')
                kempulPartTanggung[generateCounter - 1] = FindPreviousNonZero(generateCounter - 1);
            else
                kempulPartTanggung[generateCounter - 1] = userInputArr[noteCounter - 1];
        }
        else
            kempulPartTanggung[generateCounter - 1] = userInputArr[generateCounter - 1];
    }
    else
        kempulPartTanggung[generateCounter - 1] = '0';
}
kempulPartTanggung[noteCounter - 1] = 'G';

char[] pekingPartDados = new char[noteCounter * 4];
char[] bonangPanerusPartDados = new char[noteCounter * 8];
char[] bonangBarungPartDados = new char[noteCounter * 4];
char[] saronSlenthemPartDados = new char[noteCounter * 2];
char[] kenongPartDados = new char[noteCounter * 2];
char[] kempulPartDados = new char[noteCounter * 2];

//GENERATING EACH PART IN IRAMA DADOS
//BONANG PANERUS - Each eight note pattern occurs twice
for (int i = 0; i < (noteCounter * 8); i++)
{
    if (i < 4)
        bonangPanerusPartDados[i] = bonangPanerusPartTanggung[i];
    else if (i == 11)
    {
        GenerateDadosNotesHighInst(bonangPanerusPartTanggung, bonangPanerusPartDados, i, -7, -4, -3, 0.5F, -4);
        GenerateDadosNotesHighInst(bonangPanerusPartTanggung, bonangPanerusPartDados, i, -3, 0, -3, 0.5F, -4);
    }
    else if ((i - 3) % 16 == 0 && (i + 1) / 2 < (noteCounter * 4))
        GenerateDadosNotesHighInst(bonangPanerusPartTanggung, bonangPanerusPartDados, i, -7, 0, -3, 0.5F, -4);
    else if ((i + 1) % 16 == 0 && (i + 1) / 2 >= (noteCounter * 4))
        GenerateDadosNotesHighInst(bonangPanerusPartTanggung, bonangPanerusPartDados, i, -3, 0, 1, 0.5F, -4);
    else if ((i - 3) % 16 == 8)
        GenerateDadosNotesHighInst(bonangPanerusPartTanggung, bonangPanerusPartDados, i, -7, 0, -3, 0.5F, -8);
}

for (int i = 0; i < (noteCounter * 4); i++)
{
    //BONANG BARUNG - Each four note pattern occurs twice
    if (i < 2)
        bonangBarungPartDados[i] = bonangBarungPartTanggung[i];
    else if (i == 5)
    {
        bonangBarungPartDados[i - 3] = bonangBarungPartTanggung[i - 3];
        bonangBarungPartDados[i - 2] = bonangBarungPartTanggung[i - 4];
        GenerateDadosNotesHighInst(bonangBarungPartTanggung, bonangBarungPartDados, i, -1, 0, -1, 0.5F, -2);
    }
    else if ((i - 1) % 8 == 4 && i > 5 && (i - 1) / 2 < (noteCounter * 2) - 2)
        GenerateDadosNotesHighInst(bonangBarungPartTanggung, bonangBarungPartDados, i, -3, 0, -1, 0.5F, -4);
    else if ((i - 1) % 8 == 4 && (i - 1) / 2 >= (noteCounter * 2) - 2)
    {
        GenerateDadosNotesHighInst(bonangBarungPartDados, bonangBarungPartDados, i, -3, 0, 0, 1, -7);
        bonangBarungPartDados[i + 1] = bonangBarungPartDados[i - 5];
        bonangBarungPartDados[i + 2] = bonangBarungPartDados[1];
    }
    else if ((i - 1) % 8 == 0)
        GenerateDadosNotesHighInst(bonangBarungPartTanggung, bonangBarungPartDados, i, -3, 0, -1, 0.5F, -2);

    //PEKING - Each pair of note pairs (each four notes) occurs twice
    if ((i + 1) % 8 == 4)
        GenerateDadosNotesHighInst(pekingPartTanggung, pekingPartDados, i, -3, 0, 1, 0.5F, -2);
    else if ((i + 1) % 8 == 0)
        GenerateDadosNotesHighInst(pekingPartTanggung, pekingPartDados, i, -3, 0, 1, 0.5F, -4);
}

//SARON/SLENTHEM, KENONG, AND KEMPUL - Add a space before each note
for (int i = 0; i < (noteCounter * 2); i++)
{
    GenerateDadosNotesLowInst(saronSlenthemPartTanggung, saronSlenthemPartDados, i);
    GenerateDadosNotesLowInst(kenongPartTanggung, kenongPartDados, i);
    GenerateDadosNotesLowInst(kempulPartTanggung, kempulPartDados, i);
}
kempulPartDados[(noteCounter * 2) - 1] = 'G';

Console.WriteLine("\nYour parts have been generated! Please select one of the following options:\nenter \"1\" to display all parts in irama tanggung\nenter \"2\" to display all parts in irama dados\nenter \"3\" to display all parts in both iramas\nenter \"4\" to display a selection of parts\nenter \"5\" to transpose your balungan\nenter \"6\" to exit the application\n");

bool exit = false;

do
{
    string userIrama = "";
    bool[,] chosenParts = { { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true } };
    string? menuInput = Console.ReadLine();
    if (menuInput != null)
    {
        menuInput = menuInput.Trim();
        switch (menuInput)
        {
            case "1":
                userIrama = "tanggung";
                break;
            case "2":
                userIrama = "dados";
                break;
            case "3":
                userIrama = "both";
                break;
            case "4":
                userIrama = "both";
                chosenParts = GetUserParts();
                break;
            case "5":
                if (chosenPathet != pathetPelogLima && chosenPathet != pathetPelogNem)
                {
                    TransposeBalungan();
                    userLaras = UpdateUserLaras();
                    userPathet = UpdateUserPathet();
                    Console.WriteLine($"Your balungan has been transposed to laras {userLaras} pathet {userPathet}.");
                }
                else
                    Console.WriteLine($"Sorry but this option is currently only available for balungan in laras slendro pathet manyura/sanga, and laras pelog pathet barang.");
                break;
            case "6":
                exit = true;
                break;
        }
    }

    if (userIrama == "tanggung" || userIrama == "both")
    {
        Console.WriteLine("\nDisplaying parts in irama tanggung\n");

        if (chosenParts[0, 0])
        {
            Console.WriteLine("\n\nBonang Panerus:");
            Console.Write($" ({userInputArr[noteCounter - 1]}{bonangPanerusPartTanggung[1]}{bonangPanerusPartTanggung[2]}{bonangPanerusPartTanggung[3]})");
            Console.Write("\t");
            DisplayPart(bonangPanerusPartTanggung, 16);
        }

        if (chosenParts[1, 0])
        {
            Console.WriteLine("\n\nBonang Barung:");
            if (userInputArr[0] != '0')
                Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[0]})");
            else if (userInputArr[0] == '0')
                Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[noteCounter - 1]})");
            Console.Write("\t");
            DisplayPart(bonangBarungPartTanggung, 8);
        }

        if (chosenParts[2, 0])
        {
            Console.WriteLine("\n\nPeking:");
            Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[noteCounter - 1]}) ");
            DisplayPart(pekingPartTanggung, 8);
        }

        if (chosenParts[3, 0])
        {
            Console.WriteLine("\n\nSaron and Slenthem:");
            Console.Write($"    ({userInputArr[noteCounter - 1]}) ");
            DisplayPart(saronSlenthemPartTanggung, 4);
        }

        if (chosenParts[4, 0])
        {
            Console.WriteLine("\n\nKenong:");
            Console.Write($"    ({userInputArr[noteCounter - 1]}) ");
            DisplayPart(kenongPartTanggung, 4);
        }

        if (chosenParts[5, 0])
        {
            Console.WriteLine("\n\nKempul:");
            Console.Write($" (Gong) ");
            DisplayPart(kempulPartTanggung, 4);
        }
    }

    if (userIrama == "dados" || userIrama == "both")
    {
        Console.WriteLine("\n\n\nDisplaying parts in irama dados\n");

        if (chosenParts[0, 1])
        {
            Console.WriteLine("\n\nBonang Panerus:");
            Console.Write($" ({userInputArr[noteCounter - 1]}{bonangPanerusPartDados[1]}{bonangPanerusPartDados[2]}{bonangPanerusPartDados[3]})");
            Console.Write("\t");
            DisplayPart(bonangPanerusPartDados, 16);
        }

        if (chosenParts[1, 1])
        {
            Console.WriteLine("\n\nBonang Barung:");
            if (userInputArr[0] != '0')
                Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[0]})");
            else if (userInputArr[0] == '0')
                Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[noteCounter - 1]})");
            Console.Write("\t");
            DisplayPart(bonangBarungPartDados, 8);
        }

        if (chosenParts[2, 1])
        {
            Console.WriteLine("\n\nPeking:");
            Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[noteCounter - 1]}) ");
            DisplayPart(pekingPartDados, 8);
        }

        if (chosenParts[3, 1])
        {
            Console.WriteLine("\n\nSaron and Slenthem:");
            Console.Write($"    ({userInputArr[noteCounter - 1]}) ");
            DisplayPart(saronSlenthemPartDados, 4);
        }

        if (chosenParts[4, 1])
        {
            Console.WriteLine("\n\nKenong:");
            Console.Write($"    ({userInputArr[noteCounter - 1]}) ");
            DisplayPart(kenongPartDados, 4);
        }

        if (chosenParts[5, 1])
        {
            Console.WriteLine("\n\nKempul:");
            Console.Write($" (Gong) ");
            DisplayPart(kempulPartDados, 4);
        }

        Console.WriteLine("\n\nParts displayed in irama dados are written in the same time-frame as tanggung to illustrate the difference between the two - each four-note gatra takes up eight notes of space (two gatras).\n");
    }

    Console.WriteLine($"\n\nYour balungan (in laras {userLaras} pathet {userPathet}):\n");
    DisplayBalungan(userInputArr);
    Console.WriteLine("\n\nPlease select one of the following options:\nenter \"1\" to display all parts in irama tanggung\nenter \"2\" to display all parts in irama dados\nenter \"3\" to display all parts in both iramas\nenter \"4\" to display a selection of parts\nenter \"5\" to transpose your balungan\nenter \"6\" to exit the application\n");
} while (!exit);

Console.WriteLine("\n\nSome considerations: Peking ...(different styles of playing, note above/below rule on double notes/rests)");
Console.WriteLine("Bonang...(flowery phrases/octaves, notes in brackets at the start = same as at end)");
Console.WriteLine("Kempul...(other pitches, usually closely related ones)");
Console.WriteLine("Displayed notation for dados... (representative compared with tanggung)");
Console.WriteLine("Some of the rules in pelog, particularly around note 4, are not present.");
Console.WriteLine("Note - the instruments interpret the balungan as a ladrang, regardless of the length.");

void DefineKempulNotes()
{
    Console.WriteLine("Under Construction");
}

string GetUserLaras(string input)
{
    if (input == "1")
    {
        chosenLaras = larasSlendro;
        return "slendro";
    }
    else if (input == "2")
    {
        chosenLaras = larasPelog;
        return "pelog";
    }
    else
        throw new ArgumentException($"Sorry but \"{input}\" is not a valid option. Please choose the laras of your balungan: enter \"1\" for slendro or \"2\" for pelog.\n");
}

void DisplayPathetOptions(string laras)
{
    if (laras == "slendro")
        Console.WriteLine("(Slendro: enter \"1\" for manyura, \"2\" for sanga, or \"3\" for nem)\n");

    else if (laras == "pelog")
        Console.WriteLine("(Pelog: enter \"1\" for barang, \"2\" for nem, or \"3\" for lima)\n");
}

string GetUserPathet(string input)
{
    if (userLaras == "slendro")
    {
        try
        {
            string result = CheckSlendroPathet(input);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
    else
    {
        try
        {
            string result = CheckPelogPathet(input);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

string CheckSlendroPathet(string input)
{
    switch (input)
    {
        case "1":
            chosenPathet = pathetSlendroManyura;
            return "manyura";
        case "2":
            chosenPathet = pathetSlendroSanga;
            return "sanga";
        case "3":
            chosenPathet = pathetSlendroNem;
            return "nem";
        default:
            throw new ArgumentException($"Sorry but \"{input}\" is not a valid option. Please choose a valid pathet.");
    }
}

string CheckPelogPathet(string input)
{
    switch (input)
    {
        case "1":
            chosenPathet = pathetPelogBarang;
            return "barang";
        case "2":
            chosenPathet = pathetPelogNem;
            return "nem";
        case "3":
            chosenPathet = pathetPelogLima;
            return "lima";
        default:
            throw new ArgumentException($"Sorry but \"{input}\" is not a valid option. Please choose a valid pathet.");
    }
}

bool CheckNotesValid(char[] balungan, char[] laras, char[] pathet)
{
    try
    {
        CheckCharactersValid(balungan);
    }
    catch (Exception)
    {
        throw;
    }

    try
    {
        CheckPathetValid(balungan, pathet);
    }
    catch (Exception)
    {
        throw;
    }
    return true;
}

void CheckCharactersValid(char[] balungan)
{
    foreach (char ch in userInputArr)
    {
        if (ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9' && ch != '0')
        {
            throw new ArgumentException($"Please only enter numbers and spaces.\"{ch}\" is not a number or space.\n");
        }
    }
    return;
}

void CheckPathetValid(char[] balungan, char[] pathet)
{
    foreach (char ch in userInputArr)
    {
        if (ch != '0')
        {
            if (!chosenPathet.Contains(ch))
                throw new FormatException($"\"{ch}\" is not a valid note for {userLaras} {userPathet}. Please enter a balungan in {userLaras} {userPathet}.\nValid notes:");
        }
    }
    return;
}

bool CheckLengthAndSeleh(char[] balungan, char[] pathet, int noteAmount, bool checkSelehTrue = false)
{
    try
    {
        CheckLength(noteAmount);
    }
    catch (Exception)
    {
        throw;
    }

    if (checkSelehTrue)
    {
        try
        {
            CheckSeleh(balungan, pathet, noteAmount);
        }
        catch (Exception)
        {
            throw;
        }
    }
    return true;
}

void CheckLength(int noteAmount)
{
    if (noteAmount != 16 && noteAmount != 32 && noteAmount != 64)
        throw new ArgumentException($"Sorry but you have not used the specified amount of notes/gatra. You have used {noteCounter} notes and {userTotalGatras} whole gatra of 4 notes each.\nPlease enter a balungan with a length of either 4, 8, or 16 full gatra of 4 notes each (using \"0\" as a rest).\n");
    return;
}

void CheckSeleh(char[] balungan, char[] pathet, int noteAmount)
{
    if (balungan[noteAmount - 1] != pathet[0])
        throw new ArgumentException($"Your final seleh is the note \"{userInputArr[noteCounter - 1]}\". For {userLaras} {userPathet}, Please enter a balungan where the final note is {chosenPathet[0]}.\n");
    return;
}

void NoteFourWarning(char[] balungan, char[] pathet)
{
    if (CheckNoteFour(balungan, pathet))
        Console.WriteLine("Warning - the pelog note 4 is used in this balungan. Currently this program may produce results that do not reflect proper garap involving note 4, especially when used as a seleh.");
}

bool CheckNoteFour(char[] balungan, char[] pathet)
{
    if (pathet.Contains('4') && balungan.Contains('4'))
        return true;
    return false;
}

void DisplayBalungan(char[] balungan)
{
    foreach (char ch in balungan)
    {
        displayCounter++;
        if (ch != '0')
            Console.Write(ch);
        else
            Console.Write("-");
        if ((displayCounter) % 4 == 0)
            Console.Write(" ");
    }
}

char FindPreviousNonZero(int initialIncrementValue) //CHANGE THIS TO FIND PREVIOUS NON-ZERO FOR ANY PART?
{
    for (int j = initialIncrementValue; j >= 0; j--)
    {
        if (userInputArr[j] != '0')
            return userInputArr[j];
    }
    return '0';
}

void GenerateBonangPanerusNotesGroup(int generateCounter, char outNotes2and4, char outNote3, char outNote1 = '0')
{
    if (generateCounter >= 4)
        GenerateBonangPanerusNotes(generateCounter, 4, -12, -5, outNotes2and4, outNote3, outNote1);

    if (generateCounter == 2)
        GenerateBonangPanerusNotes(generateCounter, 2, -4, -1, outNotes2and4, outNote3, outNote1);
}

void GenerateBonangPanerusNotes(int generateCounter, int generateCounterMultiplier, int incrementLowerBound, int incrementUpperBound, char outNotes2and4, char outNote3, char outNote1 = '0')
{
    int panerusGenCounter = 0;
    for (int j = incrementLowerBound; j <= incrementUpperBound; j++)
    {
        panerusGenCounter++;
        if (panerusGenCounter % 4 == 1)
            bonangPanerusPartTanggung[(generateCounter * generateCounterMultiplier) + j] = outNote1;

        if (panerusGenCounter % 4 == 2 || panerusGenCounter % 4 == 0)
            bonangPanerusPartTanggung[(generateCounter * generateCounterMultiplier) + j] = outNotes2and4;

        if (panerusGenCounter % 4 == 3)
            bonangPanerusPartTanggung[(generateCounter * generateCounterMultiplier) + j] = outNote3;
    }
}

void GenerateBonangBarungNotesGroup(int generateCounter, char outNotes2and4, char outNote3, char outNote1 = '0')
{
    if (generateCounter > 2)
        GenerateBonangBarungNotes(generateCounter, 2, -6, -3, outNotes2and4, outNote3, outNote1);

    if (generateCounter == 2)
        GenerateBonangBarungNotes(generateCounter, 1, -2, -1, outNotes2and4, outNote3, outNote1);
}

void GenerateBonangBarungNotes(int generateCounter, int generateCounterMultiplier, int incrementLowerBound, int incrementUpperBound, char outNotes2and4, char outNote3, char outNote1 = '0')
{
    int barungGenCounter = 0;
    for (int j = incrementLowerBound; j <= incrementUpperBound; j++)
    {
        barungGenCounter++;
        if (incrementUpperBound - incrementLowerBound == 1)
        {
            if (barungGenCounter == 1)
                bonangBarungPartTanggung[(generateCounter * generateCounterMultiplier) + j] = outNote3;

            if (barungGenCounter == 2)
                bonangBarungPartTanggung[(generateCounter * generateCounterMultiplier) + j] = outNotes2and4;
        }

        else
        {
            if (barungGenCounter % 4 == 1)
                bonangBarungPartTanggung[(generateCounter * generateCounterMultiplier) + j] = outNote1;

            if (barungGenCounter % 4 == 2 || barungGenCounter % 4 == 0)
                bonangBarungPartTanggung[(generateCounter * generateCounterMultiplier) + j] = outNotes2and4;

            if (barungGenCounter % 4 == 3)
                bonangBarungPartTanggung[(generateCounter * generateCounterMultiplier) + j] = outNote3;
        }
    }
}

void ChangePekingNotes(int pekingCounter, int howManyNotesBack, int howManyPairs, bool firstPairOdd, char outNoteOdd, char outNoteEven = '0')
{
    //Only use for up to three pairs
    int x = 0;
    if (!firstPairOdd)
        x = 2;

    for (int j = -1 - howManyNotesBack; j < ((howManyPairs * 2) - howManyNotesBack); j++)
    {
        x++;
        if (x % 2 == 0) //(for each pair of notes)
        {
            if ((x / 2) % 2 == 1) //(if the pair is an odd pair)
            {
                pekingPartTanggung[pekingCounter + j] = outNoteOdd;
                pekingPartTanggung[pekingCounter + j + 1] = outNoteOdd;
            }
            else if ((x / 2) % 2 == 0) //(if the pair is an even pair)
            {
                pekingPartTanggung[pekingCounter + j] = outNoteEven;
                pekingPartTanggung[pekingCounter + j + 1] = outNoteEven;
            }
        }
    }
}

void ChangePekingNotesBasedOnDifference(int pekingNoteUpIndex, int pekingNoteDownIndex, int pekingNotePreviousIndex, int pekingCounter, int howManyNotesBack, int howManyPairs, bool firstPairOdd, char pekingNoteUp, char pekingNoteDown, char outNoteEven = '0')
{
    int pekingNoteUpPreviousIndexDifference = Math.Abs(pekingNoteUpIndex - pekingNotePreviousIndex);
    int pekingNoteDownPreviousIndexDifference = Math.Abs(pekingNoteDownIndex - pekingNotePreviousIndex);

    if (pekingNoteUpPreviousIndexDifference > pekingNoteDownPreviousIndexDifference)
        ChangePekingNotes(pekingCounter, howManyNotesBack, howManyPairs, firstPairOdd, pekingNoteDown, outNoteEven);

    if (pekingNoteUpPreviousIndexDifference < pekingNoteDownPreviousIndexDifference || pekingNoteUpPreviousIndexDifference == pekingNoteDownPreviousIndexDifference)
        ChangePekingNotes(pekingCounter, howManyNotesBack, howManyPairs, firstPairOdd, pekingNoteUp, outNoteEven);
}

void GenerateDadosNotesHighInst(char[] partTanggung, char[] partDados, int index, int dadosIncrementLowerBound, int dadosIncrementUpperBound, int tanggungIndexModifier, float tanggungIndexMultiplier, int tanggungInitialModifier)
{
    int counter = 0;
    for (int j = dadosIncrementLowerBound; j <= dadosIncrementUpperBound; j++)
    {
        partDados[index + j] = partTanggung[(int)((index + tanggungIndexModifier) * tanggungIndexMultiplier) + tanggungInitialModifier + counter];
        counter++;
    }
}

void GenerateDadosNotesLowInst(char[] partTanggung, char[] partDados, int index)
{
    if ((index + 1) % 2 == 1)
        partDados[index] = '0';
    else if ((index + 1) % 2 == 0)
        partDados[index] = partTanggung[((index + 1) / 2) - 1];
}

bool[,] GetUserParts()
{
    int[] userPartsTanggung = GetUserPartsInput("tanggung");
    int[] userPartsDados = GetUserPartsInput("dados");
    bool[,] userPartsOut = { { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true } };

    for (int j = 1; j < 7; j++)
    {
        if (!userPartsTanggung.Contains(j))
            userPartsOut[j - 1, 0] = false;
        if (!userPartsDados.Contains(j))
            userPartsOut[j - 1, 1] = false;
    }
    return userPartsOut;
}

int[] GetUserPartsInput(string irama)
{
    int[] userPartsArray;
    Console.WriteLine($"Please enter any number of specific parts you would like to be displayed for irama {irama} (please enter a string of numbers):\nenter \"1\" for bonang panerus\nenter \"2\" for bonang barung\nenter \"3\" for peking\nenter \"4\" for saron/slenthem\nenter \"5\" for kenong\nenter \"6\" for kempul\n");
    do
    {
        string? userParts = Console.ReadLine();
        if (userParts != null)
        {
            userParts = userParts.Trim().Replace(" ", "");
            if (int.TryParse(userParts, out int result))
            {
                userPartsArray = result.ToString().Select(o => Convert.ToInt32(o) - 48).ToArray();
                return userPartsArray;
            }
        }
        //Could be changed to an exception in the future
        Console.WriteLine($"Invalid entry. Please enter any number of specific parts you would like to be displayed for irama {irama} (please enter a string of numbers):\nenter \"1\" for bonang panerus\nenter \"2\" for bonang barung\nenter \"3\" for peking\nenter \"4\" for saron/slenthem\nenter \"5\" for kenong\nenter \"6\" for kempul\n");
    } while (true);
}

void TransposeBalungan()
{
    Console.WriteLine("Please select which laras/pathet you wish to transpose your balungan and parts to:\nenter \"1\" for slendro manyura\nenter \"2\" for slendro sanga\nenter \"3\" for pelog barang\nenter \"4\" for pelog nem/lima (under construction - dev note: which? one of them might be ok)");
    do
    {
        string? transInput = Console.ReadLine();
        if (transInput != null)
        {
            transInput = transInput.Trim();
            switch (transInput)
            {
                case "1":
                    if (chosenPathet != pathetSlendroManyura)
                    {
                        TransposeAllParts(chosenPathet, pathetSlendroManyura);
                        chosenLaras = larasSlendro;
                        chosenPathet = pathetSlendroManyura;
                        return;
                    }
                    else
                        Console.WriteLine($"Your balungan is already in pathet {userPathet}!\n");
                    return;
                case "2":
                    if (chosenPathet != pathetSlendroSanga)
                    {
                        TransposeAllParts(chosenPathet, pathetSlendroSanga);
                        chosenLaras = larasSlendro;
                        chosenPathet = pathetSlendroSanga;
                        return;
                    }
                    else
                        Console.WriteLine($"Your balungan is already in pathet {userPathet}!\n");
                    return;
                case "3":
                    if (chosenPathet != pathetPelogBarang)
                    {
                        TransposeAllParts(chosenPathet, pathetPelogBarang);
                        chosenLaras = larasPelog;
                        chosenPathet = pathetPelogBarang;
                        return;
                    }
                    else
                        Console.WriteLine($"Your balungan is already in pathet {userPathet}!\n");
                    return;
                case "4":
                    Console.WriteLine("Under construction. Your balungan has not been transposed.");
                    return;
                default:
                    Console.WriteLine("Invalid entry.");
                    break;
            }
        }
        Console.WriteLine("Please select which laras/pathet you wish to transpose your balungan and parts to:\nenter \"1\" for slendro manyura\nenter \"2\" for slendro sanga\nenter \"3\" for pelog barang\nenter \"4\" for pelog nem(OR LIMA?? WHICH)");
    } while (true);
}

void TransposeAllParts(char[] pathetFrom, char[] pathetTo)
{
    userInputArr = Transpose(userInputArr, pathetFrom, pathetTo);
    bonangPanerusPartTanggung = Transpose(bonangPanerusPartTanggung, pathetFrom, pathetTo);
    bonangPanerusPartDados = Transpose(bonangPanerusPartDados, pathetFrom, pathetTo);
    bonangBarungPartTanggung = Transpose(bonangBarungPartTanggung, pathetFrom, pathetTo);
    bonangBarungPartDados = Transpose(bonangBarungPartDados, pathetFrom, pathetTo);
    pekingPartTanggung = Transpose(pekingPartTanggung, pathetFrom, pathetTo);
    pekingPartDados = Transpose(pekingPartDados, pathetFrom, pathetTo);
    //saronSlenthemPartTanggung = Transpose(saronSlenthemPartTanggung, pathetFrom, pathetTo); ***
    saronSlenthemPartDados = Transpose(saronSlenthemPartDados, pathetFrom, pathetTo);
    kenongPartTanggung = Transpose(kenongPartTanggung, pathetFrom, pathetTo);
    kenongPartDados = Transpose(kenongPartDados, pathetFrom, pathetTo);
    kempulPartTanggung = Transpose(kempulPartTanggung, pathetFrom, pathetTo);
    kempulPartDados = Transpose(kempulPartDados, pathetFrom, pathetTo);
    //***Since saronSlenthemPartTanggung = userInputArr (during tanggung generation), they seem to be linked (to do with arrays being ref values?)
    //***transposing both down means they both go down 2 times which causes a bug - commenting the saron part out seems to fix
}

char[] Transpose(char[] part, char[] pathetFrom, char[] pathetTo)
{
    char[] transPart = part;
    if (pathetFrom == pathetPelogLima || pathetFrom == pathetPelogNem || pathetTo == pathetPelogLima || pathetTo == pathetPelogNem)
    {
        Console.WriteLine("Under Construction, N/A");
        return part;
    }
    for (int j = 0; j < part.Length; j++)
    {
        for (int k = 0; k < pathetFrom.Length; k++)
        {
            if (part[j] == pathetFrom[k])
            {
                transPart[j] = pathetTo[k];
                break;
            }
        }
    }
    return transPart;
}

string UpdateUserLaras()
{
    if (chosenLaras == larasSlendro)
        return "slendro";
    else
        return "pelog";
    //exception handling can be implemented
}

string UpdateUserPathet()
{
    if (chosenPathet == pathetSlendroManyura)
        return "manyura";
    else if (chosenPathet == pathetSlendroSanga)
        return "sanga";
    else if (chosenPathet == pathetSlendroNem)
        return "nem";
    else if (chosenPathet == pathetPelogBarang)
        return "barang";
    else if (chosenPathet == pathetPelogNem)
        return "nem";
    else
        return "lima";
    //exception handling can be implemented
}

void DisplayPart(char[] part, int notesPerGatra)
{
    displayCounter = 0;
    foreach (char note in part)
    {
        if (note == '0')
            Console.Write("-");
        else
            Console.Write(note);

        displayCounter++;
        if (displayCounter >= 128 && (displayCounter) % 128 == 0 && displayCounter != part.Length)
            Console.Write("\n\t");
        else if ((displayCounter) % notesPerGatra == 0)
            Console.Write(" ");
    }
}