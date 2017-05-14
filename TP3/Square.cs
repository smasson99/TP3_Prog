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
  public class Square:Enemy
  {
    #region:proprietes
    //Proprietés Statiques
    /// <summary>
    /// Représente la vitesse de déplacement de l'ennemi basique
    /// </summary>
    static float BasicEnemySpeed;
    /// <summary>
    /// Représente l'emplacement du fichier de l'effet sonore du spawn de l'ennemi basique
    /// </summary>
    static Music spawnMusic;
    //Propriétés privées
    /// <summary>
    /// Représente l'angle qu'il faut atteindre pour se rendre à une position précise
    /// </summary>
    private float angleCible;
    /// <summary>
    /// Représente la cible à atteindre
    /// </summary>
    private Vector2f cible;
    /// <summary>
    /// Représente la durée du stade de spawning de l'ennemi de base
    /// </summary>
    private DateTimeOffset timeSpawn;
    /// <summary>
    /// Représente la durée du stade de chasse de l'ennemi de base
    /// </summary>
    private DateTimeOffset timeChase;
    /// <summary>
    /// Représente la durée du stade d'investigation de l'ennemi de base
    /// </summary>
    private DateTimeOffset timeInvMax;
    private DateTime tirDuree;
    #endregion

    #region:methodes
    //Méthodes
    /// <summary>
    /// Constructeur permettant d'initialiser les propriétés 
    /// statiques de la classe héritée: "BasicEnemy"
    /// </summary>
    static Square()
    {
      BasicEnemySpeed = 0.75f;
      spawnMusic = new Music(@"data//Enemy_spawn_purple.wav");
      spawnMusic.Volume = 20.00f;
    }
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les variables de base.
    /// </summary>
    /// <param name="posX">Représente la postition en X de l'ennemi de base</param>
    /// <param name="posY">Représente la postition en Y de l'ennemi de base</param>
    /// <param name="angle">Représente l'inclinaison de l'angle de l'ennemi de base</param>
    public Square(Single posX, Single posY, Single angle)
    :base(posX, posY, 4, 27.50f, new Color(65, 61, 175), 1.00f)
    {
      //Initialisation des variables de base
      Angle = angle;
      timeSpawn = new DateTimeOffset(DateTime.Now.AddSeconds(0.20f));
      tirDuree = DateTime.Now.AddSeconds(2);
    }
    /// <summary>
    /// Fonction dont le rôle est d'assurer le comportement de l'ennemi de base en toutes 
    /// cirsonstances. L'ennemi suivra un parcourt aléatoire lorsqu'il ne détectera pas le 
    /// joueur et foncera sur celui-ci s'il le perçoit tout en ouvrant le feu. Retourner VRAI 
    /// si l'ennemi est toujours considéré comme vivant.
    /// </summary>
    /// <param name="deltaT">Représente la vitesse de rafraichissement du jeu</param>
    /// <param name="gw">Représente le jeu</param>
    /// <returns>Vaux VRAI si l'ennemi est toujours considéré comme viable et FAUX dans le cas contraire</returns>
    public override bool Update(Single deltaT, GW gw)
    {
      //Initialisation de l'effet de particule
      gw.AddParticle(new Particle(Position.X, Position.Y, 4, new Color(enemyColor.R, enemyColor.G, enemyColor.B,
      (byte)rnd.Next(25, 255 + 1)), 5.35f, 0.55f, -rnd.Next(180 - (int)Angle - 5, 180 - (int)Angle + 5 + 1)));
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
        BasicEnemySpeed = 0.17f;
        if (this.Position.X >= cible.X - 5 && this.Position.X <= cible.X + 5 && this.Position.Y >= cible.Y - 5 && this.Position.Y <= cible.Y + 5 ||
        DateTime.Now > timeInvMax)
        {
          timeInvMax = DateTime.Now.AddSeconds(rnd.Next(2, 4));
          cible = new Vector2f(rnd.Next(30, GW.WIDTH - 30), rnd.Next(30, GW.HEIGHT - 30));
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
        if (Angle < TargetAngle(gw.hero.Position) + 10 && Angle > TargetAngle(gw.hero.Position) - 10)
        {
          timeChase = DateTime.Now.AddSeconds(rnd.Next(6, 15 + 1));
        }
        Advance(BasicEnemySpeed);
      }
      //En stade de tir
      else if (DateTime.Now < timeChase)
      {
        BasicEnemySpeed = 0.50f;
        float angleCible = TargetAngle(gw.hero.Position);
        if (Angle > angleCible + 12)
        {
          Rotate(-90 * deltaT);
        }
        else if (Angle < angleCible - 12)
        {
          Rotate(90 * deltaT);
        }
        else if (rnd.Next(0, 45 + 1) == 1)
        {
          for (int i = 0; i < 4; i++)
          {
            fireSound.Play();
            gw.AddProjectile(new Projectile(CharacterType.ENNEMI, Position.X, Position.Y, 4, enemyColor, 7.50f, 360 / this.Size / 2, 0));
            gw.AddProjectile(new Projectile(CharacterType.ENNEMI, Position.X, Position.Y, 4, enemyColor, 7.50f, 360 / this.Size / 2, 90));
            gw.AddProjectile(new Projectile(CharacterType.ENNEMI, Position.X, Position.Y, 4, enemyColor, 7.50f, 360 / this.Size / 2, 180));
            gw.AddProjectile(new Projectile(CharacterType.ENNEMI, Position.X, Position.Y, 4, enemyColor, 7.50f, 360 / this.Size / 2, 270));
          }
        }
        Advance(BasicEnemySpeed);
      }
      return true;
    }
    #endregion
  }
}
