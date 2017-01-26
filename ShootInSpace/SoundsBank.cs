using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace ShootInSpace
{
    public class SoundsBank
    {
        public static Dictionary<string, SoundEffect> Sounds;

        public static void LoadSounds(ContentManager content)
        {
            Sounds = new Dictionary<string, SoundEffect>();

            Sounds.Add("Explosion", content.Load<SoundEffect>("Bombe"));
            Sounds.Add("LaserGun", content.Load<SoundEffect>("Laser_Gun"));
            Sounds.Add("LaserGunModify", content.Load<SoundEffect>("Laser_Gun_Modify"));
            Sounds.Add("Click", content.Load<SoundEffect>("Clic"));
        }

        public static void PlaySoundsEffect(string nameSound)
        {
            Sounds[nameSound].Play();
        }
    }
}
