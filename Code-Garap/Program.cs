using System;

//TEST BALUNGAN:
// LADRANG PANKUR - SLENDRO MANYURA (CAN BE PUT IN OTHER PATHET/LARAS) - 3231 3216 1632 5321 3532 6532 5321 3216
// GAMBIR SAWIT - SLENDRO SANGA - 0352 0356 2200 2321 0032 0126 2200 2321 0032 0165 0056 1653 0023 5321 6532 0165

//TODO
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

string userLaras = "initialise";
bool userLarasValid = false;

Console.WriteLine("\nHello! This program generates a basic literal representation of some of the Javanese Gamelan parts for a balungan entered by the user.\n(Disclaimer - do not use these parts as a substitute for garap - this is only a demonstration, and definitely not a replacement!)\n\n");
Console.WriteLine("Firstly, please choose the laras of your balungan: enter \"1\" for slendro or \"2\" for pelog.\n");

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
    Console.WriteLine($"Please enter a 4, 8, or 16 gatra balungan in {userLaras} {userPathet}.\nYou may use spaces between gatra or anywhere you like. Please use \"-\" or \"0\" to depict a rest.");
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
                userInputValid = CheckLengthAndSeleh(userInputArr, chosenPathet, noteCounter);
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
int[] chosenParts = { 1, 1, 1, 1, 1, 1 }; //maybe make this string/2 dimensional to include instrument names?
bool userExit = false;
//testing stuff: set the below to true to use the menu system
bool menuActive = false;

if (menuActive)
{
    do
    {
        Console.WriteLine("Please select one of the following options to customise the output:");
        string? userOption = Console.ReadLine();
        if (userOption != null)
        {
            switch (userOption)
            {
                case "1":
                    Console.WriteLine("Sorry, but this function is unfinished. Irama tanggung is the default option.");
                    //userIrama = GetUserIrama(); this will be the same code as the original irama stuff
                    break;
                case "2":
                    GetUserParts();
                    //USE chosenParts[] TO STORE USER INPUT AND DETERMINE WHICH PARTS ARE TO BE GENERATED
                    //PRINT ALL PARTS BY DEFAULT
                    //FOR PRINTING PARTS: PUT THE NAMES OF THE PARTS IN AN ARRAY AND DO A FOR LOOP OF THE METHOD SOMEHOW?
                    break;
                case "3":
                    ChangeKempulNotes();
                    //USE ARRAYS AVAILABLE KEMPUL NOTES LIKE THE LARAS ARRAYS (ONE FOR SLENDRO AND ONE FOR PELOG) AND ASK THE USER TO LIST ALL THE NOTES THEY WANT
                    //(ALL AVAILABLE BY DEFAULT EXCEPT PELOG 4, ALLOW USER TO ENTER THE RANGE AVAILABLE ACCORDING TO THE LARAS OF THEIR BALUNGAN)
                    break;
                case "4":
                    if (chosenPathet[0] != '2' && chosenPathet[0] != '5' && CheckNoteFour(userInputArr, chosenPathet))
                    {
                        TransposeBalungan(userInputArr); //THIS ISN'T DONE YET
                                                         //userLaras, chosenLaras, userPathet and chosenPathet ALL need to change! 
                                                         //Have specific methods to change the strings according to TransposeBalungan
                                                         //e.g. ChangeUserLaras(); and ChangeUserPathet(); which output strings? (would this work scoped in the dowhile loop?)
                        Console.WriteLine($"Displayed below is your balungan, now transposed to laras {userLaras} pathet {userPathet}.");
                        DisplayBalungan(userInputArr);
                    }
                    else
                    {
                        Console.WriteLine($"Sorry but this option is currently only available for balungan in pathet slendro manyura, slendro sanga, pelog barang, and pelog nem while note 4 is absent.");
                    }
                    break;
                case "5":
                    //ASK DAN HOW TO DO THIS/TO PUT GENERATION IN A SEPARATE FILE
                    Console.WriteLine("For now, use option 6 to do this (which will also generate the parts).");
                    break;
                case "6":
                    userExit = true;
                    break;

            }
        }
    } while (!userExit);
}


