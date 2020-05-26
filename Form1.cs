using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using NAudio.Wave;
using System.Collections.Generic;
using Yggdrasil.Properties;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Yggdrasil
{
    public partial class Yggdrasil : Form
    {
        #region Var Area
        public Form2 form2 = new Form2();
        public Form3 form3 = new Form3();
        Button[] decisionBtns;
        Button[] charBtns;
        dynamic[] chars;
        Story curStory;
        Character myChar;
        SaveFile mySave;
        Color light = new Color();
        Color decisionColor = new Color();
        Color outline = new Color();
        Color fade = new Color();
        Color dfade = new Color();
        Font main = new Font("Book Antiqua", 15.75f);
        List<int> storyIds = new List<int>();
        List<int> unlockedChars = new List<int>();
        public static List<int> achievements = new List<int>();
        bool mute = false;
        int volume = 90;
        int lr, lg, lb, dr, dg, db;
        int myDecision;
        int area = 666;

        int specialStoryCounter = 0;

        #region Audio
        /* -- Audio -- */
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private KeyHandler ghk;
        string audioPath;
        /* -- Audio End -- */
        #endregion

        #endregion

        public Yggdrasil()
        {           
            InitializeComponent();
            ghk = new KeyHandler(Keys.Escape, this);
            ghk.Register();
        }

        protected override CreateParams CreateParams //prevents flickering of form
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public void loadStory(int m) //loads story text and generates decision buttons
        {
            Console.WriteLine(curStory.ID); //for DEBUG version only

            if (m == 0) storyIds.Add(curStory.ID);
            if (area != curStory.area)
            {
                area = curStory.area;
                switch (area)
                {
                    case 0:
                        audioPath = "files/sound/125.mp3";
                        changeBackground(Resources.background_stone);
                        break;
                    case 10:
                        audioPath = "files/sound/126.mp3";
                        changeBackground(Resources.background_night);
                        break;
                    case 20:
                        audioPath = "files/sound/122.mp3";
                        changeBackground(Resources.background_plain);
                        break;
                    case 21:
                        audioPath = "files/sound/131.mp3";
                        changeBackground(Resources.background_ragnarok);
                        break;
                    case 30:
                        audioPath = "files/sound/125.mp3";
                        changeBackground(Resources.background_yggdrasil);
                        break;
                    case 50:
                        audioPath = "files/sound/123.mp3";
                        changeBackground(Resources.background_mountains_one);
                        break;
                    case 51:
                        audioPath = "files/sound/128.mp3";
                        changeBackground(Resources.background_mountains_two);
                        break;
                    case 52:
                        audioPath = "files/sound/128.mp3";
                        changeBackground(Resources.background_mountains_three);
                        break;
                    case 60:
                        audioPath = "files/sound/121.mp3";
                        changeBackground(Resources.background_storm);
                        break;
                    case 61:
                        audioPath = "files/sound/126.mp3";
                        changeBackground(Resources.background_mountains_three);
                        break;
                    case 62:
                        audioPath = "files/sound/129.mp3";
                        changeBackground(Resources.background_mountains_three);
                        break;
                    case 63:
                        audioPath = "files/sound/127.mp3";
                        changeBackground(Resources.background_forest_dark);
                        break;
                    case 70:
                        audioPath = "files/sound/126.mp3";
                        changeBackground(Resources.background_muspell);
                        break;
                    case 71:
                        audioPath = "files/sound/127.mp3";
                        changeBackground(Resources.background_muspell);
                        break;
                    case 80:
                        audioPath = "files/sound/124.mp3";
                        changeBackground(Resources.background_fjord);
                        break;
                    default:
                        audioPath = "files/sound/125.mp3";
                        changeBackground(Resources.background_stone);
                        break;
                }
                outputDevice.Stop();
            }
            sceneLbl.Text = curStory.scenery;
            sceneLbl.Location = new Point((Width / 2) - sceneLbl.Width / 2, Height * 15 / 100);
            situationLbl.Text = curStory.situation;
            situationLbl.Location = new Point((Width / 2) - situationLbl.Width / 2, Height * 20 / 100 + sceneLbl.Height);

            int decisionCount = curStory.decisions.Length;

            int btnWidth = Width * 10 / 100;
            int btnHeight = Height * 5 / 100;
            Size btnMin = new Size(btnWidth, btnHeight);
            Size btnMax = new Size(btnWidth * 3, btnHeight);
            int btnGap = 12;
            decisionBtns = new Button[decisionCount];
            int offset = 0;
            for (int i = 0; i < decisionBtns.Length; i++)
            {
                myTabs.TabPages[1].Controls.Add(decisionBtns[i] = new Button());
                decisionBtns[i].MinimumSize = btnMin;
                decisionBtns[i].MaximumSize = btnMax;
                decisionBtns[i].AutoSize = true;
                decisionBtns[i].ForeColor = Color.Black/*decisionColor*/;
                decisionBtns[i].BackColor = Color.Transparent;              
                decisionBtns[i].FlatStyle = FlatStyle.Flat;
                decisionBtns[i].FlatAppearance.BorderSize = 1;
                decisionBtns[i].FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 45, 38);
                decisionBtns[i].FlatAppearance.MouseDownBackColor = Color.Transparent;
                decisionBtns[i].Font = main;
                decisionBtns[i].Text = curStory.decisions[i].text;
                decisionBtns[i].Tag = i;
                decisionBtns[i].Click += new EventHandler(decisionBtn_Click);
                decisionBtns[i].BringToFront();
                offset += decisionBtns[i].Width;
            }
            int startX = Convert.ToInt32((Width / 2) - ( offset / 2 ) - ( (decisionBtns.Length -1) * btnGap / 2));
            for (int i = 0; i < decisionBtns.Length; i++)
            {              
                decisionBtns[i].Location = new Point(startX, Height * 70 / 100);
                startX += decisionBtns[i].Width + btnGap;
            }
        }

        public void jormun()
        {           
            double[] myVals = new double[] { -100.0, -100.0, -100.0, -100.0, -100.0, -100.0, -100.0, -100.0, -100.0, -100.0 }; 
            int[] bestFollowUps = new int[10];
            Random r = new Random();
            for (int i = 0; i < curStory.decisions[myDecision].followups.Length; i++)
            {
                double detVal = 0.1;
                detVal += r.NextDouble() * 0.3;
                for (int j = 0;  j < curStory.decisions[myDecision].followups[i].conditions.Length;  j++)
                {
                    if (curStory.decisions[myDecision].followups[i].conditions[j] > 0)
                    {
                        if(myChar.skills[j] == 0)
                        {
                            detVal = -200.0;
                        }
                        else if (myChar.skills[j] == 100)
                        {
                            detVal = 200.0;
                        }
                        else 
                            detVal += (myChar.skills[j] - curStory.decisions[myDecision].followups[i].conditions[j]) / 100.0;                                             
                    }
                }
                detVal *= curStory.decisions[myDecision].followups[i].probability;
                
                for (int k = 0; k < 9; k++)
                {
                    if(detVal > myVals[k])
                    {
                        for (int l = 9; l > k; l--)
                        {
                            myVals[l] = myVals[l-1];
                            bestFollowUps[l] = bestFollowUps[l-1];
                        }
                        myVals[k] = detVal;
                        bestFollowUps[k] = curStory.decisions[myDecision].followups[i].ID;
                    }
                }
            }           
            
            if(curStory.ID == 114 || curStory.ID == 1117 || curStory.ID == 1123 || curStory.ID == 1115 || curStory.ID == 1116 || curStory.ID == 1108)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (bestFollowUps[0] == 14 && myChar.ID != 1)
                    {
                        bestFollowUps[0] = bestFollowUps[1];
                        while (bestFollowUps[0] != 14)
                        {
                            for (int i = 1; i < 9; i++)
                            {
                                bestFollowUps[i] = bestFollowUps[i + 1];
                            }
                        }
                    }
                    else if ((bestFollowUps[0] == 1119 || bestFollowUps[0] == 1124 || bestFollowUps[0] == 1126 || bestFollowUps[0] == 1132 || bestFollowUps[0] == 1110) && myChar.ID != 2)
                    {
                        bestFollowUps[0] = bestFollowUps[1];
                        while ((bestFollowUps[0] != 1119 || bestFollowUps[0] != 1124 || bestFollowUps[0] != 1126 || bestFollowUps[0] != 1132 || bestFollowUps[0] != 1110))
                        {
                            for (int i = 1; i < 9; i++)
                            {
                                bestFollowUps[i] = bestFollowUps[i + 1];
                            }
                        }
                    }
                }              
            }   

            else if (storyIds.Contains(bestFollowUps[0]) && !(bestFollowUps[0] == 100 || bestFollowUps[0] == 101 || bestFollowUps[0] == 102 ||
                bestFollowUps[0] == 103 || bestFollowUps[0] == 104 || bestFollowUps[0] == 105 || bestFollowUps[0] == 106 ||
                bestFollowUps[0] == 107 || bestFollowUps[0] == 108 || bestFollowUps[0] == 109 || bestFollowUps[0] == 110 ||
                bestFollowUps[0] == 111 || bestFollowUps[0] == 112 || bestFollowUps[0] == 113 || bestFollowUps[0] == 114 ||
                bestFollowUps[0] == 115 || bestFollowUps[0] == 116 || bestFollowUps[0] == 117 || bestFollowUps[0] == 2000))
            {
                curStory = JsonConvert.DeserializeObject<Story>(File.ReadAllText("files/story/405.json"));
            }
            if (bestFollowUps[0] == 99999)
            {
                drawCharTab();
                myTabs.SelectedIndex--;
                audioPath = "files/sound/125.mp3";
                outputDevice.Stop();
                changeBackground(Resources.bckgrnd_wood);
                storyIds.Clear();
                mySave.inStory = 0;
                area = 666;
                mySave.stories = storyIds.ToArray();
            }
            else
            {
                if (File.Exists("files/story/" + bestFollowUps[0] + ".json"))
                    curStory = JsonConvert.DeserializeObject<Story>(File.ReadAllText("files/story/" + bestFollowUps[0] + ".json"));
                else
                    curStory = JsonConvert.DeserializeObject<Story>(File.ReadAllText("files/story/0.json"));
                loadStory(0);
            }
                   
            if (curStory.ID == 5)
            {
                if (!unlockedChars.Contains(3))
                {
                    unlockedChars.Add(3);
                    mySave.myChars = unlockedChars.ToArray();
                }                
                unlockAch(1);
            } //Ragnarok
            else if (curStory.ID == 0) //Dark Souls
                unlockAch(0);
            else if (curStory.ID == 100) //Yggdrasil
                unlockAch(3);
            else if (curStory.ID == 5000) //Jotunheim
                unlockAch(4);
            else if (curStory.ID == 1010) //Freeze to death
                unlockAch(5);
            else if(curStory.ID == 2000)
            {
                specialStoryCounter++;
                if(specialStoryCounter == 50)
                {
                    unlockAch(2);
                    specialStoryCounter = 0;
                }
            } //Surtr is waiting

            else if (curStory.ID == 116) //drank from well
                unlockAch(6);
            else if (curStory.ID == 1105 || curStory.ID == 1101 || curStory.ID == 1103) //Killed Garm
                unlockAch(7);
            else if(curStory.ID == 3017)
            {
                if (!unlockedChars.Contains(5))
                    unlockedChars.Add(5);
                mySave.myChars = unlockedChars.ToArray();
            }
     
        } //does everything

        #region Generated Button Click Events
        private void decisionBtn_Click(object sender, EventArgs e) //event when decision button is clicked
        {
            Button button = sender as Button;
            myDecision = Convert.ToInt32(button.Tag);
            if(!(fadeOutTimer.Enabled || fadeInTimer.Enabled)) fadeOutTimer.Enabled = true;
        }

        private void charBtn_Click(object sender, EventArgs e) //updates specific info on character selection screen
        {
            startBtn.Enabled = true;
            startBtn.Visible = true;
            Button button = sender as Button;
            button.BackColor = Color.FromArgb(117, 82, 66);
            string[] spaces = new string[8];
            myChar = JsonConvert.DeserializeObject<Character>(File.ReadAllText("files/char/" + Convert.ToInt32(button.Tag) + ".json"));
            desLbl.Text = myChar.description;
            for (int i = 0; i < 8; i++)
            {
                if (myChar.skills[i] < 10)
                    spaces[i] = "  " + myChar.skills[i];
                else if (myChar.skills[i] < 100)
                    spaces[i] = " " + myChar.skills[i];
                else spaces[i] = myChar.skills[i].ToString();
            }
            skillLbl.Text = "\n\nDieser Charakter hat folgende Attribute:\n\nVitalität:          " + spaces[0] + 
                "\nAusdauer:           " + spaces[1] + "\nStärke:             " + spaces[2] + 
                "\nGeschicklichkeit:   " + spaces[3] + "\nWahrnehmung:        " + spaces[4] +
                "\nGlück:              " + spaces[5] + "\nIntelligenz:        " + spaces[6] +
                "\nMagie:              " + spaces[7];
        }
        #endregion

        private void Yggdrasil_Load(object sender, EventArgs e) //initializes often used variables and generates GUI elements
        {          

            #region Init
            int btnEdge = 60;
            int btnGap = 20;           
            changeBackground(Resources.bckgrnd_wood);
            lr = 247;
            lg = 241;
            lb = 227;
            light = Color.FromArgb(lr, lg, lb);
            dr = 214; //214,138,105
            dg = 138;
            db = 105;
            decisionColor = Color.FromArgb(dr, dg, db);
            outline = decisionColor;

            Size btnSize = new Size(btnEdge, btnEdge);

            exitBtn.Size = btnSize;
            exitBtn.BringToFront();
            exitBtn.Location = new Point(Width - btnGap - btnEdge, btnGap);

            muteBtn.Size = btnSize;
            muteBtn.BringToFront();
            muteBtn.Location = new Point(Width - btnGap * 2 - btnEdge * 2, btnGap);

            achBtn.Size = btnSize;
            achBtn.Location = new Point(Width - btnGap * 3 - btnEdge * 3, btnGap);
            achBtn.BringToFront();

            #endregion

            #region Save File
            mySave = new SaveFile();
            if (File.Exists("save.sf"))
                mySave = loadGame<SaveFile>();
            else
            {
                mySave.myChars = new int[]{0,1,2,4};
                mySave.inStory = 0;
                mySave.verification = getSHA1(mySave.inStory.ToString() + 42 + unlockedChars.ToString());
                mySave.achievements = new int[0];
            }
            if(mySave.verification != getSHA1(mySave.inStory.ToString() + 42 + unlockedChars.ToString()))
            {
                mySave.myChars = new int[] { 0, 1, 2 };
                mySave.inStory = 0;
                mySave.verification = getSHA1(mySave.inStory.ToString() + 42 + unlockedChars.ToString());
                mySave.achievements = new int[0];
            }
            myChar = JsonConvert.DeserializeObject<Character>(File.ReadAllText("files/char/" + mySave.selectedChar + ".json"));
            #endregion

            #region Tabs

            myTabs.Size = new Size(Width + 8, Height + 26);
            myTabs.Location = new Point(-4, -22);

            #region StoryTab
            sceneLbl.MaximumSize = new Size(Width * 60 / 100, 0);
            sceneLbl.Location = new Point((Width / 2) - sceneLbl.Width / 2, Height * 15 / 100);
            sceneLbl.Parent = myTabs.TabPages[1];

            situationLbl.MaximumSize = new Size(Width * 60 / 100, 0);
            situationLbl.Location = new Point((Width / 2) - situationLbl.Width / 2, Height * 20 / 100 + sceneLbl.Height);

            if (Width < 1500)
                sceneLbl.Font = situationLbl.Font = main = new Font("Book Antiqua", 13.5f);

            saveBtn.Size = new Size(btnEdge, btnEdge);
            saveBtn.Location = new Point(Width - btnGap * 3 - btnEdge * 3, btnGap);

            #endregion

            unlockedChars.AddRange(mySave.myChars);
            achievements.AddRange(mySave.achievements);
            drawCharTab();

            #endregion

            #region Audio
            /* -- Audio -- */
            
            audioPath = "files/sound/125.mp3";
            
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
                outputDevice.Volume = volume / 100f;
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(audioPath);
                outputDevice.Init(audioFile);
            }
     
            outputDevice.Play();

            /* -- Audio End -- */
            #endregion            

            #region Open Action
            if (mySave.inStory == 1)
            {
                storyIds.AddRange(mySave.stories);                        
                curStory = JsonConvert.DeserializeObject<Story>(File.ReadAllText("files/story/" + storyIds[storyIds.Count - 1] + ".json"));
                myTabs.SelectedIndex = 1;
                loadStory(1);            
                fadeInTimer.Enabled = true;
            }

            #endregion
        }

        private void unlockAch(int ach)
        {          
            achBtn.Visible = true;
            if(!achievements.Contains(ach))
                achievements.Add(ach);
        }

        private void achBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!form3.Visible)
                    form3.ShowDialog();
            }
            catch (Exception)
            {
                //need this for some reason, random exception outta nowhere
            }
        }

        private void drawCharTab()
        {
            #region CharacterTab
            //Character Buttons
            if(Width < 1500)
            {
                desLbl.Font = new Font("Book Antiqua", 13.5f);
                skillLbl.Font = new Font("Courier New", 13.5f);
                startBtn.Font = new Font("Book Antiqua", 13.5f);
            }
            Size rightSpan = new Size(Width * 42 / 100, 0);
            int startY = Height * 15 / 100;
            desLbl.Location = new Point(Width * 40 / 100, startY);
            desLbl.MaximumSize = rightSpan;
            skillLbl.MaximumSize = rightSpan;
            startBtn.Size = new Size(rightSpan.Width/* / 2*/, Height * 5 / 100);


            int charCount = mySave.myChars.Length;

            Size charBtnSize = new Size(Width * 20 / 100, Height * 5 / 100);
            int charBtnGap = 18;

            charBtns = new Button[charCount];
            chars = new dynamic[charCount];

            int pos = 0;
            foreach (int i in unlockedChars)
            {
                chars[pos] = JsonConvert.DeserializeObject(File.ReadAllText("files/char/" + i + ".json"));
                myTabs.TabPages[0].Controls.Add(charBtns[pos] = new Button());
                charBtns[pos].Size = charBtnSize;
                charBtns[pos].Location = new Point(Width - Width * 88 / 100, pos * (charBtnGap + charBtnSize.Height) + startY);
                charBtns[pos].Tag = i;
                charBtns[pos].Text = chars[pos].name;
                charBtns[pos].FlatStyle = FlatStyle.Flat;
                charBtns[pos].FlatAppearance.BorderColor = outline;
                charBtns[pos].Font = main;
                charBtns[pos].ForeColor = light;
                charBtns[pos].BackColor = Color.Transparent;
                charBtns[pos].FlatAppearance.MouseOverBackColor = Color.FromArgb(117, 82, 66);
                charBtns[pos].FlatAppearance.MouseDownBackColor = Color.Transparent;
                charBtns[pos].Click += new EventHandler(charBtn_Click);
                pos++;
            }
        
            if (achievements.Count > 0)
                achBtn.Visible = true;
            
            skillLbl.Location = new Point(desLbl.Location.X, Height * 35 / 100);
            startBtn.Location = new Point(skillLbl.Location.X /*+ skillLbl.Width / 2 + startBtn.Width / 2*/, Height * 75 / 100);

            #endregion
        }

        private void changeBackground(Bitmap img)
        {
            if(myTabs.SelectedIndex == 0)
            {
                myTabs.TabPages[0].BackgroundImage = img;
                BackgroundImage = img;
                
            } else if (myTabs.SelectedIndex == 1)
            {
                myTabs.TabPages[1].BackgroundImage = img;
                BackgroundImage = img;
            }
        }

        private void startBtn_Click(object sender, EventArgs e) //starts story with selected character
        {
            for (int i = 0; i < mySave.myChars.Length; i++)
            {
                charBtns[i].BackColor = Color.Transparent;
            }         
            myTabs.SelectedIndex++;
            curStory = JsonConvert.DeserializeObject<Story>(File.ReadAllText("files/story/" + myChar.startstory + ".json"));
            changeBackground(Resources.background_stone);
            loadStory(0);
            lr = lg = lb = dr = dg = db = 0;
            fadeInTimer.Enabled = true;
        }

        #region Control Buttons
        private void exitBtn_Click(object sender, EventArgs e) //closes application
        {
            myExit();
        }

        private void saveBtn_Click(object sender, EventArgs e) //saves game progress
        {
            //save to local file
        }
        #endregion

        private void Yggdrasil_FormClosed(object sender, FormClosedEventArgs e)
        {
            mySave.myChars = unlockedChars.ToArray();
            mySave.achievements = achievements.ToArray();
            mySave.selectedChar = myChar.ID;
            if (storyIds.Count > 0)
            {
                mySave.inStory = 1;
                mySave.stories = storyIds.ToArray();
            }
            mySave.verification = getSHA1(mySave.inStory.ToString() + 42 + unlockedChars.ToString());
            saveGame(mySave);

            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        private void saveGame<T>(T obj)
        {
            using (Stream stream = File.Open("save.sf", FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, obj);
            }
        }

        private T loadGame<T>()
        {
            using (Stream stream = File.Open("save.sf", FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public string getSHA1(string text)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(Encoding.ASCII.GetBytes(text));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in result)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        #region Text and Button fade out
        private void fadeOutTimer_Tick(object sender, EventArgs e) //fades out color/graphic
        {
            if (lr > 0 || dr > 0) { lr -= 8; dr -= 8; }              
            if (lg > 0 || dg > 0) { lg -= 8; dg -= 5; }
            if (lb > 0 || db > 0) { lb -= 8; db -= 4; }
            if (lr < 0) lr = 0;
            if (lg < 0) lg = 0;
            if (lb < 0) lb = 0;
            if (dr < 0) dr = 0;
            if (dg < 0) dg = 0;
            if (db < 0) db = 0;

            fade = Color.FromArgb(lr, lg, lb);
            dfade = Color.FromArgb(dr, dg, db);
            sceneLbl.ForeColor = fade;
            situationLbl.ForeColor = fade;
            for (int i = 0; i < curStory.decisions.Length; i++)
            {
                decisionBtns[i].ForeColor = dfade;
            }
            if (lr == 0 && lg == 0 && lb == 0 && dr == 0 && dg == 0 && db == 0)
            {
                fadeOutTimer.Enabled = false;
                for (int i = 0; i < decisionBtns.Length; i++)
                {
                    decisionBtns[i].Dispose();
                }
                jormun();
                fadeInTimer.Enabled = true;
            }
        }

        private void fadeInTimer_Tick(object sender, EventArgs e) //fades in color/graphic
        {
            if (lr < 247 || dr < 214) { lr += 8; dr += 8; }
            if (lg < 241 || dg < 138) { lg += 8; dg += 5; }
            if (lb < 227 || db < 105) { lb += 8; db += 4; }
            if (lr > 247) lr = 247;
            if (lg > 241) lg = 241;
            if (lb > 227) lb = 227;
            if (dr > 214) dr = 214;
            if (dg > 138) dg = 138;
            if (db > 105) db = 105;
            fade = Color.FromArgb(lr, lg, lb);
            dfade = Color.FromArgb(dr, dg, db);
            sceneLbl.ForeColor = fade;
            situationLbl.ForeColor = fade;

            for (int i = 0; i < curStory.decisions.Length; i++) //curStory already changed at this point
            {
                decisionBtns[i].ForeColor = dfade;
            }
            if (lr == 247 && lg == 241 && lb == 227 && dr == 214 && dg == 138 && db == 105)
            {
                fadeInTimer.Enabled = false;
            }
        }
        #endregion

        #region Audio 
        /* -- Audio -- */
        private void OnPlaybackStopped(object sender, StoppedEventArgs args) //cleans the audio player
        {
            audioFile.Dispose();
            audioFile = null;
            audioFile = new AudioFileReader(audioPath);
            outputDevice.Init(audioFile);
            outputDevice.Play();         
        }

        #region Mute Control
        private void muteBtn_Click(object sender, EventArgs e) //mutes or plays sound
        {
            mute = !mute;
            muteTimer.Enabled = true;
            if (mute)
                muteBtn.BackgroundImage = Resources.drum_mute;
            else
                muteBtn.BackgroundImage = Resources.drum;
        }

        private void muteTimer_Tick(object sender, EventArgs e) //fades out sound
        {
            if(mute)
            {
                volume -= 2;
                if (volume < 0) volume = 0;
                outputDevice.Volume = volume / 100f;
            }
            else
            {
                volume += 2;
                if (volume > 90) volume = 90;
                outputDevice.Volume = volume / 100f;
            }
            if(volume == 0 || volume == 90)
                muteTimer.Enabled = false;
        }
        #endregion
        /* -- Audio End -- */
        #endregion

        private void myExit()
        {
            //build fancy closing form
            try
            {
                if (!form2.Visible)
                    form2.ShowDialog();
            }
            catch (Exception)
            {
                //need this for some reason, random exception outta nowhere
            }
            
        }

        #region Key syshook
        private void HandleHotkey()
        {
            myExit();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }
    }

    public static class Constants
    {
        public const int WM_HOTKEY_MSG_ID = 0x0312;
    }

    public class KeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int key;
        private IntPtr hWnd;
        private int id;

        public KeyHandler(Keys key, Form form)
        {
            this.key = (int)key;
            this.hWnd = form.Handle;
            id = this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, 0, key);
        }

        public bool Unregiser()
        {
            return UnregisterHotKey(hWnd, id);
        }
    }
    #endregion

}