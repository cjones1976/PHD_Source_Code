using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Figureof8Race
{
    class Target
    {
        public Vector2 Pos;
        public double Reward;
        public Color Colour;



        public Target(Vector2 _pos, double _reward, Color _colour, int _ID)
        {
            Pos = _pos;
            Reward = _reward;
            Colour = _colour;
        }

        public Color GetAdjustedColour()
        {
            Color Temp;
            Temp = Colour;
            Temp.R = (byte)(Temp.R - 1);
            Temp.G = (byte)( Temp.G - 1);
            return Temp;


        }
    }
}
