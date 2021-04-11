namespace TextActor.Converters
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TextActor.Extensions;
    using TextActor.Models;
    using TextActor.Services;
    using TextActor.ViewModels;

    /// <summary>
    /// Defines the <see cref="StoryConverter" />.
    /// </summary>
    public static class StoryConverter
    {
        #region Methods

        /// <summary>
        /// The GetDialogs.
        /// </summary>
        /// <param name="story">The story<see cref="Story"/>.</param>
        /// <param name="availableActors">The availableActors<see cref="IList{Actor}"/>.</param>
        /// <returns>The <see cref="Task{IList{Dialog}}"/>.</returns>
        public static async Task<IList<Dialog>> GetDialogs(this Story story, IList<Actor> availableActors)
        {
            var playedActors = JsonConvert.DeserializeObject<IList<Actor>>(story.ActorsJson);
            var dialogs = JsonConvert.DeserializeObject<IList<Dialog>>(story.DialogsJson);
            await dialogs.ValidateDialogs(availableActors, playedActors);
            return dialogs;
        }

        /// <summary>
        /// The ToStory.
        /// </summary>
        /// <param name="storyViewModel">The storyViewModel<see cref="StoryViewModel"/>.</param>
        /// <returns>The <see cref="Task{Story}"/>.</returns>
        public static async Task<Story> ToStory(this StoryViewModel storyViewModel)
        {
            var dialogs = await storyViewModel.DialogDataStore.GetItemsAsync();
            var playedActorIds = dialogs.Select(dialog => dialog.ActorId).Distinct();
            var availableActors = await storyViewModel.ActorDataStore.GetItemsAsync();
            if (availableActors == null || !availableActors.Any())
            {
                return null;
            }

            var actorDict = availableActors.ToIdDictionary();
            var playedActors = new HashSet<Actor>();
            var defaultActor = availableActors.First();
            foreach (var actorId in playedActorIds)
            {
                var actor = defaultActor;
                if (actorDict.ContainsKey(actorId))
                {
                    actor = actorDict[actorId];
                }

                if (!playedActors.Contains(actor))
                {
                    playedActors.Add(actor);
                }
            }

            var actorJson = JsonConvert.SerializeObject(playedActors.ToList());
            var dialogJson = JsonConvert.SerializeObject(dialogs);
            var story = new Story
            {
                Id = storyViewModel.Id,
                Name = storyViewModel.StoryTitle,
                Date = storyViewModel.Date,
                ActorsJson = actorJson,
                DialogsJson = dialogJson,
            };

            return story;
        }

        /// <summary>
        /// The ValidateDialogs.
        /// </summary>
        /// <param name="dialogs">The dialogs<see cref="IList{Dialog}"/>.</param>
        /// <param name="availableActors">The availableActors<see cref="IList{Actor}"/>.</param>
        /// <param name="playedActors">The playedActors<see cref="IList{Actor}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task ValidateDialogs(this IList<Dialog> dialogs, IList<Actor> availableActors, IList<Actor> playedActors)
        {
            if (dialogs == null || !dialogs.Any())
            {
                return;
            }

            var defaultActor = ActorDataStore.DefaultActor;
            var defaultActorIndex = availableActors.IndexOf(defaultActor);
            var dialogDetails = new List<DialogDetailViewModel>();
            if (playedActors == null || !playedActors.Any())
            {
                playedActors = new List<Actor> { defaultActor };
            }

            await Task.Run(() =>
            {
                var availableActorDict = availableActors.ToIdDictionary();
                var playedActorDict = playedActors.ToIdDictionary();
                for (var i = 0; i < dialogs.Count; i++)
                {
                    var dialog = dialogs[i];

                    // Validate actor
                    dialog.ActorId = availableActorDict.ContainsKey(dialog.ActorId) ? dialog.ActorId : defaultActorIndex;
                }
            });
        }

        #endregion Methods
    }
}