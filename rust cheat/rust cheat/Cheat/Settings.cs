using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covet.cc.Cheat
{
    class Settings
    {
       public class Visuals
       {
            public static bool Box = false;
            public static bool Snaplines = false;
            public static bool Healthbar = false;
            public static bool Crosshair = false;
            public static bool Distance = false;

            public static BoxType BoxStyle = BoxType.Box;
            public static SnaplineType SnaplineStyle = SnaplineType.Top;
            public static CrosshairType CrosshairStyle = CrosshairType.Plus;

            public static Color InsideBoxColor = Color.FromArgb(50, 50, 50, 255);
            public static Color OutsideBoxColor = Color.White;
            public static Color SnaplineColor = Color.White;
            public static Color CrosshairColor = Color.White;
            public static Color TextColor = Color.White;

            public static int MaxDistance = 300;

            public enum BoxType
            {
                Box,
                BoxFilled,
                Corner,
                CornerFilled
            }

            public enum SnaplineType
            {
                Top,
                Center,
                Bottom
            }

            public enum CrosshairType
            {
                Plus,
                Dot,
                Cross
            }

        }

        public class Aimbot
        {
            public static bool AimBot = false;
            public static bool NoRecoil = false;

            public static int FOV = 100;
            public static int Smoothness = 10;

        }

        public class Misc
        {

        }

        public class Config
        {

        }
    }
}