string userIrama = "tanggung";
bool userIramaValid = false;

Console.WriteLine("\nNow, please choose the irama you would like the application to generate the parts in: enter \"1\" for irama tanggung or \"2\" for irama dados.\n");

do
{
    string? iramaInput = Console.ReadLine();
    if (iramaInput != null)
    {
        iramaInput = iramaInput.Trim();
        try
        {
            userIrama = GetUserIrama(iramaInput);
            userIramaValid = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    else
    {
        Console.WriteLine("\nPlease choose the irama you would like the application to generate the parts in: enter \"1\" for irama tanggung or \"2\" for irama dados.\n");
    }
} while (userIramaValid == false);
Console.WriteLine($"You have chosen irama {userIrama}.\n");

Console.WriteLine($"The output below contains generated representative parts for your balungan in laras {userLaras} pathet {userPathet}, and in irama {userIrama}:\n");
DisplayBalungan(userInputArr);

Console.WriteLine($"\n\nNote - the instruments interpret the balungan as a ladrang, regardless of the length.\n");


char[] pekingPartTanggung = new char[noteCounter * 2];
char[] bonangPanerusPartTanggung = new char[noteCounter * 4];
char[] bonangBarungPartTanggung = new char[noteCounter * 2];
char[] saronSlenthemPartTanggung = new char[noteCounter];
char[] kenongPartTanggung = new char[noteCounter];
char[] kempulPartTanggung = new char[noteCounter];

//GENERATING AND DISPLAYING EACH PART IN TANGGUNG

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
    bonangBarungPartTanggung[(generateCounter * 2) - 1] = chosenPathet[0];

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
//the kempul plays on the odd seleh in tangung (but not the first!) - THIS IS ASSUMING THAT THE GAMELAN HAS A KEMPUL OF EVERY PITCH - WHICH IS BASICALLY NEVER TRUE.
//!!!NEW RULE!!! THE KEMPUL CANNOT PLAY ON SELEH 4
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

//IMPORTANT: PEKINGAN IN DADOS REPEATS EACH PAIR TWICE - ARE THERE EXCEPTIONS?
//SARON/KENONG/KEMPUL ARE EASY (add a space before each note)
//BONANG AND PEKING ARE ALSO FAIRLY EASY (double each pattern/pair)
//TO GENERATE, USE THE TANGGUNG PARTS
//GENERATING AND DISPLAYING EACH PART IN DADOS - UNDER CONSTRUCTION!!!

char[] pekingPartDados = new char[noteCounter * 4];
char[] bonangPanerusPartDados = new char[noteCounter * 8];
char[] bonangBarungPartDados = new char[noteCounter * 4];
char[] saronSlenthemPartDados = new char[noteCounter * 2];
char[] kenongPartDados = new char[noteCounter * 2];
char[] kempulPartDados = new char[noteCounter * 2];

//DADOS GENERATION GOES HERE
//BONANG - PRINT EACH FOUR/EIGHT NOTES TWICE
//PANERUS

//BARUNG
for (int i = 0; i < (noteCounter * 4); i++)
{
    if (i < 2)
        bonangBarungPartDados[i] = bonangBarungPartTanggung[i];
    else if (i == 5)
    {
        GenerateDadosNotesHighInst(bonangBarungPartTanggung, bonangBarungPartDados, i, -3, -2, -1, 0.5F, 0);
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
}
//PEKING - PRINT EACH PAIR TWICE (repeat each four notes twice)
for (int i = 0; i < (noteCounter * 4); i++)
{
    if ((i + 1) % 8 == 4)
        GenerateDadosNotesHighInst(pekingPartTanggung, pekingPartDados, i, -3, 0, 1, 0.5F, -2);

    else if ((i + 1) % 8 == 0)
        GenerateDadosNotesHighInst(pekingPartTanggung, pekingPartDados, i, -3, 0, 1, 0.5F, -4);
}
//SARON/SLENTHEM, KENONG, KEMPUL - ADD A SPACE BEFORE EACH NOTE
for (int i = 0; i < (noteCounter * 2); i++)
{
    GenerateDadosNotesLowInst(saronSlenthemPartTanggung, saronSlenthemPartDados, i);

    GenerateDadosNotesLowInst(kenongPartTanggung, kenongPartDados, i);

    GenerateDadosNotesLowInst(kempulPartTanggung, kempulPartDados, i);
}

//NEW: HAVE A MESSAGE HERE THAT SAYS: YOUR PARTS WERE GENERATED - SELECT WHICH PARTS YOU WANT TO SEE

if (userIrama == "tanggung")
{
    Console.WriteLine("\n\nBonang Panerus:");
    Console.Write($" ({userInputArr[noteCounter - 1]}{bonangPanerusPartTanggung[1]}{bonangPanerusPartTanggung[2]}{bonangPanerusPartTanggung[1]})");
    Console.Write("\t");
    DisplayPart(bonangPanerusPartTanggung, 16);

    Console.WriteLine("\n\nBonang Barung:");
    if (userInputArr[0] != '0')
        Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[0]})");
    else if (userInputArr[0] == '0')
        Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[noteCounter - 1]})");
    Console.Write("\t");
    DisplayPart(bonangBarungPartTanggung, 8);

    Console.WriteLine("\n\nPeking:");
    Console.Write($"   ({userInputArr[noteCounter - 1]}{userInputArr[noteCounter - 1]}) ");
    DisplayPart(pekingPartTanggung, 8);

    Console.WriteLine("\n\nSaron and Slenthem:");
    Console.Write($"    ({userInputArr[noteCounter - 1]}) ");
    DisplayPart(saronSlenthemPartTanggung, 4);

    Console.WriteLine("\n\nKenong:");
    Console.Write($"    ({userInputArr[noteCounter - 1]}) ");
    DisplayPart(kenongPartTanggung, 4);

    Console.WriteLine("\n\nKempul:");
    Console.Write($" (gong) ");
    DisplayPart(kempulPartTanggung, 4);
}

