namespace TripMapCam.App.UI.Services
{
    using SQLite;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TripMapCam.App.UI.Models;

    /// <summary>
    /// Defines the <see cref="TripMapCamDataBase" />.
    /// </summary>
    public class TripMapCamDataBase
    {
        #region Fields

        private readonly SQLiteAsyncConnection _database;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TripMapCamDataBase"/> class.
        /// </summary>
        /// <param name="dbPath">The dbPath<see cref="string"/>.</param>
        public TripMapCamDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<PhotoModel>().Wait();
            _database.CreateTableAsync<LocationModel>().Wait();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The DeleteLocationModelAsync.
        /// </summary>
        /// <param name="model">The model<see cref="LocationModel"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public Task<int> DeleteLocationModelAsync(LocationModel model)
        {
            // Delete a LocationModel from the data base.
            return _database.DeleteAsync(model);
        }

        /// <summary>
        /// The DeletePhotoModelAsync.
        /// </summary>
        /// <param name="model">The model<see cref="PhotoModel"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public Task<int> DeletePhotoModelAsync(PhotoModel model)
        {
            // Delete a PhotoModel from the data base.
            return _database.DeleteAsync(model);
        }

        /// <summary>
        /// The GetLocationModelAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{LocationModel}"/>.</returns>
        public Task<LocationModel> GetLocationModelAsync(int id)
        {
            // Get a specific PhotoModel.
            return _database.Table<LocationModel>()
                            .Where(i => i.PhotoId == id)
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// The GetLocationModelsAsync.
        /// </summary>
        /// <returns>The <see cref="Task{List{LocationModel}}"/>.</returns>
        public Task<List<LocationModel>> GetLocationModelsAsync()
        {
            //Get all LocationModel.
            return _database.Table<LocationModel>().ToListAsync();
        }

        /// <summary>
        /// The GetPhotoModelAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{PhotoModel}"/>.</returns>
        public Task<PhotoModel> GetPhotoModelAsync(int id)
        {
            // Get a specific PhotoModel.
            return _database.Table<PhotoModel>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// The GetPhotoModelsAsync.
        /// </summary>
        /// <returns>The <see cref="Task{List{PhotoModel}}"/>.</returns>
        public Task<List<PhotoModel>> GetPhotoModelsAsync()
        {
            //Get all PhotoModel.
            return _database.Table<PhotoModel>().ToListAsync();
        }

        /// <summary>
        /// The SaveLocationModelAsync.
        /// </summary>
        /// <param name="model">The model<see cref="LocationModel"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public Task<int> SaveLocationModelAsync(LocationModel model)
        {
            if (model.PhotoId != 0)
            {
                // Update an existing LocationModel.
                return _database.UpdateAsync(model);
            }
            else
            {
                // Save a new LocationModel.
                return _database.InsertAsync(model);
            }
        }

        /// <summary>
        /// The SaveLocationModelsAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{LocationModel}"/>.</param>
        /// <param name="token">The token<see cref="CancellationToken"/>.</param>
        public async void SaveLocationModelsAsync(IEnumerable<LocationModel> models, CancellationToken token)
        {
            foreach (var model in models)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await this.SaveLocationModelAsync(model);
            }
        }

        /// <summary>
        /// The SavePhotoModelAsync.
        /// </summary>
        /// <param name="model">The model<see cref="PhotoModel"/>.</param>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        public Task<int> SavePhotoModelAsync(PhotoModel model)
        {
            if (model.Id != 0)
            {
                // Update an existing PhotoModel.
                return _database.UpdateAsync(model);
            }
            else
            {
                // Save a new PhotoModel.
                return _database.InsertAsync(model);
            }
        }

        /// <summary>
        /// The SavePhotoModelsAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{PhotoModel}"/>.</param>
        /// <param name="token">The token<see cref="CancellationToken"/>.</param>
        public async void SavePhotoModelsAsync(IEnumerable<PhotoModel> models, CancellationToken token)
        {
            foreach (var model in models)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await this.SavePhotoModelAsync(model);
            }
        }

        #endregion
    }
}
