namespace TripMapCam.App.UI.Services
{
    using SQLite;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TripMapCam.App.UI.Core;
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

        #endregion
    }
}
