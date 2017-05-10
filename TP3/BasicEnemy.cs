using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
namespace TP3
{
  public class BasicEnemy:Enemy
  {
    //Proprietés
    static float BasicEnemySpeed;
    static Music spawnMusic;

    //Méthodes

    static BasicEnemy()
    {
      BasicEnemySpeed = 1.00f;
      spawnMusic = new Music(@"data//Enemy_spawn_orange.wav");
    }
    public BasicEnemy(Single posX, Single posY, Single angle)
    :base(posX, posY, 3, 5.00f, Color.Red, 1.00f)
    {
      
    }
  }
}
