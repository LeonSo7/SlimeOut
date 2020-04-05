using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets instance { get; private set; }

   private void Awake() {
       instance = this;
   }
   public Sprite blue1Sprite;
   public Sprite blue2Sprite;
   public Sprite blue3Sprite;
   public Sprite red1Sprite;
   public Sprite red2Sprite;
   public Sprite red3Sprite;
   public Sprite green1Sprite;
   public Sprite green2Sprite;
   public Sprite green3Sprite;
   public Sprite xp1Sprite;
   public Sprite xp2Sprite;
   public Sprite xp3Sprite;
}
