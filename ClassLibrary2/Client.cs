using RDR2;
using RDR2.Math;
using RDR2.Native;
using RDR2.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ClassLibrary2
{
    public class Client : Script
    {
        Keys openMenuKey;
        RDR2.Control LKey;
        bool controls = false;
        Vector3 pos1;
        float hopefullydirection;
        bool keypressed = false;
        bool keypressed1 = false;
        public Client()
        {
            //openMenuKey = Settings.GetValue("Options", "LightningKey", Keys.J);
            int x = Settings.GetValue("Options", "ConorKey", 0);
            if (x == 0)
            {
                controls = false;
                openMenuKey = Settings.GetValue("Options", "WindKey", Keys.J);
            }
            else
            {
                controls = true;
                LKey = Settings.GetValue("Options", "WindKey", RDR2.Control.ScriptPadUp);
            }
            Tick += OnTick;
            Interval = 1;
            KeyDown += OnKeyDown;
        }
        ~Client()
        {

        }
        private void OnTick(object sender, EventArgs evt)
        {
            //RaycastResult pos = World.CrosshairRaycast(100000, IntersectOptions.Everything);
            //Vector3 pos1 = pos.HitPosition;
            pos1 = World.WaypointPosition;
            //var txt = new TextElement($"{pos1.X} {pos1.Y} {pos1.Z}", new PointF(300f, 300f), 0.3f);
            //txt.Draw();
            //var txt = new TextElement($"{hopefullydirection}", new PointF(300f, 300f), 0.3f);
            //txt.Draw();
            //pos1 -= Game.Player.Character.Position;
            /* if ((Math.Sign(pos1.X) == -1 && Math.Sign(pos1.Y) == -1) || (Math.Sign(pos1.X) == 1 && Math.Sign(pos1.Y) == 1))
             {
                 sub = -90f;
                 control = true;
             }else if((Math.Sign(pos1.X) == -1 && Math.Sign(pos1.Y) == 1) || (Math.Sign(pos1.X) == 1 && Math.Sign(pos1.Y) == -1))
             {
                 sub = 90f;
                 control = true;
             }*/
            //var txt = new TextElement($"{pos1.X} {pos1.Y} {pos1.Z}", new PointF(300f, 300f), 0.3f);
            //txt.Draw();
            if (World.IsWaypointActive)
            {
                pos1 = Vector3.Normalize(pos1 - Game.Player.Character.Position);
                pos1.Y *= -1f;
                hopefullydirection = pos1.ToHeading();
                //if (control)
                //{
                //hopefullydirection += sub;
                //}
                if (keypressed)
                {
                    Function.Call(Hash.SET_WIND_DIRECTION, hopefullydirection);
                }
                if (controls && Game.IsControlJustPressed(2, LKey))
                {
                    if (!keypressed1)
                    {
                        //Function.Call(Hash.SET_WIND_DIRECTION, hopefullydirection);
                        for (int i = 0; i < 21; i++)
                        {
                            Task.Delay(500);
                            Function.Call(Hash.SET_WIND_SPEED, (float)i);
                        }
                        keypressed1 = true;
                    }
                    else if (keypressed1)
                    {
                        for (int i = 20; i > 1; i--)
                        {
                            Task.Delay(500);
                            Function.Call(Hash.SET_WIND_SPEED, (float)i);
                        }
                        Random x = new Random();
                        double sc = (x.NextDouble() * 360);
                        Function.Call(Hash.SET_WIND_DIRECTION, (float)sc);
                        keypressed1 = false;
                    }
                }
            }
            //var txt = new TextElement($"{hopefullydirection}", new PointF(300f, 300f), 0.3f);
            //txt.Draw();
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == openMenuKey && World.IsWaypointActive)
            {
                if (!keypressed)
                {
                    //Function.Call(Hash.SET_WIND_DIRECTION, hopefullydirection);
                    for (int i = 0; i < 21; i++)
                    {
                        Task.Delay(500);
                        Function.Call(Hash.SET_WIND_SPEED, (float)i);
                    }
                    keypressed = true;
                }else if (keypressed)
                {
                    for (int i = 20; i > 1; i--)
                    {
                        Task.Delay(500);
                        Function.Call(Hash.SET_WIND_SPEED, (float)i);
                    }
                    Random x = new Random();
                    double sc = (x.NextDouble() * 360);
                    Function.Call(Hash.SET_WIND_DIRECTION, (float)sc);
                    keypressed = false;
                }
            }
        }

    }
}
