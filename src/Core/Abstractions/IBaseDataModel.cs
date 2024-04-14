namespace Core.Abstractions
{
    /// <summary>
    /// Base Proprieties for every Entity. Every model that inherits from Base Model can be manipulated with CRUD operations form our generic repository without additional requirements.
    /// </summary>
    public interface IBaseDataModel
    {
        /// <summary>Stores the time when object was created</summary>
        DateTime Created { get; set; }

        /// <summary>Stores the time when object was modified. Nullable</summary>
        DateTime Changed { get; set; }

        /// <summary>
        /// Stores state of the Object. True if object is deleted and false otherwise
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Version of data
        /// </summary>
        int Version { get; set; }
    }
}
