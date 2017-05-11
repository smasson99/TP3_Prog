using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
namespace TP3
{
  public class BasicEnemy:Enemy
  {
    //Proprietés
    static float BasicEnemySpeed;
    static Music spawnMusic;
    private DateTimeOffset tempsSpawn;

    //Méthodes

    static BasicEnemy()
    {
      BasicEnemySpeed = 1.00f;
      spawnMusic = new Music(@"data//Enemy_spawn_orange.wav");
      spawnMusic.Volume = 45.25f;
    }

    public BasicEnemy(Single posX, Single posY, Single angle)
    :base(posX, posY, 3, 5.00f, new Color(252, 130, 0), 1.00f)
    {
      //Initialisation des variables de base
      Angle = angle;
      tempsSpawn = new DateTimeOffset(DateTime.Now.AddSeconds(0.2));
      //Initialisation visuelle de l'ennemi
      this[0] = new Vector2f(-7, 20);
      this[1] = new Vector2f(55, 0);
      this[2] = new Vector2f(-7,-20);
    }

    public override bool Update(Single deltaT, GW gw)
    {
      //A COMPLETE
      //Initialisation de l'effet de particule
      gw.AddParticle(new Particle(Position.X - 12.50f, Position.Y - 12.50f, 4, new Color(enemyColor.R, enemyColor.G, enemyColor.B, 
      (byte)rnd.Next(25, 225 + 1)), 25.00f, true, 0.07f));
      //En stade de spawn...
      if (IsSpawning)
      {
        Advance(1.50f);
        if (DateTime.Now >= tempsSpawn)
        {
          spawnMusic.Play();
          IsSpawning = false;
        }
      }
      return true;
    }
  }
}