//DADOS DISPLAY GOES HERE
if (userIrama == "dados")
{
    Console.WriteLine("(The parts displayed for irama dados are written in the same time-frame as tanggung to illustrate the difference between the two.)\n");
    Console.WriteLine("Dados is under construction. Please only use irama tanggung for now.\n");
}

Console.WriteLine("\n\nSome considerations: Peking ...(different styles of playing, note above/below rule on double notes/rests)");
Console.WriteLine("Bonang...(flowery phrases/octaves, notes in brackets at the start = same as at end)");
Console.WriteLine("Kempul...(other pitches, usually closely related ones)");
Console.WriteLine("Displayed notation for dados... (representative compared with tanggung)");
Console.WriteLine("Some of the rules in pelog, particularly around note 4, are not present.");


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

bool CheckLengthAndSeleh(char[] balungan, char[] pathet, int noteAmount)
{
    try
    {
        CheckLength(noteAmount);
    }
    catch (Exception)
    {
        throw;
    }

    try
    {
        CheckSeleh(balungan, pathet, noteAmount);
    }
    catch (Exception)
    {
        throw;
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

void GetUserParts()
{
    Console.WriteLine("Under Construction");
}

void ChangeKempulNotes()
{
    Console.WriteLine("Under Construction");
}

string GetUserIrama(string input)
{
    switch (input)
    {
        case "1":
            return "tanggung";
        case "2":
            return "dados";
        default:
            throw new ArgumentException($"Sorry but \"{input}\" is not a valid option. Please choose a valid irama: enter \"1\" for irama tanggung or \"2\" for irama dados.\n");
    }
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

void TransposeBalungan(char[] balungan)
{
    Console.WriteLine("UNDER CONSTRUCTION");
    //(OPTIONS: MANYURA/SANGA/BARANG/NEM(IGNORING NOTE 4/NOT AVAILABLE IF SO) - FIRST CHECK IF THE USER BALUNGAN IS VIABLE)
}

//NOTE THAT FOR NOW THESE METHODS AND THEIR NAMES ONLY APPLY TO IRAMA TANGGUNG

char FindPreviousNonZero(int initialIncrementValue)
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