using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Yggdrasil.Properties;

namespace Yggdrasil
{
    public partial class Form3 : Form
    {
        PictureBox[] myAchievements;

        protected override CreateParams CreateParams //prevents flickering of form
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public Form3()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {      
        }
      
        private void showPopup(object sender, EventArgs e)
        {
            popup.Visible = true;
            popup.BringToFront();
            PictureBox picBox = sender as PictureBox;
            popup.Location = new Point(((Control)sender).Location.X, ((Control)sender).Location.Y + 75);
            switch (picBox.Tag)
            {
                case 0:
                    titleLbl.Text = "Dark Souls";
                    desLbl.Text = "Du bist gestorben.";
                    break;
                case 1:
                    titleLbl.Text = "Ragnarök";
                    desLbl.Text = "Du hast das Ende der Welt miterlebt.";
                    break;
                case 2:
                    titleLbl.Text = "Der Schwarze";
                    desLbl.Text = "Surtr hat genug gewartet.";
                    break;
                case 3:
                    titleLbl.Text = "Yggdrasil";
                    desLbl.Text = "Erreiche den Weltenbaum Yggdrasil.";
                    break;
                case 4:
                    titleLbl.Text = "Jötunheim";
                    desLbl.Text = "Erreiche Jötunheim, die Welt der Riesen.";
                    break;
                case 5:
                    titleLbl.Text = "Eis am Stiel";
                    desLbl.Text = "Du bist erfroren.";
                    break;
                case 6:
                    titleLbl.Text = "Erleuchtung";
                    desLbl.Text = "Trinke aus Mimirs Quelle.";
                    break;
                case 7:
                    titleLbl.Text = "Bezwinger";
                    desLbl.Text = "Garm konnte dich nicht stoppen.";
                    break;
                default: break;
            }
        }

        private void removePopup(object sender, EventArgs e)
        {
            popup.Visible = false;
        }

        private void Form3_Activated(object sender, EventArgs e)
        {
            myAchievements = new PictureBox[20];
            Size achSize = new Size(75, 75);
            int achGap = 20;
            int pos = 0;
            foreach (int i in Yggdrasil.achievements)
            {
                Controls.Add(myAchievements[i] = new PictureBox());
                myAchievements[i].Size = achSize;
                myAchievements[i].Location = new Point((pos - ((pos / 6) * 6)) * (achGap + achSize.Width) + 70, (pos / 6) * (achGap + achSize.Height) + 70);
                myAchievements[i].MouseEnter += showPopup;
                myAchievements[i].MouseLeave += removePopup;
                myAchievements[i].Tag = i;
                pos++;

                switch (i)
                {
                    case 0:
                        myAchievements[i].BackgroundImage = Resources.icon_dark_souls;
                        break;
                    case 1:
                        myAchievements[i].BackgroundImage = Resources.icon_ragnarok;
                        break;
                    case 2:
                        myAchievements[i].BackgroundImage = Resources.icon_der_schwarze;
                        break;
                    case 3:
                        myAchievements[i].BackgroundImage = Resources.icon_yggdrasil;
                        break;
                    case 4:
                        myAchievements[i].BackgroundImage = Resources.icon_jotunheim;
                        break;
                    case 5:
                        myAchievements[i].BackgroundImage = Resources.icon_eis;
                        break;
                    case 6:
                        myAchievements[i].BackgroundImage = Resources.icon_well;
                        break;
                    case 7:
                        myAchievements[i].BackgroundImage = Resources.icon_garm;
                        break;
                    default: break;
                }
            }
        }     
    }
}
