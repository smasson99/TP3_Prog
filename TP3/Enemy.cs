using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
namespace TP3
{
  public class Enemy:Character
  {
    //Propriétés
    protected static Random rnd = new Random();
    private bool isSpawning;
    protected int nbUpdates;
    protected Color enemyColor;
    protected static Music explodeMusic;

    protected bool IsSpawning
    {
      get
      {
        return isSpawning;
      }
      set
      {
        isSpawning = value;
      }
    }

    static Enemy()
    {
      explodeMusic = new Music(@"data//enemy_explode.wav");
      explodeMusic.Volume = 20.25f;
    }

    protected Enemy(Single posX, Single posY, UInt32 nbVertices, Single size, Color color, Single speed)
    :base(posX, posY, nbVertices, color, speed, CharacterType.ENNEMI)
    {
      //Initialisation des variables
      isSpawning = true;
      enemyColor = color;
      nbUpdates = 0;
      //Initialisation visuelle de l'ennemi
      for (int i = 0; i < (int)nbVertices; i++)
      {
        double angle = (2 * Math.PI) / nbVertices * i;
        float x = size * (float)Math.Cos(angle);
        float y = size * (float)Math.Sin(angle);
        this[(uint)i] = new Vector2f(x, y);
      }
      
    }
    public virtual bool Update(Single deltaT, GW gw)
    {
      //A COMPLETE
      nbUpdates++;
      return true;
    }

    public void PlayDeath()
    {
      explodeMusic.Play();
    }

    public float TargetAngle(Vector2f target)
    {
      Vector2f vectOH = new Vector2f(target.X - this.Position.X, target.Y - this.Position.Y);
      return (float)((Math.Atan2(vectOH.Y, vectOH.X) * 360.00f) / (2 * Math.PI));
    }
  }
}
