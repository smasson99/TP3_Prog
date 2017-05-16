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
    //Toutes les propriétés
    #region:propriétes
    //Propriétés statiques protected
    /// <summary>
    /// Propriété représentant l'effet sonore du son de tir
    /// </summary>
    protected static Music fireSound;
    /// <summary>
    /// Propriété indiquant le type de personnage du personnage
    /// </summary>
    private CharacterType type;
    //Propriétés privées
    /// <summary>
    /// Propriété représentant la couleur de la forme du personnage
    /// </summary>
    private Color color;
    /// <summary>
    /// Propriété représentant la vitesse courante du personnage
    /// </summary>
    private float speed;
    /// <summary>
    /// Propriété représentant le dernier moment de tir du personnage
    /// </summary>
    private DateTime lastFire;
    //Propriétés protected
    /// <summary>
    /// Propriété représentant la cadence de tir du personnage
    /// </summary>
    protected double fireDelay = 0.25;

    //Propriétés C#
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
    //Toutes les méthodes
    #region:methodes
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les variables statiques
    /// </summary>
    static Character()
    {
      //Initialisation des variables statiques
      fireSound = new Music(@"data//Fire_normal.wav");
      fireSound.Volume = 5.00f;
    }
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les propriétés de base
    /// </summary>
    /// <param name="posX">Représente la position de départ en X du personnage</param>
    /// <param name="posY">Représente la position de départ en Y du personnage</param>
    /// <param name="nbVertices">Représente le nombre d'arrêtes de la forme du
    /// personnage</param>
    /// <param name="color">Représente la couleur du personnage</param>
    /// <param name="speed">Représente la vitesse de déplacement du personnage</param>
    /// <param name="type">Représente le type de personnage du personnage</param>
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
      //Déterminer s'il y a intersection
      return base.Intersects(m);
    }
    /// <summary>
    /// Fonction dont le rôle est de faire avancer le personnage selon un nombre de pixels entré en
    /// paramètre
    /// </summary>
    /// <param name="nbPixels">Le nombre de pixels dont le personnage sera avancé</param>
    protected override void Advance(Single nbPixels)
    {
      base.Advance(nbPixels);
      //Si la position est hors de l'écran, rénitialiser la position à son maxium ou à son 
      //minimum, dépendement de sa position
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
    public virtual bool Fire(GW gw, Single deltaT)
    {
      //Si la demande de tir est supérieure ou égale au dernier moment de tir + la cadence, alors
      if (DateTime.Now >= lastFire.AddSeconds(fireDelay))
      {
        //Le dernier moment de tir:
        lastFire = DateTime.Now;
        //Jouer l'effet sonore de tir
        fireSound.Play();
        //Ajouter le projectile (tirer)
        gw.AddProjectile(new Projectile(type, Position.X, Position.Y, 4, Color.Red, 7.50f, 3.50f, Angle));
        //succès, la cadence de tir est respectée
        return true;
      }
      else
      {
        //échec, la cadence de tir n'est pas respectée
        return false;
      }
    }
    #endregion
  }
}
