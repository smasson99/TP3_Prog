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
    private Shape shape;
    private DateTime debutVie;
    
    //Méthodes
    public Particle(float posX, float posY, uint nbVertices, Color color, float dimension, bool hasTexture, float dureeVie)
    {
      //Initialisation des variables de base
      if (nbVertices >= 4)
        shape = new RectangleShape(new Vector2f(dimension, dimension));
      this.dureeVie = dureeVie;
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
