using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace TP3
{
  public class Particle
  {
    //Propriétés
    private float dureeVie;
    private RectangleShape shape;
    private DateTime debutVie;
    private DateTime finVie;
    
    //Méthodes
    public Particle(float posX, float posY, uint nbVertices, Color color, float dimension, bool hasTexture)
    {
      //Initialisation des variables de base
      shape = new RectangleShape(new Vector2f(dimension, dimension));
      dureeVie = 0.5f;
      debutVie = DateTime.Now;
      
      ////Initialisation visuelle du projectile
      if (hasTexture)
        shape.Texture = new Texture(@"data//Particle.tga");
      shape.FillColor = color;
      shape.Position = new Vector2f(posX, posY);
    }
    public void Draw(RenderWindow window)
    {
      window.Draw(shape);
    }
    public bool Update(GW gw)
    {
      if (DateTime.Now >= debutVie.AddSeconds(dureeVie))
        return false;
      else
        return true;
    }
  }
}
