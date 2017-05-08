using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace TP3
{
  public class Particle:Movable
  {
    //Propriétés

    //Méthodes
    public Particle(float posX, float posY, uint nbVertices, Color color, float dimension)
    :base(posX, posY, nbVertices, color, 0.00f)
    {
      
    }
  }
}
