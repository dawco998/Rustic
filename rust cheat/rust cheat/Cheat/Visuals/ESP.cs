using covet.cc.Rust.Structs;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace covet.cc.Cheat.Visuals
{
    class ESP
    {
        public static SharpDX.Matrix GameMatrix;

       
        public static bool WorldToScreen(Vector3 origin, out Vector2 screen)
        {

            UpdateMatrix();

            screen = Vector2.Zero;
            Vector3 translationVector = new Vector3(GameMatrix.M41, GameMatrix.M42, GameMatrix.M43);
            Vector3 up = new Vector3(GameMatrix.M21, GameMatrix.M22, GameMatrix.M23);
            Vector3 right = new Vector3(GameMatrix.M11, GameMatrix.M12, GameMatrix.M13);

            float w = Vector3.Dot(translationVector, origin) + GameMatrix.M44;

            if (w < 0.098f)
                return false;

            float y = Vector3.Dot(up, origin) + GameMatrix.M24;
            float x = Vector3.Dot(right, origin) + GameMatrix.M14;

            x = (EntityUpdater.EntityUpdater.ScreenSize.Width / 2) * (1.0f + x / w);
            y = (EntityUpdater.EntityUpdater.ScreenSize.Height / 2) * (1.0f - y / w);

            screen = new Vector2(x, y);




            return true;


        }


        public class Matrix34
        {
            public byte[] vec0 = new byte[16];
            public byte[] vec1 = new byte[16];
            public byte[] vec2 = new byte[16];
        }

       
        



        private readonly GraphicsWindow _window;


        public ESP()
        {
           
            // initialize a new Graphics object
            // GraphicsWindow will do the remaining initialization
            var graphics = new GameOverlay.Drawing.Graphics
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = false,
                VSync = true,
                WindowHandle = IntPtr.Zero
            };
           
            // it is important to set the window to visible (and topmost) if you want to see it!
            _window = new StickyWindow(EntityUpdater.EntityUpdater.handle, graphics)
            {
                IsTopmost = true,
                IsVisible = true,
                FPS = 144,
                X = EntityUpdater.EntityUpdater.ScreenRect.left,
                Y = EntityUpdater.EntityUpdater.ScreenRect.top,
                Width = EntityUpdater.EntityUpdater.ScreenSize.Width,
                Height = EntityUpdater.EntityUpdater.ScreenSize.Height
            };

            _window.DrawGraphics += _window_DrawGraphics;
        }

        public void Run()
        {
            // creates the window and setups the graphics
            _window.StartThread();
        }
      

        public static void UpdateMatrix()
        {



            IntPtr i = Rust.Rust.i;

            
                Int64 GameObject = Memory.Memory.Mem.ReadVirtualMemory<Int64>((IntPtr)i + 0x8);




                UInt64 ObjectClass = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)GameObject + 0x10);
                UInt64 Entity = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)ObjectClass + 0x30);

            UInt64 Entity1 = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)Entity + 0x18);

               SharpDX.Matrix temp_matrix;
                temp_matrix = Memory.Memory.Mem.ReadVirtualMemory<SharpDX.Matrix>((IntPtr)Entity1 + 0x2e4);
                temp_matrix.Transpose();
                GameMatrix = temp_matrix;
                return;



            

        }

        Entity LocalPlayer;
        private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {

            var gfx = e.Graphics;
            gfx.ClearScene();

            DrawCircle(EntityUpdater.EntityUpdater.MidScreen.X, EntityUpdater.EntityUpdater.MidScreen.Y, Settings.Aimbot.FOV * 2.5f, Color.White);

            if (Settings.Visuals.Crosshair)
            {
                if(Settings.Visuals.CrosshairStyle == Settings.Visuals.CrosshairType.Plus)
                {
                    DrawCrosshair(GameOverlay.Drawing.CrosshairStyle.Plus, EntityUpdater.EntityUpdater.MidScreen.X, EntityUpdater.EntityUpdater.MidScreen.Y, 15, 1, Settings.Visuals.CrosshairColor);

                }
                if (Settings.Visuals.CrosshairStyle == Settings.Visuals.CrosshairType.Dot)
                {
                    DrawCrosshair(GameOverlay.Drawing.CrosshairStyle.Dot, EntityUpdater.EntityUpdater.MidScreen.X, EntityUpdater.EntityUpdater.MidScreen.Y, 3, 1, Settings.Visuals.CrosshairColor);

                }
                if (Settings.Visuals.CrosshairStyle == Settings.Visuals.CrosshairType.Cross)
                {
                    DrawCrosshair(GameOverlay.Drawing.CrosshairStyle.Cross, EntityUpdater.EntityUpdater.MidScreen.X, EntityUpdater.EntityUpdater.MidScreen.Y, 15, 1, Settings.Visuals.CrosshairColor);

                }
            }



          

                foreach (Entity entity in EntityUpdater.EntityUpdater.EntityList.ToArray())
                {

                    if (entity.IsLocalPlayer)
                    {
                        LocalPlayer = entity;
                    }
                    DoVisuals(entity);
                                                   
                }
         




            float GetTextSize(float distance)
            {
                float textsize = distance / 10;
                if(textsize > 16)
                {
                    textsize = 8;
                }
                else
                {
                    textsize = 11;
                }
                return textsize;
            }
            void DoVisuals(Entity entity)
            {



                Vector3 Position = entity.Position;


                float distance = Vector3.Distance(LocalPlayer.Position, Position);

                if (entity.Health < 0.1)
                {
                    return;
                }

                if (distance > Settings.Visuals.MaxDistance)
                    return;

                if (entity == LocalPlayer)
                    return;


                Color BoxColor;
                Color TextColor;

                Vector2 screen;
                WorldToScreen(new Vector3(Position.X, Position.Y, Position.Z), out screen);

               


                Vector2 TrueHeadPos;
                Vector2 TrueHeadPosBox;

                Vector3 HeadPos = Position;
                HeadPos.Y += 1.6f;
                WorldToScreen(new Vector3(HeadPos.X, HeadPos.Y, HeadPos.Z), out TrueHeadPos);

                HeadPos.Y -= 1.6f;
                HeadPos.Y -= 1.6f;

                WorldToScreen(new Vector3(HeadPos.X, HeadPos.Y, HeadPos.Z), out TrueHeadPosBox);



                if(TrueHeadPos == new Vector2(0,0))
                {
                    return;
                }
                if (TrueHeadPosBox == new Vector2(0, 0))
                {
                    return;
                }
                if (screen == new Vector2(0, 0))
                {
                    return;
                }
                if (Settings.Visuals.Snaplines)
                {
                    if(Settings.Visuals.SnaplineStyle == Settings.Visuals.SnaplineType.Top)
                    {
                        DrawLine(EntityUpdater.EntityUpdater.MidScreen.X, EntityUpdater.EntityUpdater.MidScreen.Y - EntityUpdater.EntityUpdater.MidScreen.Y, TrueHeadPos.X, TrueHeadPos.Y, Settings.Visuals.SnaplineColor, 1);

                    }
                    if (Settings.Visuals.SnaplineStyle == Settings.Visuals.SnaplineType.Center)
                    {
                        DrawLine(EntityUpdater.EntityUpdater.MidScreen.X, EntityUpdater.EntityUpdater.MidScreen.Y, TrueHeadPos.X, TrueHeadPos.Y, Settings.Visuals.SnaplineColor, 1);

                    }

                    if (Settings.Visuals.SnaplineStyle == Settings.Visuals.SnaplineType.Bottom)
                    {
                        DrawLine(EntityUpdater.EntityUpdater.MidScreen.X, EntityUpdater.EntityUpdater.MidScreen.Y + EntityUpdater.EntityUpdater.MidScreen.Y, screen.X, screen.Y, Settings.Visuals.SnaplineColor, 1);

                    }
                }


                float BoxHeight = TrueHeadPosBox.Y - screen.Y;
                float BoxWidth = (BoxHeight / 2) * 1.25f; //little bit wider box






                if (Settings.Visuals.Box)
                {


                    DrawBoxEdge(TrueHeadPos.X, TrueHeadPos.Y, BoxWidth, BoxHeight, Settings.Visuals.OutsideBoxColor, 2);
                    DrawFilledBox(TrueHeadPos.X, TrueHeadPos.Y, BoxWidth, BoxHeight, Settings.Visuals.InsideBoxColor);

                }


                if(Settings.Visuals.Distance)
                {
                    HeadPos.Y += 1.6f;
                    HeadPos.Y += 1.6f;
                    HeadPos.Y += 1.6f;
                    HeadPos.Y += .6f;
                    Vector2 Text;
                    WorldToScreen(new Vector3(HeadPos.X, HeadPos.Y, HeadPos.Z), out Text);
                    DrawTextWithOutline("{" + distance.ToString() + "}", Text.X, Text.Y, (int)GetTextSize(distance), Settings.Visuals.TextColor, Color.Black, true, false);
                }

                if (Settings.Visuals.Healthbar)
                {
                    float Health = entity.Health;
                    Color HealthColor = EntityUpdater.EntityUpdater.HealthGradient(EntityUpdater.EntityUpdater.HealthToPercent((int)Health));
                    float x = screen.X + -(BoxWidth / 4);
                    float y = TrueHeadPos.Y;
                    float w = 4;
                    float h = BoxHeight;
                    float HealthHeight = (Health * h) / 100;


                    DrawBox(x, y, w, h, Color.Black, 1);
                    DrawFilledBox(x + 1, y + 1, 2, HealthHeight - 1, HealthColor);
                }
            }


            #region drawing functions
            void DrawBoxEdge(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRectangleEdges(GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawText(string text, float x, float y, int size, Color color, bool bold = false, bool italic = false)
            {
                if (EntityUpdater.EntityUpdater.InScreenPos(x, y))
                {
                    gfx.DrawText(gfx.CreateFont("Verdana", size, bold, italic), GetBrushColor(color), x, y, text);
                }
            }

            void DrawTextWithOutline(string text, float x, float y, int size, Color color, Color outlinecolor, bool bold = true, bool italic = false)
            {
                DrawText(text, x - 1, y + 1, size, outlinecolor, bold, italic);
                DrawText(text, x + 1, y + 1, size, outlinecolor, bold, italic);
                DrawText(text, x, y, size, color, bold, italic);
            }

            void DrawTextWithBackground(string text, float x, float y, int size, Color color, Color backcolor, bool bold = false, bool italic = false)
            {
                if (EntityUpdater.EntityUpdater.InScreenPos(x, y))
                {
                    gfx.DrawTextWithBackground(gfx.CreateFont("Arial", size, bold, italic), GetBrushColor(color), GetBrushColor(backcolor), x, y, text);
                }
            }

            void DrawLine(float fromx, float fromy, float tox, float toy, Color color, float thiccness = 2.0f)
            {
                gfx.DrawLine(GetBrushColor(color), fromx, fromy, tox, toy, thiccness);
            }

            void DrawFilledBox(float x, float y, float width, float height, Color color)
            {
                gfx.FillRectangle(GetBrushColor(color), x, y, x + width, y + height);
            }

            void DrawCircle(float x, float y, float radius, Color color, float thiccness = 1)
            {
                gfx.DrawCircle(GetBrushColor(color), x, y, radius, thiccness);
            }

            void DrawCrosshair(GameOverlay.Drawing.CrosshairStyle style, float x, float y, float size, float thiccness, Color color)
            {
                gfx.DrawCrosshair(GetBrushColor(color), x, y, size, thiccness, style);
            }

            void DrawFillOutlineBox(float x, float y, float width, float height, Color color, Color fillcolor, float thiccness = 1.0f)
            {
                gfx.OutlineFillRectangle(GetBrushColor(color), GetBrushColor(fillcolor), x, y, x + width, y + height, thiccness);
            }

            void DrawBox(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRectangle(GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawOutlineBox(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.OutlineRectangle(GetBrushColor(Color.FromArgb(0)), GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawRoundedBox(float x, float y, float width, float height, float radius, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRoundedRectangle(GetBrushColor(color), x, y, x + width, y + height, radius, thiccness);
            }

            GameOverlay.Drawing.SolidBrush GetBrushColor(Color color)
            {
                
                return gfx.CreateSolidBrush(color.R, color.G, color.B, color.A);
            }
            #endregion
        }
    }
}

