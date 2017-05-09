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
  public class Character:Movable
  {
    #region:propriétes
    //Propriétés statiques privées
    private static Music fireSound;
    //Propriétés privées
    private CharacterType type;
    private Color color;
    //private Vector2f position
    private float speed;
    private DateTime lastFire;
    //Propriétés protected
    protected double fireDelay = 0.25;

    //Propriétés C#2
    /// <summary>
    /// Représente le type de personnage du personnage
    /// </summary>
    public CharacterType Type
    {
      get
      {
        return type;
      }
    }
    #endregion

    #region:Méthodes
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser le ou les variables statiques de
    /// la classe en question.
    /// </summary>
    static Character()
    {
      fireSound = new Music(@"data//Fire_normal.wav");
      fireSound.Volume = 1.25f;
    }
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les propriétés de base
    /// de la classe.
    /// </summary>
    /// <param name="posX">Représente la position de départ en X du personnage</param>
    /// <param name="posY">Représente la position de départ en Y du personnage</param>
    /// <param name="nbVertices">Représente le nombre d'arrêtes de la forme du
    /// personnage</param>
    /// <param name="color">Représente la couleur du personnage</param>
    /// <param name="speed">Représente la vitesse de déplacement du personnage</param>
    /// <param name="type">Représente le type du personnage</param>
    protected Character(float posX, float posY, uint nbVertices, Color color, float speed, CharacterType type)
    :base(posX, posY, nbVertices, color, speed)
    {
      //Initialisation des variables de base
      this.speed = speed;
      this.color = color;
      this.type = type;
    }
    /// <summary>
    /// Fonction dont le rôle est de déterminer s'il y a intersection entre deux objets.
    /// </summary>
    /// <returns>retourne True s'il y a collision, False sinon</returns>
    public bool Intersects(Movable m)
    {
      return base.Intersects(m);
    }
    /// <summary>
    /// Fonction dont le rôle est de faire avancer le personnage selon un nombre de pixels.
    /// </summary>
    /// <param name="nbPixels">Le nombre de pixels dont le personnage sera avancé</param>
    protected override void Advance(Single nbPixels)
    {
      base.Advance(nbPixels);
      //Si la position est hors de l'écran, rester la position à son maxium ou à son minimum
      float x = Math.Max(45, Math.Min(Position.X, GW.WIDTH-45));
      float y = Math.Max(45, Math.Min(Position.Y, GW.HEIGHT-45));
      Position = new Vector2f(x, y);
    }
    /// <summary>
    /// Fonction dont le rôle est de tirer un projectile.
    /// </summary>
    /// <param name="gw">Le jeu (motteur)</param>
    /// <param name="deltaT">Représente le multiplicateur de vitesse de rafraichissement
    /// du jeu</param>
    /// <return>Retourne un booléen vallant Vrai si le tir est fait et Faux si le tir n'est pas fait </return>
    public bool Fire(GW gw, Single deltaT)
    {
      //Si la demande de tir est supérieure ou égale au dernier moment de tir + la cadence, alors
      if (DateTime.Now >= lastFire.AddSeconds(fireDelay))
      {
        //Le dernier moment de tir:
        lastFire = DateTime.Now;
        //Jouer l'effet sonore de tir
        fireSound.Play();
        //Ajouter le projectile (tirer)
        gw.AddProjectile(new Projectile(type, Position.X, Position.Y, 4, Color.Red, 7.50f, Angle));
        return true;
      }
      else
      {
        return false;
      }
    }
    #endregion
  }
}
