﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace TP3
{
  public class Enemy:Character
  {
    //Propriétés
    static Random rnd = new Random();
    private bool isSpawning;
    protected int nbUpdates;

    protected bool IsSpawning
    {
      get
      {
        return isSpawning;
      }
    }

    protected Enemy(Single posX, Single posY, UInt32 nbVertices, Single size, Color color, Single speed)
    :base(posX, posY, nbVertices, color, speed, CharacterType.ENNEMI)
    {
      //Initialisation des variables
      isSpawning = true;
      nbUpdates = 0;
    }
    public bool Update(Single deltaT, GW gw)
    {
      //A COMPLETE
      nbUpdates++;
      return true;
    }
  }
}
