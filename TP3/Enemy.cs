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
    #region:proprietes
    //Propriétés statiques
    private bool isSpawning;
    //Propriétés protégées
    protected int nbUpdates;
    protected static Random rnd = new Random();
    protected static Music explodeMusic;
    protected Color enemyColor;

    //Propriétés C#
    /// <summary>
    /// Propriété C# indiquant si l'ennemi en en stade de spawn(VRAI) ou non(FAUX)
    /// </summary>
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
    #endregion

    #region:methodes
    //Méthodes
    /// <summary>
    /// Constructeur statique dont le rôle est d'initialiser les variables statiques de la classe
    /// </summary>
    static Enemy()
    {
      explodeMusic = new Music(@"data//enemy_explode.wav");
      explodeMusic.Volume = 10.00f;
    }
    /// <summary>
    /// Constructeur dont le but est d'initialiser l'ennemi selon les valeurs de base. Le visuelle 
    /// des ennemis dépend aussi de ce constructeur, qui s'adaptera en fonction des paramètres entrés
    /// </summary>
    /// <param name="posX">La position en X de l'ennemi</param>
    /// <param name="posY">La position en Y de l'ennemi</param>
    /// <param name="nbVertices">Le nombre de côtés de la forme de l'ennemi</param>
    /// <param name="size">La taille de l'ennemi</param>
    /// <param name="color">La couleur de l'ennemi</param>
    /// <param name="speed">Vitesse de déplacement de base de l'ennemi</param>
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
    /// <summary>
    /// Fonction dont le rôle est d'incrémenter le nombre d'updates tout en retournant si l'ennemi 
    /// peut encore être dans le jeu ou non
    /// </summary>
    /// <param name="deltaT">Vitesse de rafraichissement du jeu</param>
    /// <param name="gw">Instance du jeu</param>
    /// <returns>Booléen indiquant VRAI si l'ennemi est en vie et FAUX s'il est mort</returns>
    public virtual bool Update(Single deltaT, GW gw)
    {
      nbUpdates++;
      return IsAlive;
    }
    /// <summary>
    /// Fonction dont le rôle est de faire jouer le son d'explosion de l'ennemi
    /// et de préparer le visuel de celle-ci
    /// </summary>
    /// <param name="gw"></param>
    public void PlayDeath(GW gw)
    {
      //Jouer l'effet sonore
      explodeMusic.Play();
      //Effet visuel de destruction
      for (int i = 0; i < 360; i++)
      {
        gw.AddParticle(new Particle(Position.X, Position.Y, 4, new Color(enemyColor.R, enemyColor.G, enemyColor.B,
        (byte)rnd.Next(25, 255 + 1)), 5.35f, 0.75f, i));
      }
    }
    /// <summary>
    /// Fonction dont le rôle de calculer l'angle à atteindre pour se diriger vers une position précise
    /// </summary>
    /// <param name="target">Position et X et en Y de la cible dont il faut trouver l'angle</param>
    /// <returns>Réel représentant l'angle à atteindre pour se rendre à la cible</returns>
    public float TargetAngle(Vector2f target)
    {
      //Calculer les dimensions du vecteur selon les distances des deux positions
      Vector2f vectOH = new Vector2f(target.X - this.Position.X, target.Y - this.Position.Y);
      //Retourner l'angle qu'il faut atteindre pour aller précisément dant la direction de la cible
      return (float)((Math.Atan2(vectOH.Y, vectOH.X) * 360.00f) / (2 * Math.PI));
    }
    #endregion
  }
}
