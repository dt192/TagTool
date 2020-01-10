﻿using TagTool.Cache;
using TagTool.Commands.Tags;
using TagTool.Serialization;
using TagTool.Tags.Definitions;

namespace TagTool.Commands.Porting
{
    static class PortingContextFactory
    {
        public static CommandContext Create(CommandContextStack contextStack, GameCache currentCache, GameCache portingCache)
        {
            var context = new CommandContext(contextStack.Context, portingCache.DisplayName + "\\tags");

            // only support gen3 to HO porting for now, add more later
            if(portingCache.GetType() == typeof(GameCacheContextGen3) && currentCache.GetType() == typeof(GameCacheContextHaloOnline))
                Populate(contextStack, context, currentCache, portingCache);

            // add tags command to the new cache
            TagCacheContextFactory.Populate(contextStack, context, portingCache);

            return context;
        }
        
        public static SoundCacheFileGestalt LoadSoundGestalt(GameCache cache)
        {
            CachedTag blamTag = null;

            foreach (var tag in cache.TagCache.TagTable)
            {
                if (tag.Group.Tag == "ugh!")
                {
                    blamTag = tag;
                    break;
                }
            }

            if (blamTag == null)
                return null;
            SoundCacheFileGestalt result;
            using (var stream = cache.TagCache.OpenTagCacheRead())
            {
                result = cache.Deserialize<SoundCacheFileGestalt>(stream, blamTag);
            }
            return result;
        }

        public static void Populate(CommandContextStack contextStack, CommandContext context, GameCache currentCache, GameCache portingCache)
        {


            if(currentCache.GetType() == typeof(GameCacheContextHaloOnline))
            {
                var hoCache = currentCache as GameCacheContextHaloOnline;
                var portTagCommand = new PortTagCommand(hoCache, portingCache);
                context.AddCommand(portTagCommand);
                context.AddCommand(new MergeAnimationGraphsCommand(hoCache, portingCache, portTagCommand));
            }

            
            /*
            

 
            context.AddCommand(new PortArmorVariantCommand(cacheContext, blamCache));
            context.AddCommand(new PortMultiplayerEventsCommand(cacheContext, blamCache));
            context.AddCommand(new PortMultiplayerScenarioCommand(cacheContext, blamCache, portTagCommand));
            */
        }
    }
}