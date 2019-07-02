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
    class Items
    {

        Color Colour;
        string TextColour;

        double reward = 0;

        public Items()
        {
        }

        public Items(Color _Colour,  double _reward, string _Text)
        {
            Colour = _Colour;
            reward = _reward;
            TextColour = _Text;
            
        }

        public double GetReward()
        {

            return reward;
        }

        public Color GetColour()
        {
            return Colour;
        }


    }
}
