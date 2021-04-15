namespace TextActor.Services
{
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TextActor.Helpers;
    using TextActor.Models;

    /// <summary>
    /// Defines the <see cref="TextActorDataBase" />.
    /// </summary>
    public class TextActorDataBase
    {
        #region Fields

        private readonly SQLiteAsyncConnection database;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextActorDataBase"/> class.
        /// </summary>
        /// <param name="dbPath">The dbPath<see cref="string"/>.</param>
        public TextActorDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Actor>().Wait();
            database.CreateTableAsync<Story>().Wait();
            database.CreateTableAsync<Settings>().Wait();
            Initialize();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DefaultActor.
        /// </summary>
        public static Actor DefaultActor { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The DeleteActorAsync.
        /// </summary>
        /// <param name="actor">The actor<see cref="Actor"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public Task<int> DeleteActorAsync(Actor actor)
        {
            // Delete a actor.
            return database.DeleteAsync(actor);
        }

        /// <summary>
        /// The DeleteStoryAsync.
        /// </summary>
        /// <param name="story">The story<see cref="Story"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public Task<int> DeleteStoryAsync(Story story)
        {
            // Delete a story.
            return database.DeleteAsync(story);
        }

        /// <summary>
        /// The GetActorAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{Actor}"/>.</returns>
        public Task<Actor> GetActorAsync(int id)
        {
            // Get a specific actor.
            return database.Table<Actor>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// The GetActorsAsync.
        /// </summary>
        /// <returns>The <see cref="Task{List{Actor}}"/>.</returns>
        public Task<List<Actor>> GetActorsAsync()
        {
            //Get all actors.
            return database.Table<Actor>().ToListAsync();
        }

        /// <summary>
        /// The GetSettingsAsync.
        /// </summary>
        /// <returns>The <see cref="Task{Settings}"/>.</returns>
        public async Task<Settings> GetSettingsAsync()
        {
            var settings = await database.Table<Settings>().FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new Settings();
                await database.InsertAsync(settings);
            }

            //Get all stories.
            return await database.Table<Settings>().FirstOrDefaultAsync();
        }

        /// <summary>
        /// The GetStoriesAsync.
        /// </summary>
        /// <returns>The <see cref="Task{List{Story}}"/>.</returns>
        public Task<List<Story>> GetStoriesAsync()
        {
            //Get all stories.
            return database.Table<Story>().ToListAsync();
        }

        /// <summary>
        /// The GetStoryAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{Story}"/>.</returns>
        public Task<Story> GetStoryAsync(int id)
        {
            // Get a specific story.
            return database.Table<Story>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// The SaveActorAsync.
        /// </summary>
        /// <param name="actor">The actor<see cref="Actor"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public Task<int> SaveActorAsync(Actor actor)
        {
            if (actor.Id != 0)
            {
                // Update an existing actor.
                return database.UpdateAsync(actor);
            }
            else
            {
                // Save a new actor.
                return database.InsertAsync(actor);
            }
        }

        /// <summary>
        /// The SaveStoryAsync.
        /// </summary>
        /// <param name="story">The story<see cref="Story"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public Task<int> SaveStoryAsync(Story story)
        {
            if (story.Id != 0)
            {
                // Update an existing story.
                return database.UpdateAsync(story);
            }
            else
            {
                // Save a new story.
                return database.InsertAsync(story);
            }
        }

        /// <summary>
        /// The UpdateSettingsAsync.
        /// </summary>
        /// <param name="settingsAction">The settingsAction<see cref="Action{Settings}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task UpdateSettingsAsync(Action<Settings> settingsAction)
        {
            var settings = await GetSettingsAsync();
            settingsAction(settings);
            await database.UpdateAsync(settings);
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        private async void Initialize()
        {
            var actors = await GetActorsAsync();
            if (!actors.Any())
            {
                await SaveActorAsync(new Actor { IsProtected = true, Name = "Ivana", Pitch = 0.8f, Volume = 0.6f, LocaleName = TextToSpeechHelper.DefaultLocaleName });
                await SaveActorAsync(new Actor { IsProtected = true, Name = "Olga", Pitch = 0.7f, Volume = 0.8f, LocaleName = "Russian (Russia)" });
                await SaveActorAsync(new Actor { IsProtected = true, Name = "Indira", Pitch = 0.7f, Volume = 0.8f, LocaleName = "Hindi (India)" });
            }

            actors = await GetActorsAsync();
            DefaultActor = actors.First();
            var stories = await GetStoriesAsync();
            if (!stories.Any())
            {
                await SaveStoryAsync(new Story { Name = "Introduction" });
                await SaveStoryAsync(new Story { Name = "Party with Friends" });
            }
        }

        #endregion Methods
    }
}