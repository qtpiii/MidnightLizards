using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Fisobs;
using Fisobs.Core;

namespace MidnightLizards
{
    [BepInPlugin(MOD_ID, "Midnight Lizards", "0.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private const string MOD_ID = "qtpi.midnight-lizards";

        public void OnEnable()
        {
            try
            {
                Content.Register(new MidnightLizardCritob());
            }
            catch(Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }
    }
    public static class critobTemplate
    {
        public static CreatureTemplate.Type MidnightLizard = new(nameof(MidnightLizard), true);
        public static void UnregisterValues()
        {
            if (MidnightLizard != null)
            {
                MidnightLizard.Unregister();
                MidnightLizard = null;
            }
        }
    }

    public static class SandboxUnlockID
    {
        public static MultiplayerUnlocks.SandboxUnlockID MidnightLizard = new(nameof(MidnightLizard), true);
        public static void UnregisterValues()
        {
            if (MidnightLizard != null)
            {
                MidnightLizard.Unregister();
                MidnightLizard = null;
            }
        }
    }
}
