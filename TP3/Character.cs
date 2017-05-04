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
    private Vector2f position;
    private float speed;
    private DateTime lastFire;
    //Propriétés protected
    protected double fireDelay;

    //Propriétés C#
    /// <summary>
    /// Représente la position en X du personnage
    /// </summary>
    public float PositionX
    {
      get
      {
        return position.X;
      }
      set
      {
        position.X = value;
      }
    }
    /// <summary>
    /// Représente la position en Y du personnage
    /// </summary>
    public float PositionY
    {
      get
      {
        return position.Y;
      }
      set
      {
        position.Y = value;
      }
    }
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
      fireSound = new Music(@"Fire_normal.wav");
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
      PositionX = posX;
      PositionY = posY;
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
    }
    /// <summary>
    /// Fonction dont le rôle est de tirer un projectile.
    /// </summary>
    /// <param name="gw">Le jeu (motteur)</param>
    /// <param name="deltaT">Représente le multiplicateur de vitesse de rafraichissement
    /// du jeu</param>
    public void Fire(GW gw, Single deltaT)
    {
      //A COMPLETER
    }
    #endregion
  }
}
