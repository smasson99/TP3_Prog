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
    private float angleCible;
    private Vector2f cible;
    private DateTimeOffset timeSpawn;
    private DateTimeOffset timeInvMax;
    private DateTimeOffset timeChase;

    //Méthodes

    static BasicEnemy()
    {
      BasicEnemySpeed = 0.75f;
      spawnMusic = new Music(@"data//Enemy_spawn_orange.wav");
      spawnMusic.Volume = 45.25f;
    }

    public BasicEnemy(Single posX, Single posY, Single angle)
    :base(posX, posY, 3, 5.00f, new Color(252, 130, 0), 1.00f)
    {
      //Initialisation des variables de base
      Angle = angle;
      timeSpawn = new DateTimeOffset(DateTime.Now.AddSeconds(0.20f));
      //Initialisation visuelle de l'ennemi
      this[0] = new Vector2f(-7, 20);
      this[1] = new Vector2f(55, 0);
      this[2] = new Vector2f(-7, -20);

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
        if (DateTime.Now >= timeSpawn)
        {
          spawnMusic.Play();
          IsSpawning = false;
          //Trouver une cible aléatoire
          cible = new Vector2f(rnd.Next(15, GW.WIDTH - 50), rnd.Next(15, GW.HEIGHT - 50));
          angleCible = TargetAngle(cible);
          timeInvMax = new DateTimeOffset(DateTime.Now.AddSeconds(3.40f));
        }
      }
      //En stade de repérage
      if (DateTime.Now > timeChase)
      {
        BasicEnemySpeed = 0.75f;
        if (this.Position.X >= cible.X - 5 && this.Position.X <= cible.X + 5 && this.Position.Y >= cible.Y-5 && this.Position.Y <= cible.Y+5 ||
        DateTime.Now > timeInvMax)
        {
          timeInvMax = DateTime.Now.AddSeconds(rnd.Next(2, 4));
          cible = new Vector2f(rnd.Next(30, GW.WIDTH-30), rnd.Next(30, GW.HEIGHT-30));
          angleCible = TargetAngle(cible);
        }
        if (Angle > angleCible)
        {
          Rotate(-BasicEnemySpeed);
        }
        else if (Angle < angleCible)
        {
          Rotate(BasicEnemySpeed);
        }
        if (Angle < TargetAngle(gw.hero.Position) +10 && Angle > TargetAngle(gw.hero.Position) - 10)
        {
          timeChase = DateTime.Now.AddSeconds(rnd.Next(12, 20 + 1));
        }
        Advance(BasicEnemySpeed);
      }
      //En stade de chasse
      else if (DateTime.Now < timeChase)
      {
        BasicEnemySpeed = 1.75f;
        float angleCible = TargetAngle(gw.hero.Position);
        if (Angle > angleCible + 12)
        {
          Rotate(-90*deltaT);
        }
        else if (Angle < angleCible - 12)
        {
          Rotate(90*deltaT);
        }
        else if (rnd.Next(0, 45+1) == 1)
        {
          Fire(gw, deltaT);
        }
        Advance(BasicEnemySpeed);
      }
      return true;
    }
  }
}
