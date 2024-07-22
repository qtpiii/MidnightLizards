using Fisobs.Core;
using Fisobs.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Fisobs.Sandbox;
using JetBrains.Annotations;
using DevInterface;

namespace MidnightLizards
{
    internal class MidnightLizardCritob : Critob
    {
        public MidnightLizardCritob() : base(critobTemplate.MidnightLizard)
        {
            Icon = new SimpleIcon("Kill_Black_Lizard", new Color(0.0f, 0.05f, 0.6f));
            LoadedPerformanceCost = 100f;
            SandboxPerformanceCost = new SandboxPerformanceCost(0.5f, 0.5f);
            RegisterUnlock(KillScore.Configurable(8), SandboxUnlockID.MidnightLizard);
        }

        public override ArtificialIntelligence CreateRealizedAI(AbstractCreature acrit)
        {
            return new LizardAI(acrit, acrit.world);
        }

        public override Creature CreateRealizedCreature(AbstractCreature acrit)
        {
            return new Lizard(acrit, acrit.world);
        }

        public override CreatureState CreateState(AbstractCreature acrit)
        {
            return new LizardState(acrit);
        }

        public override CreatureTemplate CreateTemplate()
        {
            return LizardBreeds.BreedTemplate(Type, StaticWorld.GetCreatureTemplate(CreatureTemplate.Type.LizardTemplate), null, null, null);
        }

        public override void EstablishRelationships()
        {
            var s = new Relationships(Type);
            s.Eats(CreatureTemplate.Type.Slugcat, 1f);
        }

        public override IEnumerable<string> WorldFileAliases()
        {
            return new string[] { "MidnightLizard", "Midnight" };
        }

        public override IEnumerable<RoomAttractivenessPanel.Category> DevtoolsRoomAttraction()
        {
            return new[] { RoomAttractivenessPanel.Category.Lizards, RoomAttractivenessPanel.Category.All, RoomAttractivenessPanel.Category.LikesOutside };
        }

        public override string DevtoolsMapName(AbstractCreature acrit)
        {
            return "MidLiz";
        }
        public override Color DevtoolsMapColor(AbstractCreature acrit)
        {
            return new Color(0.0f, 0.05f, 0.6f);
        }

        private static CreatureTemplate templateBreed(On.LizardBreeds.orig_BreedTemplate_Type_CreatureTemplate_CreatureTemplate_CreatureTemplate_CreatureTemplate orig, CreatureTemplate.Type type, CreatureTemplate lizardAncestor, CreatureTemplate pinkTemplate, CreatureTemplate blueTemplate, CreatureTemplate greenTemplate)
        {
            if (type == critobTemplate.MidnightLizard)
            {
                var temp = orig(CreatureTemplate.Type.CyanLizard, lizardAncestor, pinkTemplate, blueTemplate, greenTemplate);
                var breedparams = (temp.breedParameters as LizardBreedParams);
                temp.type = type;
                temp.name = "Midnight Lizard";
                breedparams.tailSegments = 6;
                breedparams.standardColor = new(0.0f, 0.05f, 0.6f);
                breedparams.perfectVisionAngle = Mathf.Lerp(1f, -1f, 0f);
                breedparams.periferalVisionAngle = Mathf.Lerp(1f, -1f, 0.3f);
                breedparams.baseSpeed = 1.2f;
                temp.doPreBakedPathing = false;
                temp.preBakedPathingAncestor = StaticWorld.GetCreatureTemplate(CreatureTemplate.Type.BlueLizard);
                temp.requireAImap = true;
                return temp;
            }
            return orig(type, lizardAncestor, pinkTemplate, blueTemplate, greenTemplate);
        }

        private void GiveSuperHearing(On.LizardAI.orig_ctor orig, LizardAI self, AbstractCreature creature, World world)
        {
            orig(self, creature, world);
            if(self.lizard.Template.type == critobTemplate.MidnightLizard)
            {
                self.AddModule(new SuperHearing(self, self.tracker, 350f));
                self.noiseTracker.hearingSkill = 2f;
            }
        }
    }
}
